using System;
using Common.Support;
using Newtonsoft.Json;

namespace Common.Models
{
    public class Operation
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public string OperationType { get; set; }

        public string Active { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        
        public int AccountNumber { get; set; }
    }
}
