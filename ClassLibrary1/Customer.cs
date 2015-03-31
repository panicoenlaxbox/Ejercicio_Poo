using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Customer
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

    public class CustomerRisk : Customer
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
}
