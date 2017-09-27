using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AddClassLibrary
{
    /// <summary>
    /// ボタンイベント処理のためのイベントハンドラ。
    /// 画面 MainWindowクラスでボタンに対してイベントを紐づけるとき、
    /// 
    /// </summary>
    class DelegateCommand : ICommand
    {
        /// <summary>
        /// MainViewModelのインスタンスと処理を紐づける。
        /// </summary>
        /// <param name="owner">オブジェクト：ViewModelクラス ここではMainViewModel</param>
        /// <param name="execute">デリゲート：Executeメソッド ここでは各種ボタン処理のためのExecuteメソッド</param>
        public DelegateCommand(INotifyPropertyChanged owner, Action execute)
        {
            // owner.PropertyChanged.add(引数s,eをもとにonCanExcuteChanged()を実行する匿名関数のデリゲート)
            //
            // owner.PropertyChangedはsenderとPropertyChangedEventArgsの2つの引数を持つイベントハンドラメソッドのデリゲート。
            // +=はevent型独自の使い方か？ イベント型にイベントハンドラを追加する。
            // この1行で、2つの引数をもとにOnCanExecuteChanged()を呼び出す匿名関数のデリゲートを、PropertyChanedに追加している。
            // やりたいことは、イベントハンドラの登録？s, eはダミーなので、結局、
            // イベントが起きたら、OnCanExecuteChanged()を呼び出すだけ。
            owner.PropertyChanged += (s, e) => OnCanExecuteChanged();

            // _executeにデリゲードを入れる。引数 x をもとに 引数で渡されたexecute()を実行する。
            _execute = x => execute();

            // 常にtrueを返す
            _canExecute = x => true;
        }

        /// <summary>
        /// MainViewModelのインスタンスと処理を紐づける。
        /// </summary>
        /// <param name="owner">インスタンス：ViewModelクラス ここではMainViewModel</param>
        /// <param name="execute">デリゲート：Executeメソッド ここでは各種ボタン処理のためのExecuteメソッド</param>
        /// <param name="canExecute">デリゲート：canExecuteメソッド Execute可能かどうかを判定するメソッド</param>
        public DelegateCommand(
            INotifyPropertyChanged owner, Action execute, Func<bool> canExecute)
        {
            // イベントハンドラの登録？
            // イベントが起きたら、OnCanExecuteChanged()を呼び出す。
            // OnCanExcuteChangedの中では、CanExecuteChanged()（イベントハンドラ）に自分自身をセットする
            owner.PropertyChanged += (s, e) => OnCanExecuteChanged();

            // _executeにデリゲードを入れる。
            // 引数 x をもとに execute()を実行する。
            // xは_execute (Action<object>)がObject型の引数を要求するためセットしたダミーの変数
            _execute = x => execute();

            // _canExecuteにデリゲードを入れる。
            // 引数 x をもとに canExecute()を実行する。
            // xは_canExecute (Func<object, bool>) がObject型の引数を要求するためセットしたダミーの変数
            _canExecute = x => canExecute();
        }

        public DelegateCommand(INotifyPropertyChanged owner, Action<object> execute)
        {
            owner.PropertyChanged += (s, e) => OnCanExecuteChanged();
            _execute = execute;
            _canExecute = x => true;
        }

        public DelegateCommand(INotifyPropertyChanged owner, Action<object> execute, Func<object, bool> canExecute)
        {
            owner.PropertyChanged += (s, e) => OnCanExecuteChanged();
            _execute = execute;
            _canExecute = canExecute;
        }

        // デリゲート宣言：_executeをobjectを引数とする汎用delegate型として定義する。
        private readonly Action<Object> _execute;

        // メソッド：Executeメソッドを宣言する。objectを引数にexecuteを実行させる。
        public void Execute(object value) => _execute(value);

        // デリゲート宣言：_canExecuteを、object,boolを引数とする汎用delegate型として定義する。
        private readonly Func<object, bool> _canExecute;

        // メソッド：CanExecuteメソッドを宣言する。objectを引数に_canExecuteを実行させる。
        public bool CanExecute(object value) => _canExecute(value);

        // デリゲート宣言：CanExecuteChangedを、イベント型のデリゲート型として定義する。
        // イベント型のCanExecuteChangedを宣言
        public event EventHandler CanExecuteChanged;

        // メソッド：CanExecuteChangedがnull出ない場合、イベントハンドラメソッド（CanExecuteChanged）に自身を渡す。
        protected virtual void OnCanExecuteChanged()
        {
            if (CanExecuteChanged == null) return;
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
