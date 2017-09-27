using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddClassLibrary
{
    /// <summary>
    /// DB接続の環境設定プロパティを用意する。
    /// 
    /// 当クラスのメンバ変数 Customersが、
    /// データベースのCustomersテーブルにアクセスするオブジェクトとして、
    /// MainModelの各メソッドから呼ばれる。
    /// </summary>
    internal class AddDbContext : DbContext
    {
        public static string DebugConnectionString { get; set; }
        public static string ReleaseConnectionString { get; set; }
#if DEBUG
        public AddDbContext() : base(DebugConnectionString) { }
#else
        public AddDbContext() : base(ReleaseConnectionString) { }
#endif
        public DbSet<Customer> Customers { get; set; }
    }
}
