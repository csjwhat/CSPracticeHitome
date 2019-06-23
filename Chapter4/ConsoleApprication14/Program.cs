using System;
using System.Collections.Generic;

namespace ConsoleApprication14
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> sushi = new List<string>();
            sushi.Add("赤身");
            sushi.Add("中とろ");
            sushi.Add("大トロ");
            Console.WriteLine(sushi[1]);

            Dictionary<string, int> price = new Dictionary<string, int>();
            price.Add("白", 100);
            price.Add("赤", 200);
            price.Add("青", 300);
            price.Add("銀", 400);
            price.Add("金", 500);

            Console.WriteLine("金の皿は、" + price["金"] + "円です");
            Console.ReadLine();
        }
    }
}
