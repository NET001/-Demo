using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Repository.UnitOfWork
{
    /// <summary>
    /// 事务操作接口
    /// </summary>
    public interface IUnitOfWork
    {
        SqlSugarClient GetDbClient();
        void BeginTran();
        void CommitTran();
        void RollbackTran();
    }
}
