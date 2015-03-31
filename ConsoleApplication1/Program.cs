using System;
using System.Collections.Generic;
using System.IO;
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
            string path =  @"C:\Temp\Customers.txt";
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
            IEnumerable<string> lines = GetLinesFromFile(path);
            var customers = new List<Customer>();
            foreach (var line in lines)
            {
                customers.Add(GetCustomerFromText(line));
            }
            return customers;
        }

        private IEnumerable<string> GetLinesFromFile(string path)
        {
            List<string> lines = new List<string>();
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(path, FileMode.Open);
                sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());    
                }
                return lines;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
                if (sr != null)
                {
                    sr.Dispose();
                }
            }
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
