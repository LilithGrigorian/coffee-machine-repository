using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoffeeMachineProject.Entites
{
    public class Coffee : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PortionId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
