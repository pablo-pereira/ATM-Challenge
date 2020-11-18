using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ATM.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM.DAL
{
    public class OperationsDAL : IRepository<Operations>
    {
        private ATMContext db = new ATMContext();

       
        public IEnumerable<Operations> Get(Expression<Func<Operations, bool>> filter = null)
        {
            if (filter != null)
            {
                return db.Operations.Where(filter).ToList();
            }
            return db.Operations.ToList();
        }

        public Operations GetByID(object id)
        {
            return db.Operations.Find(id);
        }

        public void Add(Operations entityToSave)
        {
            db.Operations.Add(entityToSave);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Operations entityToUpdate)
        {
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
