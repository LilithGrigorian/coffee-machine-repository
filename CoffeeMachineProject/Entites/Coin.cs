using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoffeeMachineProject.Entites
{
    public class Coin : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
