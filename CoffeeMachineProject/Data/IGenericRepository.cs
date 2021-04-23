using CoffeeMachineProject.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachineProject.Data
{
    public interface IGenericRepository<T> where T : class, IBaseEntity
    {
        void Update(T obj);
        void Save();
        T GetById(object id);
        IEnumerable<T> GetAll();
    }
}
