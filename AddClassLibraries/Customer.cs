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

        // override可能なメンバーは、virtual override,abstractのいずれか
        // override後、子クラスのメソッドにアクセスする場合は、クラスなし、もしくはthis.
        // 親クラス（基底クラス）のメソッドにアクセスする場合は、base.(Javaでいうsuper.)
        // Javaと異なり、サブクラスを基底クラスの変数に代入できるが、サブクラス特有のオブジェクト・メソッドにはアクセスできなくなる。
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


        // 関係演算子のオーバーロード オーバーロードにはstatic宣言が必要
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
    }
}
