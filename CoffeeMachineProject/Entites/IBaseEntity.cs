using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineProject.Entites
{
    public interface IBaseEntity
    {
        DateTime ModifiedDate { get; set; }
    }
}
