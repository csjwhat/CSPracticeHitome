using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AddClassLibrary
{
    public partial class MainViewModel : CustomerViewModel
    {
        public MainViewModel() { Clear(); }

        public string Title => "一目でわかる顧客情報";

        public Action<string> ShowErrorMessage { private get; set; }
        public Func<string, bool> ShowYesNoMessage { private get; set; }

        public string DebugConnectionString
        {
            get { return AddDbContext.DebugConnectionString; }
            set { AddDbContext.DebugConnectionString = value; }
        }

        public string RelaseConnectionString
        {
            get { return AddDbContext.ReleaseConnectionString; }
            set { AddDbContext.ReleaseConnectionString = value; }
        }

        public string HelpUri { get; set; }

        // プロパティ：画面と共有するCustomerViewModelのリスト。ObservableCollectionを使うことで
        // 内容変更時にイベントを発生させる。
        public ObservableCollection<CustomerViewModel> Items { get; private set; }
            = new ObservableCollection<CustomerViewModel>();

        private CustomerViewModel _item;
        public CustomerViewModel Item
        {
            get { return _item; }
            set
            {
                if (_item == value) return;
                _item = value;

                // 引数で与えられた（=の右辺で与えられた）値をセットして、
                // Itemの値の変更時のイベントハンドラを設定する。
                SetProperties(value);
                OnPropertyChanged(nameof(Item));
            }
        }

        // キーワードの定義
        private string _keyword = "";
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                if (_keyword == value) return;
                _keyword = value;
                OnPropertyChanged(nameof(Keyword));
            }
        }

        // 画面に表示するItemsをクリアする。
        private void Clear()
        {
            Item = null;
            Items.Clear();
            CustomerViewModel m =
                new CustomerViewModel { Id = 0, Name = "(新規)", PostCode = "0" };
            Items.Add(m);
            Item = m;
        }

        // 検索を実行する。
        private void ExecuteSearchCommand()
        {
            Clear();
            foreach (Customer x in MainModel.GetItems(Keyword))
            {
                CustomerViewModel item = new CustomerViewModel(x);
                Items.Add(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        private void ExecuteClearCommand()
        {
            Keyword = "";
            Clear();
            OnPropertyChanged(nameof(Items));
        }

        private bool CanExecuteUpdateCommand() => !HasErrors;
        private void ExecuteUpdateCommand()
        {
            Action add = () =>
            {
                Customer m = new Customer();

                // Customerには_model（ModelBase<Customer>）がセットされている。
                // mにも値をセットする。→ mはDB登録に使用する。
                m.SetProperties(Customer);

                // DBに登録する。
                m = MainModel.AddItem(m);

                // m をCustomerViewModelに設定
                CustomerViewModel item = new CustomerViewModel(m);

                // 画面と共有するCustomerViewModelのリスト（プロパティ）に設定
                // このプロパティに設定すると変更をイベントハンドラが拾う仕組み
                Items.Insert(1, item);

                // ItemにCustomerをセットする。
                Item = item;
            };
            Action update = () =>
            {
                Customer m = Customer;
                m = MainModel.UpdateItem(m);
                Item.SetProperties(m);
                SetProperties(Item);
            };

            // ActionはItem.Idが0ならば、add, Item.Idが0いがいならupdate
            Action a = (Item.Id == 0) ? add : update;
            ExecuteUpdateItem(a);
        }

        private bool CanExecuteDeleteCommand() => !HasErrors && Id != 0;
        private void ExecuteDeleteCommand()
        {
            if (!ShowYesNoMessage("選択中の顧客情報を削除してよいですか？")) return;
            Action delete = () =>
            {
                MainModel.DeleteItem(Customer);
                CustomerViewModel m = Item;
                Item = null;
                Items.Remove(m);
                Item = Items[0];
            };
            ExecuteUpdateItem(delete);

        }

        private void ExecuteAboutCommand() { }
        private void ExecuteHelpCommand() { }

        private void ExecuteUpdateItem(Action action)
        {
            try
            {
                // action()には、AddもしくはUpdateのメソッドがセットされているので実行する。
                action();
                OnPropertyChanged(nameof(Items));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ShowErrorMessage("他のユーザによって変更されています。再度検索しなおしてください。\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("データの更新または削除に失敗しました\n\n" + ex.Message);
            }
        }
    }
}
