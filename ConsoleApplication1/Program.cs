using System;
using System.Collections.Generic;
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
        static CustomersType CustomersType = CustomersType.Normal;

        static CustomersFileReaderBase CreateReader()
        {
            CustomersFileReaderBase reader = null;
            switch (CustomersType)
            {
                case CustomersType.Normal:
                    reader = new CustomersFileReader();
                    break;
                case CustomersType.Risk:
                    reader = new CustomersRiskFileReader();
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
            CustomersFileReaderBase reader = CreateReader();
            string path = GetPath();
            IEnumerable<Customer> customers = reader.Read(path);
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

    abstract class CustomersFileReaderBase
    {
        public IEnumerable<Customer> Read(string path)
        {
            IEnumerable<string> lines = GetLines(path);
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
