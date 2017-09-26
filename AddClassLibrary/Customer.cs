using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AddClassLibrary
{
    public class Customer
    {
        // 自動プロパティ
        [Key]
        public int Id { get; set; } = 0;

        [StringLength(16, ErrorMessage = "名前は{1}文字以内で入力してください")]
        public string Name { get; set; } = "";

        [StringLength(16, ErrorMessage = "カナは{1}文字以内で入力してください)")]
        public string Kana { get; set; } = "";

        [Range(0, 9999999, ErrorMessage = "郵便番号は{1} ～ {2}の数値で入力して下さい")]
        public int PostCode { get; set; } = 0;

        [StringLength(64, ErrorMessage = "住所は{1}文字以内で入力してください")]
        public string Address { get; set; } = "";

        [StringLength(14, ErrorMessage = "電話番号は{1}文字以内で入力してください")]
        public string TelNumber { get; set; } = "";

        public string Memo { get; set; } = "";

        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public void SetProperties(Customer source)
        {
            Id = source.Id;
            Name = source.Name;
            Kana = source.Kana;
            PostCode = source.PostCode;
            Address = source.Address;
            TelNumber = source.TelNumber;
            Memo = source.Memo;
            TimeStamp = source.TimeStamp;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            int a = GetHashCode();
            int b = obj.GetHashCode();
            return a == b;
        }

        public static bool operator ==(Customer a, Customer b)
        {
            //return a?.Id == b?.Id;
            object o1 = a;
            object o2 = b;

            if (o1 == null && o2 == null) return true;
            if (o1 == null) return false;
            if (o2 == null) return false;
            return o1.Equals(o2);

        }

        public static bool operator !=(Customer a, Customer b)
        {
            //return a?.Id != b?.Id;
            object o1 = a;
            object o2 = b;

            if (o1 == null && o2 == null) return false;
            if (o1 == null) return true;
            if (o2 == null) return true;
            return !o1.Equals(o2);
        }

        //// フィールド
        //string _name = "";

        // 基本的なプロパティ。Setterは引数なしで値をセットできる。
        //string Name
        //{
        //    get
        //    {
        //        return _name;
        //    }
        //    set
        //    {
        //        _name = value;
        //    }
        //}


        //// メソッド
        //void SetProperties(Customer source)
        //{
        //}



    }
}
