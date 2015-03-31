using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1;

namespace ConsoleApplication1
{
    enum CustomersType
    {
        Normal,
        Risk
    }

    class Program
    {
        static CustomersType CustomersType = CustomersType.Risk;

        static IReader CreateReader()
        {
            IReader reader = null;
            var path = GetPath();
            switch (CustomersType)
            {
                case CustomersType.Normal:
                    reader = new CustomersFileReader(path);
                    break;
                case CustomersType.Risk:
                    reader = new CustomersRiskFileReader(path);
                    break;
            }
            return reader;
        }

        static string GetPath()
        {
            var path = "";
            switch (CustomersType)
            {
                case CustomersType.Normal:
                    path = "C:\\Temp\\Customers.txt";
                    break;
                case CustomersType.Risk:
                    path = "C:\\Temp\\CustomersRisk.txt";
                    break;
            }
            return path;
        }

        static void Main(string[] args)
        {
            IReader reader = CreateReader();
            IEnumerable<Customer> customers = reader.Read();
            var printer = new CustomersPrinter();
            printer.Print(customers);
            Console.ReadKey();
        }
    }

    class CustomersPrinter
    {
        public void Print(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }
    }

    internal interface IReader
    {
        IEnumerable<Customer> Read();
    }

    abstract class CustomersFileReaderBase : IReader
    {
        private readonly string _path;

        public CustomersFileReaderBase(string path)
        {
            _path = path;
        }

        public IEnumerable<Customer> Read()
        {
            IEnumerable<string> lines = GetLines(_path);
            var customers = new List<Customer>();
            foreach (var line in lines)
            {
                customers.Add(GetCustomerFromLine(line));
            }
            return customers;
        }

        private IEnumerable<string> GetLines(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }

        abstract protected Customer GetCustomerFromLine(string line);
    }

    class CustomersFileReader : CustomersFileReaderBase
    {
        public CustomersFileReader(string path)
            : base(path)
        {

        }
        protected override Customer GetCustomerFromLine(string line)
        {
            string[] values = line.Split(new[] { ',' });
            string id = values[0];
            string name = values[1];
            return new Customer(id, name);
        }
    }

    class CustomersRiskFileReader : CustomersFileReaderBase
    {
        public CustomersRiskFileReader(string path)
            : base(path)
        {

        }
        protected override Customer GetCustomerFromLine(string line)
        {
            string[] values = line.Split(new[] { ',' });
            string id = values[0];
            string name = values[1];
            int risk = int.Parse(values[2]);
            return new CustomerRisk(id, name, risk);
        }
    }
}
