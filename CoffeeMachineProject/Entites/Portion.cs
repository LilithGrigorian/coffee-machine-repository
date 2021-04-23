using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoffeeMachineProject.Entites
{
    public class Portion : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public double WaterGram { get; set; }
        public double CoffeeBeansGram { get; set; }
        public double SugarGram { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
