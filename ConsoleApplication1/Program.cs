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

    class Customer
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

    class CustomersManager
    {
        public IEnumerable<Customer> RetrieveFromFile(string path)
        {
            string text = "1,Customer 1";
            var customer = GetCustomerFromText(text);
            var customer2 = GetCustomerFromText(text);
            return new List<Customer> { customer, customer2 };
        }

        private Customer GetCustomerFromText(string text)
        {
            string[] values = text.Split(new[] { ',' });
            string id = values[0];
            string name = values[1];
            return new Customer(id, name);
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
