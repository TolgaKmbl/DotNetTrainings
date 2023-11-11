using DevFramework.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EFQueryableRepository<T> : IQueryableRepository<T>
        where T : class, IEntity, new()
    {

        private readonly DbContext _context;
        private IDbSet<T> _dbSet;

        public EFQueryableRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table => this.DbSet;

        protected virtual IDbSet<T> DbSet
        {
            get
            {
                return _dbSet ?? (_dbSet=_context.Set<T>());
            }
        }
    }
}
