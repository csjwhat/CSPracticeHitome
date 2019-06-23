using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddClassLibraries
{
    static class MainModel
    {
        public static IEnumerable<Customer> GetItems(string keyword)
        {
            return new List<Customer>();
        }

        public static Customer AddItem(Customer model)
        {
            return new Customer();
        }

        public static Customer UpdateItem(Customer model)
        {
            return new Customer();
        }

        public static Customer DeleteItem(Customer model)
        {
            return new Customer();
        }
    }
}

