using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            Prefecture prefecture = InputPrefecture();
            if (prefecture == null) return;

            Age age = InputAge();
            if (age == null) return;

            if (prefecture.Value == Prefecture.Tochigi && age.Value >= 18)
            {
                UserInterface.OutputString("割引料金です");
            }
            else
            {
                UserInterface.OutputString("通常料金です");
            }
        }

        static Prefecture InputPrefecture()
        {
            string choices = GetPrefectureChoices();
            string message = "次の地域を入力してください [" + choices + "]";
            string s = UserInterface.InputString(message);

            Prefecture r = new Prefecture(s);
            if (r.IsValid) return r;
            UserInterface.OutputString("地域の入力が間違っています");
            return null;
        }

        static string GetPrefectureChoices()
        {
            string r = "";
            List<string> list = Prefecture.GetList();
            foreach (var x in list)
            {
                if (r != "") r += "/";
                r += x;
            }

            return r;
        }

        static Age InputAge()
        {
            string s = UserInterface.InputString("年齢を入力してください");
            Age r = new Age(s);
            if (r.IsValid) return r;

            UserInterface.OutputString("年齢の入力が間違っています");
            return null;
        }


    }


    class Prefecture
    {
        public const string Tochigi = "栃木";
        public const string Gunma = "群馬";
        public const string Other = "その他";

        public Prefecture(string value)
        {
            IsValid = IsExist(value);
            Value = value;
        }

        public string Value { get; private set; }
        public bool IsValid { get; private set; }

        public static List<string> GetList()
        {
            return new List<string> { Tochigi, Gunma, Other };
        }

        public bool IsExist(string value)
        {
            List<string> list = GetList();
            foreach (var x in list)
            {
                if (x == value) return true;
            }
            return false;
        }
    }

    class Age
    {
        public Age(string value)
        {
            int i = 0;
            IsValid = int.TryParse(value, out i);
            Value = i;
        }
        public int Value { get; private set; }
        public bool IsValid { get; private set; }
    }

    class UserInterface
    {
        public static string InputString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public static void OutputString(string value)
        {
            Console.WriteLine(value);
            Console.ReadLine();
        }
    }

}
