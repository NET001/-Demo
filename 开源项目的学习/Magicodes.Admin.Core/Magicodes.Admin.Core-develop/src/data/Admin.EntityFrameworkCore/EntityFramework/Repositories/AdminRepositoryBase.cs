// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : AdminRepositoryBase.cs
//           description :
// 
//           created by 雪雁 at  2019-06-14 11:22
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace Magicodes.Admin.EntityFrameworkCore.EntityFramework.Repositories
{
    /// <summary>
    ///     Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class
        AdminRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<AdminDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected AdminRepositoryBase(IDbContextProvider<AdminDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //add your common methods for all repositories
    }

    /// <summary>
    ///     Base class for custom repositories of the application.
    ///     This is a shortcut of <see cref="AdminRepositoryBase{TEntity,TPrimaryKey}" /> for <see cref="int" /> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class AdminRepositoryBase<TEntity> : AdminRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected AdminRepositoryBase(IDbContextProvider<AdminDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //do not add any method here, add to the class above (since this inherits it)!!!
    }
}