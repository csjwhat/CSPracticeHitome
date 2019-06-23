using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddClassLibraries
{
    public class Customer
    {
        // 通常のプロパティ
        //string _name = "";
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
        // 以下 自動プロパティ

        public int Id { set; get; } = 0;

        public string Name { set; get; } = "";

        public string Kana { set; get; } = "";

        public int PostCode { get; set; } = 0;

        public string Address { get; set; } = "";

        public string TelNumber { get; set; } = "";

        public string Memo { get; set; } = "";

        public void SetProperties(Customer source)
        {
        }

        // 関係演算子のオーバーロード オーバーロードにはstatic宣言が必要
        public static bool operator ==(Customer a, Customer b)
        {
            return a?.Id == b?.Id;
        }

        public static bool operator !=(Customer a, Customer b)
        {
            return a?.Id != b?.Id;
        }
    }
}
