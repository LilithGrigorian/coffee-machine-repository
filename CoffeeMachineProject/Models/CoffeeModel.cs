using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineProject.Models
{
    public class CoffeeModel
    {
        public int Id { get; set; }
        public int PortionId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PortionModel Portion { get; set; }
    }
}
