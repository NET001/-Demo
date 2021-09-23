using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);

        T Get(string Id);
    }
}
