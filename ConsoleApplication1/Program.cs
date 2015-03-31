﻿using System;
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
            var reader = new CustomersFileReader();
            string path = @"C:\Temp\CustomersRisk.txt";
            IEnumerable<Customer> customers = reader.Read(path);
            var printer = new CustomersPrinter();
            printer.Print(customers);
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

        protected string GetTextRepresentation()
        {
            return string.Format("Id {0}, Name {1}", Id, Name);
        }

        public override string ToString()
        {
            return GetTextRepresentation();
        }
    }

    class CustomerRisk : Customer
    {
        public int Risk { get; set; }
        public CustomerRisk(string id, string name, int risk)
            : base(id, name)
        {
            Risk = risk;
        }

        public override string ToString()
        {
            return string.Format("{0}, Risk {1}", GetTextRepresentation(), Risk);
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

    class CustomersFileReader
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

        private Customer GetCustomerFromLine(string line)
        {
            string[] values = line.Split(new[] { ',' });
            string id = values[0];
            string name = values[1];
            if (values.Length == 2)
            {
                return new Customer(id, name);
            }
            else
            {
                int risk = int.Parse(values[2]);
                return new CustomerRisk(id, name, risk);
            }
        }
    }
}
