using System;

namespace ConsoleApplication13
{
    class Program
    {
        static void Main(string[] args)
        {
            // 文字列定数の宣言
            const string Tochigi = "栃木";
            const string Gunma = "群馬";
            const string Other = "その他";

            // 地域の入力
            string m = "次の地域を入力してください[" + Tochigi + "/" + Gunma + "/" + Other + "]";
            Console.WriteLine(m);
            string prefecture = Console.ReadLine();

            // 年齢の入力
            int age = 0;
            Console.WriteLine("年齢を入力して下さい");
            string s = Console.ReadLine();

            // メイン処理
            if (prefecture != Tochigi && prefecture != Gunma && prefecture != Other)
            {
                Console.WriteLine("地域の入力が間違っています。");
            }
            else if (!int.TryParse(s, out age))
            {
                Console.WriteLine("年齢の入力が間違っています。");
            }
            else
            {
                bool b = (prefecture == Tochigi && age >= 18);
                if (b)
                {
                    Console.WriteLine("割引料金です");
                }
                else
                {
                    Console.WriteLine("通常料金です");
                }
            }
            Console.ReadLine();
        }
    }
}
