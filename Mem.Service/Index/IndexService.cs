using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mem.Core.Data;
using Mem.Core.Domain.Catelog;
using Mem.Core.Domain.Settings;
namespace Mem.Service.Index
{
    public class IndexService : IIndexService
    {
        #region 对象
       
        private IRepository<Category> _categroyRepository;
        private IRepository<SettingsSiteMenu> _siteMenuReopsitory;
       
        #endregion

        #region 构造函数
        
        public IndexService(IRepository<Category> categroyRepository
                           ,IRepository<SettingsSiteMenu> siteMenuReopsitory)
        {
            this._categroyRepository = categroyRepository;
            this._siteMenuReopsitory = siteMenuReopsitory;
        }
        
        #endregion

        #region 方法
        public string GetTestName()
        {
            //int i = 0;
           // var query = _testRepository.Table;
            //Insert(p);
            var cat = new Category();
            cat.Name = "11";
            cat.TestName = "22";
            cat.TwoName = "33";
            _categroyRepository.Insert(cat);
            return "群体注入成功";
        }

        public List<Category> GetTestTable()
        {
            return _categroyRepository.GetAll().ToList();
        }

        #endregion

    }
}
