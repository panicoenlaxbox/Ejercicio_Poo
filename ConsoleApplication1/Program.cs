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
    }

    internal class CustomersManager
    {
        public IEnumerable<Customer> RetrieveFromFile(string path)
        {
            Customer customer = new Customer();
            customer.Id = "1";
            customer.Name = "Customer 1";
            Customer customer2 = new Customer();
            customer2.Id = "2";
            customer2.Name = "Customer 2";
            List<Customer> customers = new List<Customer>();
            customers.Add(customer);
            customers.Add(customer2);
            return customers;
        }

        public void Print(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("Id {0}, Name {1}", customer.Id, customer.Name);
            }
        }
    }
}
