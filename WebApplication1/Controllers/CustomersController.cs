using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassLibrary1;

namespace WebApplication1.Controllers
{
    public class CustomersController : ApiController
    {
        public IEnumerable<Customer> Get()
        {
            return new List<Customer>()
            {
                new Customer("1", "Customer 1"),
                new Customer("1", "Customer 2")
            };
        }
    }

    public class CustomersRiskController : ApiController
    {
        public IEnumerable<CustomerRisk> Get()
        {
            return new List<CustomerRisk>()
            {
                new CustomerRisk("1", "Customer 1", 100),
                new CustomerRisk("1", "Customer 2", 1500)
            };
        }
    }
}
