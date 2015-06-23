using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Collections.Generic;

using Mem.Core;
using Mem.Core.Data;
using Mem.Data;
//创建时间 ： 2013-9-26
//创建人：  谢锐
//描述：   数据仓库，所有数据操作均由此来实现
namespace Mem.Data
{
    /// <summary>
    /// 实体框架数据仓库
    /// </summary>
    public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private IDbContext Context;

        private IDbSet<TEntity> Entities
        {
            get { return this.Context.Set<TEntity>(); }
        }

        public EfRepository(IDbContext context)
        {
            this.Context = context;

        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities.AsQueryable();
        }

        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
            this.Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
            this.Context.SaveChanges();
        }

        //得到相关的分页信息
        public IQueryable<TEntity> FindPageList(int pageIndex, int pageSize, out int totalRecord)
        {
            totalRecord = 0;
            var list = Entities.AsQueryable().Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalRecord = Entities.AsQueryable().Count(); 
            return list;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
    }
}