using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using AddClassLibrary;

namespace AddForm
{
    public partial class MainWindow : Form
    {
        private readonly MainViewModel _viewModel = new MainViewModel();

        public MainWindow()
        {
            // 各種初期化する。

            // InitializeComponentはVisualStudioが自動作成する。
            // ソースはMainWindow.Designer.csに作られる。
            InitializeComponent();

            // InitializeViewModelは、画面に近いViewModelであるMainViewModelのインスタンス、_viewModelに
            // プロパティファイルの値、エラー時メッセージボックスのデリゲート、
            // プロパティ変更イベント発生時のイベントハンドラをデリゲートとして設定する。
            //なお、_viewModel は、MainWindowsのメンバ変数であり、宣言時（画面表示時）に初期化される。
            InitializeViewModel();

            // 各画面オブジェクトにdatabindingにて_viewModelの値を結び付ける
            InitializeDataBindings();

            // コマンドボタンの初期化＝コマンドボタンと_viewModelのイベントを結び付ける。
            InitializeCommands();
        }

        private void InitializeViewModel()
        {
            // ViewModel(CustomerViewModel, MainViewModel, MainModelの初期化

            // SettingDesiner.csの情報を_viewModel () のHelpUriに設定
            _viewModel.HelpUri = Properties.Settings.Default.HelpUri;

            // SettingDesiner.csの情報を_viewModel () DebugConnectionString に設定
            _viewModel.DebugConnectionString = Properties.Settings.Default.DebugConnectionString;

            // SettingDesiner.csの情報を_viewModel () ReleaseConnectionString に設定
            _viewModel.RelaseConnectionString = Properties.Settings.Default.ReleaseConnectionString;

            // デリゲート Action<string> _viewModel.ShowErrorMessage に、メッセージボックス表示のメソッドを設定
            // メソッドの引数はString m → これがメッセージとして表示される。
            _viewModel.ShowErrorMessage = m => MessageBox.Show(
                m, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            // デリゲート Func<string, bool> _viewModel.ShowYesNoMessage に、メッセージボックス表示の
            //メソッドを設定。メソッドの引数はString m → これがメッセージとして表示される。
            _viewModel.ShowYesNoMessage = m => MessageBox.Show(
                m, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;

            // デリゲート Event（イベントハンドラ） _viewModel.PropertyChangedに
            //_viewModel_PropertyChangedを設定。
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(
            object sender, PropertyChangedEventArgs e)
        {
            // _viewModel_PropertyChangedの役割は、イベント引数のプロパティ名がItemであることを
            // チェックする。
            if (e.PropertyName != nameof(MainViewModel.Item)) return;

            // _viewModel.ItemのIdと
            // Mainwindow上のNameリストボックスの要素のコレクションを取得し
            // CustomerViewModelのリストに変換したものを取得する。           
            int id = _viewModel.Item?.Id ?? 0;
            List<CustomerViewModel> list = nameListBox.Items.Cast<CustomerViewModel>().ToList();

            // LINQでCustomerViewModelのリストから、_viewModel.ItemのIdに合致するものを取得する。
            var q =
                from i in Enumerable.Range(0, list.Count)
                where list[i].Id == id
                select i;

            // 取得した値をもとに、リストボックスを選択状態にする。
            nameListBox.SelectedIndex = q.FirstOrDefault();
        }

        private void InitializeDataBindings()
        {
            MainViewModel v = _viewModel;

            // Formにある各種オブジェクトのTextの項目名を取得しておく。のちの
            // dataBindingの項目指定で使用する。
            const string TEXT = nameof(Text);

            dataBindingSource.DataSource = v;

            this.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Title));
            keywordTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Keyword));
            nameTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Name));
            kanaTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Kana));
            postCodeTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.PostCode));
            addressTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Address));
            telNulberTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.TelNumber));
            memoTextBox.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Memo));
            errorMessageLabel.DataBindings.Add(TEXT, dataBindingSource, nameof(v.Error));

            listBindingSource.DataSource = dataBindingSource;
            listBindingSource.DataMember = nameof(v.Items);

            // listboxに設定するデータのデータソースとしてlistBindingSourceを指定する。
            // listBindingSourceのDataSourceはすでにdataBindingSourceを指定してある。
            // またDataMemberには_viewModel.Itemを指定している。
            nameListBox.DataSource = listBindingSource;

            // listboxに表示する項目として、CustomerViewModelのNameを指定する。
            nameListBox.DisplayMember = nameof(v.Name);

            // リストボックスの選択状態がへんっこうされるたびに、最新の選択項目をリストボックスに
            // 表示するための設定。
            // nameListBoxのIndexChangedイベントが変更されたときのイベントハンドラを追加する。
            //listBox_SelectedIndexChanged ではリストボックスの選択結果を_viewModel.Itemに設定する。
            // listBindingSource.DataSourceとして、_viewModelが選択されているのはここの伏線。
            nameListBox.SelectedIndexChanged += listBox_SelectedIndexChanged;

            // ErrorProviderのDataSourceにdataBindingSourceを指定する。
            errorProvider1.DataSource = dataBindingSource;
        }


        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // _viewModel.Itemは MainViewModelのプロパティとして定義された CustomerViewModel.Itemをさす
            _viewModel.Item = nameListBox.SelectedItem as CustomerViewModel;
        }


        // コマンド項目のバインディング
        // View側の項目は、Control型のtarget、ViewModel側はICommand型のcommandをとる。
        // InitializeCommandで、ボタンと_ViewModelのコマンドプロパティをセットする。
        private void AddCommand(Control target, ICommand command)
        {
            // 引数としてコマンドボタンと、_viewModelの各種コマンドを取得する。
            // command.CanExecuteの結果をtargetの活性・非活性にセットするデリゲートを
            // command.CanExcuteChangedにセットする。

            // command.CanExecuteChangedは Event型のデリゲート
            // ラムダ式のs,eは、object型、EventArgs型の引数。メソッドの中では未使用のダミー引数。
            // メソッドでは_viewModel.commandのCanExecuteメソッドを実行し、結果をtarget.Enabledにセットする。

            // command.CanExecute(object balue)メソッドは、呼び出されると
            // DelegateCommandクラスの_canExecuteデリゲードを実行する。
            command.CanExecuteChanged += (s, e) => target.Enabled = command.CanExecute(null);

            // target.ClickはEvent型のデリゲート
            // ラムダ式のs,eは、object型、EventArgs型の引数。メソッドの中では未使用のダミー引数。
            // コマンドを実行する。
            // command.Execute(object balue)メソッドは、呼び出されると
            // DelegateCommandクラスの_executeデリゲードを実行する。

            // _executeデリゲードへのメソッド参照は、InitializeCommandsでMainViewModelクラスのプロパティを
            // 参照した時の、DelegateCommandインスタンス作成にて設定済み。
            target.Click += (s, e) => command.Execute(null);
        }

        private void InitializeCommands()
        {
            // イベントボタンとMainViewModelのコマンドを結び付ける。
            // イベントボタンはこのクラスのイベントボタン、MainViewModelのコマンドボタンは
            // MainViewModelクラスの各プロパティからGetする。
            // なお、MainViewModelクラスのプロパティは、もしインスタンス化されていなければ
            // 自動的にインスタンスを作成する。
            AddCommand(searchButton, _viewModel.SearchCommand);
            AddCommand(clearButton, _viewModel.ClearCommand);
            AddCommand(updateButton, _viewModel.UpdateCommand);
            AddCommand(deleteButton, _viewModel.DeleteCommand);
            AddCommand(aboutButton, _viewModel.AboutCommand);
            AddCommand(helpButton, _viewModel.HelpCommand);

            // この処理について概要をコメントしておく。
            // AddCommandの引数になっている _viewModel.XXXXCommandは、_viewModelのICommand型のプロパティだが
            // プロパティのGet時に、それぞれ、インターフェースICommandを実装したDelegateCommand型としてインスタンス化される。
            // DelegateCommand型のインスタンスは、
            //    ExecuteメソッドにMainViewModelで定義したそれぞれのメソッドを設定するため、
            //    CanExecuteメソッドにメソッドの実行可否を設定するため、
            // AddCommandメソッドではクリックイベントのイベントハンドラに
            // Excecuteを呼び出すよう処理を記述している。
        }


        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
