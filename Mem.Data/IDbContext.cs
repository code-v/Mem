using System;
using System.Collections.Generic;
using System.Data.Entity;
using Mem.Core;
//创建时间：2013-9-26
//创建人：  谢锐
//描述：  实体框架接口
namespace Mem.Data
{
    public interface IDbContext : IDependency
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
