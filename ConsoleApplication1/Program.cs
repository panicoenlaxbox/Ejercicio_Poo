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
        public string Id { get; set; }
        public string Name { get; set; }

        public Customer(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("Id {0}, Name {1}", Id, Name);
        }
    }

    internal class CustomersManager
    {
        public IEnumerable<Customer> RetrieveFromFile(string path)
        {
            var customer = new Customer("1", "Customer 1");
            var customer2 = new Customer("2", "Customer 2");
            return new List<Customer> { customer, customer2 };
        }

        public void Print(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }
    }
}
