using CoffeeMachineProject.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeMachineProject.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
    {
        private ApplicationDBContext context;
        private DbSet<T> entity;

        public GenericRepository(ApplicationDBContext context)
        {
            this.context = context;
            entity = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }

        public T GetById(object id)
        {
            return entity.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            entity.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
       
    }
}
