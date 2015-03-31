using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomersManager manager = new CustomersManager();
            string path = "";
            IEnumerable<Customer> customers = manager.RetrieveFromFile(path);
            manager.Print(customers);
            Console.ReadKey();
        }
    }

    internal class Customer
    {
    }

    internal class CustomersManager
    {
        public IEnumerable<Customer> RetrieveFromFile(string path)
        {
            return Enumerable.Empty<Customer>();
        }

        public void Print(IEnumerable<Customer> customers)
        {
        }
    }
}
