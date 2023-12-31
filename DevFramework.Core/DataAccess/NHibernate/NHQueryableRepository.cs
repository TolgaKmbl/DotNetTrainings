﻿using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NHQueryableRepository<T> : IQueryableRepository<T>
        where T : class, IEntity, new()
    {

        private readonly NHibernateHelper _nHibernateHelper;
        private IQueryable<T> entities;

        public NHQueryableRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public IQueryable<T> Table => this.Entities;

        public virtual IQueryable<T> Entities
        {
            get 
            {
                return entities ??(entities=_nHibernateHelper.OpenSession().Query<T>());
            }
        }
    }
}
