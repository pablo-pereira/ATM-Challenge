using ATM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ATM.DAL
{
    public class CardDAL : IRepository<Cards>
    {
        private ATMContext db = new ATMContext();

        //public Cards GetCardByNumber(string cardNumber)
        //{
        //    return db.Cards.Where(c => c.Number == cardNumber).First();
        //}
        public IEnumerable<Cards> Get(Expression<Func<Cards, bool>> filter = null)
        {
            if (filter != null)
            {
                return db.Cards.Where(filter).ToList();
            }
            return db.Cards.ToList();
        }

        public Cards GetByID(object id)
        {
            return db.Cards.Find(id);
        }

        public void Add(Cards entityToSave)
        {
            db.Cards.Add(entityToSave);                        
        }

        public void Update(Cards entityToUpdate)
        {
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }


    }
}
