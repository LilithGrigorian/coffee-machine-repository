using CoffeeMachineProject.Data;
using CoffeeMachineProject.Entites;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CoffeeMachineProject.Models;
using CoffeeMachineProject.Services;
using System.Threading;

namespace CoffeeMachineProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CoffeeMachineWork work = new CoffeeMachineWork())
            {
                work.Start();

            }
        }
    }
}
