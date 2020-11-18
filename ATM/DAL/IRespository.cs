using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ATM.DAL
{
    public interface IRepository<TEntity> where TEntity : class    {
        
        public void Add(TEntity entityToSave);
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetByID(object id);
        void Update(TEntity entityToUpdate);
        void Save();

    }
}
