using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddClassLibrary
{
    public class CustomerViewModel : ModelBase<Customer>
    {
        public CustomerViewModel() { }

        // 引数にCustomerをもらい、CustomerViewModel自身のプロパティにセットするコンストラクタ
        // Nameは属性が変わらないが、PostCodeはCustomerがintに対してCustomerViewModelはStringのため
        // 型変換をかける。
        public CustomerViewModel(Customer model)
        {
            SetProperties(model);
        }

        //CustomerViewModelを受け取り、自身のプロパティにセットするコンストラクタ
        public void SetProperties(CustomerViewModel source)
        {
            if (source == null) return;
		
		// テキストテンプレートによる代入の作成
            Id = source.Id;
            Name = source.Name;
            Kana = source.Kana;
            Address = source.Address;
            TelNumber = source.TelNumber;
            Memo = source.Memo;
            TimeStamp = source.TimeStamp;
			PostCode = source.PostCode;

			// Template使用前のコード
            // Name = source.Name;
            
        }


        public void SetProperties(Customer source)
        {
            if (source == null) return;

			// テキストテンプレートによる代入の作成
            Id = source.Id;
            Name = source.Name;
            Kana = source.Kana;
            Address = source.Address;
            TelNumber = source.TelNumber;
            Memo = source.Memo;
            TimeStamp = source.TimeStamp;
            PostCode = source.PostCode.ToString();

			// Template使用前のコード
            // Name = source.Name;

        }

        // メソッド：Customerをメンバ変数 _modelにセットするプロパティ
        //_modelはスーパークラスに定義されている
        public Customer Customer => _model;

		// テキストテンプレートによるメンバ変数とプロパティの作成
		// メンバ変数：名称を格納するためのメンバ変数_Idの定義
		private int _Id;

		// プロパティ：_IdをハンドリングするためのプロパティIdの定義
		public int Id
		{
            get { return _Id; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_Id == value) return;
                _Id = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(Id), value))
                {
                    _model.Id = value;
                }

                // IdとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(Id));
                OnPropertyChanged(nameof(Error));
            }
        }

		// メンバ変数：名称を格納するためのメンバ変数_Nameの定義
		private string _Name;

		// プロパティ：_NameをハンドリングするためのプロパティNameの定義
		public string Name
		{
            get { return _Name; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_Name == value) return;
                _Name = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(Name), value))
                {
                    _model.Name = value;
                }

                // NameとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Error));
            }
        }

		// メンバ変数：名称を格納するためのメンバ変数_Kanaの定義
		private string _Kana;

		// プロパティ：_KanaをハンドリングするためのプロパティKanaの定義
		public string Kana
		{
            get { return _Kana; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_Kana == value) return;
                _Kana = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(Kana), value))
                {
                    _model.Kana = value;
                }

                // KanaとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(Kana));
                OnPropertyChanged(nameof(Error));
            }
        }

		// メンバ変数：名称を格納するためのメンバ変数_Addressの定義
		private string _Address;

		// プロパティ：_AddressをハンドリングするためのプロパティAddressの定義
		public string Address
		{
            get { return _Address; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_Address == value) return;
                _Address = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(Address), value))
                {
                    _model.Address = value;
                }

                // AddressとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(Error));
            }
        }

		// メンバ変数：名称を格納するためのメンバ変数_TelNumberの定義
		private string _TelNumber;

		// プロパティ：_TelNumberをハンドリングするためのプロパティTelNumberの定義
		public string TelNumber
		{
            get { return _TelNumber; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_TelNumber == value) return;
                _TelNumber = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(TelNumber), value))
                {
                    _model.TelNumber = value;
                }

                // TelNumberとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(TelNumber));
                OnPropertyChanged(nameof(Error));
            }
        }

		// メンバ変数：名称を格納するためのメンバ変数_Memoの定義
		private string _Memo;

		// プロパティ：_MemoをハンドリングするためのプロパティMemoの定義
		public string Memo
		{
            get { return _Memo; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_Memo == value) return;
                _Memo = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(Memo), value))
                {
                    _model.Memo = value;
                }

                // MemoとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(Memo));
                OnPropertyChanged(nameof(Error));
            }
        }

		// メンバ変数：名称を格納するためのメンバ変数_TimeStampの定義
		private byte[] _TimeStamp;

		// プロパティ：_TimeStampをハンドリングするためのプロパティTimeStampの定義
		public byte[] TimeStamp
		{
            get { return _TimeStamp; }
            set
            {
                // プロパティの引数を_Nameにセットする
                if (_TimeStamp == value) return;
                _TimeStamp = value;

                // ValueをValidationValueメソッドを用いて入力チェックし、
                // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
                if (ValidationValue(nameof(TimeStamp), value))
                {
                    _model.TimeStamp = value;
                }

                // TimeStampとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
                OnPropertyChanged(nameof(TimeStamp));
                OnPropertyChanged(nameof(Error));
            }
        }


        //  // メンバ変数：名称を格納するためのメンバ変数_Nameの定義
        // public string _Name;

        // プロパティ：_NameをハンドリングするためのプロパティNameの定義
        // CustomerViewModelではデータをプロパティ自体ではなく、
        // メンバー変数に保持しています。
        // public string Name
        // {
        //    get { return _Name; }
        //    set
        //    {
        //        // プロパティの引数を_Nameにセットする
        //        if (_Name == value) return;
        //        _Name = value;
		//
        //        // スーパークラスの_modelにはすでにCustomerがセットされている。
        //        // スーパークラスのValidationValueメソッドを用いて入力チェックし、
        //        // 問題がなければスーパークラスの_modelのNameプロパティに引数をセットする。
        //        if (ValidationValue(nameof(Name), _model.Name))
        //        {
        //            _model.Name = value;
        //        }

        //        // NameとErrorに変化があったことをスーパークラスのOnPropertyChengeに伝える
        //        OnPropertyChanged(nameof(Name));
        //        OnPropertyChanged(nameof(Error));
        //    }
        //}

        // メンバ変数：郵便番号を格納するためのメンバ変数_PostCodeの定義
        public string _postCode;

        // プロパティ：_PostCodeをハンドリングするためのプロパティPostCodeの定義
        public string PostCode
        {
            get { return _postCode; }
            set
            {
                if (_postCode == value) return;
                _postCode = value;

                int i = 0;

                // プロパティ：郵便番号の場合、Customerでは郵便番号を数値で持っていますが、
                // CustomerViewModelでは画面入力値と連携させるためにstringで持っています。
                // intに変換できないときのエラーも追加します。
                if (!int.TryParse(value, out i))
                {
                    UpdateErors(
                        nameof(PostCode),
                        "数値以外の文字が入力されているか、桁数がオーバーしています");
                }
                else
                {
                    if (ValidationValue(nameof(PostCode), i))
                    {
                        // スーパークラスのValidationValueメソッドを用いて入力チェックし、問題がなければ
                        // スーパークラスの_modelのPostCodeプロパティに引数をintに変換した値をセットする。
                        _model.PostCode = i;
                    }
                }
                OnPropertyChanged(nameof(PostCode));
                OnPropertyChanged(nameof(Error));
            }
        }

        public override string Error
        {
            get
            {
                if (_errors.Count > 0) return "不正な値が入力されています";
                if (!string.IsNullOrWhiteSpace(_model.Address) && _model.PostCode == 0)
                {
                    return "住所に準じた郵便番号が入力されていません";
                }
                return null;
            }
        }
    }
}

