using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddClassLibrary
{
    internal static class MainModel
    {
        public static IEnumerable<Customer> GetItems(string keyword)
        {
            //return new List<Customer>();

            // 接続をオープンする。接続したDBについての情報はAddDbContext dbにセットする
            using (AddDbContext db = new AddDbContext())
            {
                // SQLを構築する
                var q =
                    from p in db.Customers
                    where p.Name.Contains(keyword)
                    select p;

                // SQLを実行し、１件ごとに処理する
                foreach (Customer x in q)
                {
                    // yieldは、一回呼ばれるごとにイテレータを１件づつ進ませる。
                    yield return x;
                }
            }
        }

        public static Customer AddItem(Customer model)
        {
            //return new Customer();

            // 接続をオープンする。
            using (AddDbContext db = new AddDbContext())
            {
                // modelに設定された内容をCustomerに追加する。
                db.Customers.Add(model);

                // Addの場合は、登録の瞬間のみ楽観排他がかかっていればOK。
                // SaveChange()メソッドがレコード行バージョンを使って、
                //自動的に楽観排他をかけてくれる。
                // レコード行バージョンは、DBContext作成時の行バージョンを保持しておいて、
                //SaveChange時の行バージョンと比べてくれる。
                db.SaveChanges();
                return model;
            }
        }

        public static Customer UpdateItem(Customer model)
        {
            //return new Customer();

            // 接続をオープンする。
            using (AddDbContext db = new AddDbContext())
            {
                // 最新の対象データを取得のうえ、楽観排他のチェックを行う。
                // 更新、削除時の楽観排他は、SaveChangesとは別に実施の必要がある。
                // なぜならば、db.SaveChangesは、接続時を楽観排他の起点に取るが、
                // 更新・削除の楽観排他は画面表示時（前回接続時）が楽観排他の起点となるため。
                Customer r = GetFirstRecord(db, model);

                // modelのデータで更新する
                r.Name = model.Name;
                r.Kana = model.Kana;
                r.PostCode = model.PostCode;
                r.Address = model.Address;
                r.TelNumber = model.TelNumber;
                r.Memo = model.Memo;
                db.SaveChanges();
                return r;
            }
        }

        public static Customer DeleteItem(Customer model)
        {
            //return new Customer();

            // 接続をオープンする。
            using (AddDbContext db = new AddDbContext())
            {
                // 最新の対象データを取得のうえ、楽観排他のチェックを行う。
                Customer r = GetFirstRecord(db, model);

                // 削除して、変更を保存する。
                db.Customers.Remove(r);
                db.SaveChanges();
                return null;
            }
        }

        private static Customer GetFirstRecord(AddDbContext db, Customer model)
        {
            // SQLを構築する
            var q =
                from p in db.Customers
                where p.Id == model.Id
                select p;

            // SQLを実行し、１件目（もしくはデフォルト値）を取得
            //qにはCustomerのデータが入る
            Customer r = q.FirstOrDefault();
            if (r == null)
            {
                // rがnull(参照型のデフォルト値)ならDB未保存
                throw new Exception($"Id[{0}]は、保存されていません。");
            }

            // rが存在する場合に、検索結果（DBの最新）と引数（参照画面描画時）のタイムスタンプを比較し、
            //違ったら例外
            if (IsChanged(r, model))
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateConcurrencyException();
            }
            return r;
        }

        // byte配列の比較。配列の中に1つでも差分がある場合はfalseを返す
        private static bool IsChanged(Customer a, Customer b)
        {
            byte[] t1 = a.TimeStamp;
            byte[] t2 = b.TimeStamp;
            return t1.Except(t2).Any();
        }

    }
}
