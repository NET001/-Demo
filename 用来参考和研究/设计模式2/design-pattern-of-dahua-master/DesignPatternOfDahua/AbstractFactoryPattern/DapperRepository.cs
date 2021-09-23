using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace AbstractFactoryPattern
{
    public class DapperRepository<T> : IRepository<T> where T : class
    {
        public DapperRepository()
        {
            this.conn = DbConnectionFactory.CreateDbConnection();
        }

        protected IDbConnection conn { get; private set; }

        public T Get(string Id)
        {
            T t = conn.Get<T>(Id);
            return t;
        }

        public void Insert(T entity)
        {
            conn.Insert<T>(entity);
        }
    }
}
