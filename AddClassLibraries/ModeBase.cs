using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AddClassLibraries
{
    // インデクサ＝インデクサーに特化したプロパティ。
    // インデクサ＝配列のようにオブジェクトにインデックスをつけられる。


    // データ型の別名定義。シノニムのようなもの。 Errorsクラス＝Directoryクラスになる。
    using Errors = Dictionary<string, string>;

    // インターフェースについて。インターフェースは多重継承できる。
    // インターフェースを継承すると、インターフェースの規格に定義されたメンバーの条件を満たす必要がある。
    // INotifyPropertyChanged → プロパティが変更されたことをクライアントに通知する　→ メンバー変数に PropertyChangedEventHandlerを持つ
    //IDataErrorInfo → UIにバインドできるカスタムエラー情報を提供する機能。→ インデクサ（項目名）とErrorプロパティの定義
    public abstract class ModeBase : INotifyPropertyChanged, IDataErrorInfo
    {
        // PropertyChangedはデリゲード。PropertyChangedEventHandlerメソッドへの参照を代入している。
        // イベントに関するデリゲードは、Staticメソッドのイメージ。修飾子をeventに。クラスは指定しない。
        public event PropertyChangedEventHandler PropertyChanged;

        // このメソッドを呼び出すと、PropertyChangedイベントハンドラが発火する。
        // ベースとなるクラスで継承して使うことが前提。protectedで宣言して継承先でも呼び出せるよう配慮する。
        protected void OnPropertyChaged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChangedEventArgs a = new PropertyChangedEventArgs(propertyName);

            // PropertyChangedEventArgsを引数に、PropertyChangedEventHandlerを呼び出す。
            // -> Eventに登録されたデリゲードが順次実行される。
            PropertyChanged(this, a);

        }

        // usingディレクティブを使用して定義したDictionaryの別名 Errorを初期化する。
        // ベースとなるクラスで継承して使うことが前提。protectedで宣言して継承先でも呼び出せるよう配慮する。
        protected Errors _errors = new Errors();

        // インデクサを定義する。プロパティ名を引数に自分自身を指定されたらエラーの一覧を返す。
        public string this[string propertyName]
        {
            get
            {
                // 参考演算子 A?B:C → Aがtrue以外ならB、そうでなければC
                return
                    _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
            }
        }
        
        // Error → 
        // 継承されることを前提にしたプロパティ（抽象メソッド）
        // 記述の方法が自動プロパティと似ているが、abstractをつけると、抽象プロパティと認識される。→ 実装でgetメソッドの記述が必要になる。
        // 抽象メンバーには宣言だけで実装が存在しない。→ オーバーライドするのが前提のため。
        abstract public string Error { get; }

        /// <summary>
        ///  プロパティエラーメッセージを更新します。
        /// </summary>
        /// <param name="name">プロパティ名</param>
        /// <param name="value">エラーメッセージ</param>

        // UpdateErrorsが呼ばれると、CustomerオブジェクトのErrorリストにエラーが追加される。
        // ただしValueが空欄だったら、Errorを削除する。
        // ベースとなるクラスで継承して使うことが前提。protectedで宣言して継承先でも呼び出せるよう配慮する。
        protected void UpdateErrors(string name, string value)
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
