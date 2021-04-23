using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoffeeMachineProject.Entites
{
    public class Store : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public double IngredientTrack { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
