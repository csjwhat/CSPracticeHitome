using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddClassLibrary
{
    // ErrorsをDictionaryオブジェクトの別名として登録する
    using Errors = Dictionary<string, string>;
    public abstract class ModelBase<T> : INotifyPropertyChanged, IDataErrorInfo
        where T : new()
    {
        // メンバ変数：データモデル。インスタンス生成時にジェネリックで決められた型のインスタンスを_modelに入れる。
        // 入力チェック対象となるオブジェクトを格納する。
        protected T _model = new T();

        // メンバ変数：イベントハンドラ ＝ プロパティ（派生クラスで設定したプロパティ）が
        //されたときに通知するためのイベント。
        // TODO:Event PropertyChangedEventHandlerの使い方
        public event PropertyChangedEventHandler PropertyChanged;

        // メソッド：データバインディング後、プロパティに変更があるとこのメソッドが呼ばれるようになる。
        // ハンドラがnullでなければ、PropertyChangedEventArgsを作成してプロパティ変更のイベントを呼ぶ。
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChangedEventArgs a = new PropertyChangedEventArgs(propertyName);

            // イベントハンドラを登録する。
            PropertyChanged(this, a);
        }

        // メンバ変数：エラーリスト。Dictionary<string, string>を _errors という名前で作成する。初めは 0 件。
        protected Errors _errors = new Errors();

        // インデクサ：インデクサの設定
        public string this[string propertyName]
        {
            get
            {
                // インデクサのインデックスとして、文字列を受け取り
                // _errorsにキーのオブジェクトがあれば、ErrorsのValue(Dictionary（JavaのMap)のValue)を返す。
                return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
            }
        }

        // プロパティ：Errorを返す
        abstract public string Error { get; }

        // プロパティ：エラーがあるかどうかを返す
        public bool HasErrors
        {
            get
            {
                // エラーが0件でなかったら、もしくは、Errorがnullや空でなければtrue
                return _errors.Count != 0 || !string.IsNullOrEmpty(Error);
            }
        }

        // メソッド：値の検証を行う
        // 引数 name = クラス名
        // 引数 value = 検証するstring / int
        protected bool ValidationValue(string name, object value)
        {
            try
            {
                // おまじない？：ValidationContext型のインスタンスを作成して変数vに入れる
                ValidationContext v = new ValidationContext(_model, null, null);
                v.MemberName = name;

                // ValidationContextとvalueを使ってプロパティの値を検証する。
                Validator.ValidateProperty(value, v);

                // エラーがなければ、_errorsからエントリを削除する。
                _errors.Remove(name);
                return true;
            }
            catch (Exception ex)
            {
                // 例外が発生したら、Errorsのインデクサにプロパティ名を渡してメッセージを格納する。
                _errors[name] = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// プロパティエラーメッセージを更新します
        /// </summary>
        /// <param name="name">プロパティ名</param>
        /// <param name="value">エラーメッセージ文字列</param>
        // _errorsの、name のvalueを 引数で受けたものとする。
        protected void UpdateErors(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _errors.Remove(name);
            }
            else
            {
                _errors[name] = value;
            }
        }
    }
}
