using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineProject.Models
{
    public class PortionModel
    {
        public int Id { get; set; }
        public double WaterGram { get; set; }
        public double CoffeeBeansGram { get; set; }
        public double SugarGram { get; set; }
    }
}
