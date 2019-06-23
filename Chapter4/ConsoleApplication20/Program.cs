using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Program
    {
        static void Main(string[] args)
        {
            // 操作盤オブジェクトの作成
            OperationBoard operationBoard = new OperationBoard();
            operationBoard.ControlMachine();
            Console.ReadLine();
        }


        class OperationBoard : PC { };

        public class PC
        {
            public void ControlMachine()
            {
                Console.WriteLine("ここで工場機械本体への命令を出力します");
            }
        }
    }
}
