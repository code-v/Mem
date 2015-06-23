using System.Linq;
using System.Collections.Generic;
//创建时间： 2013-9-26
//创建人：谢锐
//描述： 数据仓库的应用接口
namespace Mem.Core.Data
{
    /// <summary>
    /// Repository
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> FindPageList(int pageIndex, int pageSize, out int totalRecord);
    }
}
