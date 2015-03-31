using System;
using System.Collections.Generic;
using System.Net.Http;
using ClassLibrary1;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    enum ExecutionType
    {
        File,
        FileRisk,
        WebApi,
        WebApiRisk
    }

    class Program
    {
        static ExecutionType ExecutionType = ExecutionType.WebApiRisk;

        static IReader CreateReader()
        {
            IReader reader = null;
            switch (ExecutionType)
            {
                case ExecutionType.File:
                    reader = new CustomersFileReader("C:\\Temp\\Customers.txt");
                    break;
                case ExecutionType.FileRisk:
                    reader = new CustomersRiskFileReader("C:\\Temp\\CustomersRisk.txt");
                    break;
                case ExecutionType.WebApi:
                    reader = new CustomersWebApi(new Uri("http://localhost:65044/"), "api/Customers");
                    break;
                case ExecutionType.WebApiRisk:
                    reader = new CustomersRiskWebApi(new Uri("http://localhost:65044/"), "api/CustomersRisk");
                    break;
            }
            return reader;
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

    interface IReader
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

    class CustomersWebApi : IReader
    {
        private readonly Uri _uri;
        private readonly string _requestUri;

        public CustomersWebApi(Uri uri, string requestUri)
        {
            _uri = uri;
            _requestUri = requestUri;
        }

        public IEnumerable<Customer> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;
                HttpResponseMessage response = client.GetAsync(_requestUri).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                return customers;
            }
        }
    }

    class CustomersRiskWebApi : IReader
    {
        private readonly Uri _uri;
        private readonly string _requestUri;

        public CustomersRiskWebApi(Uri uri, string requestUri)
        {
            _uri = uri;
            _requestUri = requestUri;
        }

        public IEnumerable<Customer> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;
                HttpResponseMessage response = client.GetAsync(_requestUri).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                List<CustomerRisk> customers = JsonConvert.DeserializeObject<List<CustomerRisk>>(content);
                return customers;
            }
        }
    }
}
