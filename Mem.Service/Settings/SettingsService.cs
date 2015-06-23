using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mem.Core.Data;
using Mem.Core.Domain.Catelog;
using Mem.Core.Domain.Settings;
using Mem.Framework.AuxString;

namespace Mem.Service.Settings
{
    public class SettingsService : ISettingsService
    {
        #region 对象

        private readonly IRepository<Category> _categroyRepository;
        private readonly IRepository<SettingsSiteMenu> _siteMenuReopsitory;
        private readonly IRepository<SettingsAuthorityClassify> _authorityClassifyReopsitory;
        private readonly IRepository<SettingsAuthority> _authorityReopsitory;
        private readonly IAuxString _auxString;

        #endregion

        #region 构造函数

        public SettingsService(IRepository<Category> categroyRepository
                           , IRepository<SettingsSiteMenu> siteMenuReopsitory
                           , IRepository<SettingsAuthorityClassify> authorityClassifyReopsitory
                           , IRepository<SettingsAuthority> authorityReopsitory
                           , IAuxString auxString)
        {
            this._categroyRepository = categroyRepository;
            this._siteMenuReopsitory = siteMenuReopsitory;
            this._authorityClassifyReopsitory = authorityClassifyReopsitory;
            this._authorityReopsitory = authorityReopsitory;
            this._auxString = auxString;
        }

        #endregion


        #region 方法

        #region 菜单

        /// <summary>
        /// 通过父ID得到菜单集合
        /// </summary>
        /// <param name="parentId">父Id</param>
        /// <returns>菜单集合</returns>
        /// <author>谢锐 2014-1-25 22:41</author>
        public List<SettingsSiteMenu> GetSiteMenuByParentId(long parentId)
        {
            var query = from c in _siteMenuReopsitory.GetAll()
                        where c.ParentId == parentId
                        select c;
            return query.ToList();
        }

        /// <summary>
        /// 插入菜单信息
        /// </summary>
        /// <param name="siteMenu">菜单信息</param>
        /// <author>谢锐 2014-2-7 17:37</author>
        public void InsertSiteMenu(SettingsSiteMenu siteMenu)
        {
            if (siteMenu == null)
            {
                throw new ArgumentNullException("siteMenu");
            }

            long id = _siteMenuReopsitory.GetAll().Max(p => p.Id);

            siteMenu.Id = _auxString.GetMaxId(id) + 1;

            _siteMenuReopsitory.Insert(siteMenu);
        }

        /// <summary>
        /// 得到父ID菜单信息，此为下拉列表提供需要有分级处理
        /// </summary>
        /// <param name="parentId">父Id</param>
        /// <returns>菜单信息</returns>
        /// <author>谢锐 2014-2-7 18:50</author>
        public List<SettingsSiteMenu> GetSiteMenuInfo(long parentId)
        {
            //得到根目录下的菜单
            var childSiteMenu = GetSiteMenuByParentId(parentId);

            int Count = childSiteMenu.Count;

            //实例化一个菜单集合
            var allSiteMenu = new List<SettingsSiteMenu>();

            for (int i = 0; i < Count; i++)
            {
                var menuModel = new SettingsSiteMenu();

                var info = new List<SettingsSiteMenu>();

                menuModel.Id = childSiteMenu[i].Id;
                menuModel.ParentId = childSiteMenu[i].ParentId;
                menuModel.Name = childSiteMenu[i].Name;
                menuModel.SrcUrl = childSiteMenu[i].SrcUrl;
                //得到该分类的完整分级信息
                menuModel.Name = GetWholeSiteMenu(menuModel.Id, menuModel.ParentId, menuModel.Name);

                allSiteMenu.Add(menuModel);

                int childCount = GetSiteMenuByParentId(menuModel.Id).Count;

                if (childCount > 0)
                {
                    allSiteMenu.AddRange(GetSiteMenuInfo(menuModel.Id));
                }
            }

            return allSiteMenu;
        }

        /// <summary>
        /// 得到该分类名的完整分类信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parentId">父Id</param>
        /// <param name="name">分类名</param>
        /// <returns></returns>
        /// <author>谢锐 2014-2-7 19:29</author>
        public string GetWholeSiteMenu(long id, long parentId, string name)
        {
            if (parentId == 0)
            {
                return name;
            }

            var siteMenuModel = GetSiteMenuById(parentId);

            name = siteMenuModel.Name + " => " + name;
            parentId = siteMenuModel.ParentId;
            id = siteMenuModel.Id;

            return GetWholeSiteMenu(id, parentId, name);

        }

        /// <summary>
        /// 通过Id得到一条菜单信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>一条菜单信息</returns>
        /// <author>谢锐 2014-2-7 19:33</author>
        public SettingsSiteMenu GetSiteMenuById(long id)
        {
            var siteMenuModel = new SettingsSiteMenu();

            siteMenuModel = _siteMenuReopsitory.GetById(id);

            return siteMenuModel;
        }

        /// <summary>
        /// 删除相应的菜单
        /// </summary>
        /// <param name="ids">需要删除的菜单Id</param>
        /// <author>谢锐 2014-2-11 18:28</author>
        public void DeleteSiteMenuByIds(List<long> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                var siteMenuModel = _siteMenuReopsitory.GetById(ids[i]);

                _siteMenuReopsitory.Delete(siteMenuModel);
            }
        }

        /// <summary>
        /// 更新一条菜单信息
        /// </summary>
        /// <param name="model">菜单信息</param>
        /// <author>谢锐 2014-3-15 00:27</author>
        public void UpdateSiteMenu(SettingsSiteMenu model)
        {
            _siteMenuReopsitory.Update(model);
        }
        #endregion

        #region 权限

        /// <summary>
        /// 插入权限分组信息
        /// </summary>
        /// <param name="siteMenu">权限分组信息</param>
        /// <author>谢锐 2014-4-3 17:37</author>
        public void InsertAuthorityClassify(SettingsAuthorityClassify auClassify)
        {
            if (auClassify == null)
            {
                throw new ArgumentNullException("auClassify");
            }

            long id = _authorityClassifyReopsitory.GetAll().Count() == 0 ? 0 : _authorityClassifyReopsitory.GetAll().Max(p => p.Id);

            auClassify.Id = _auxString.GetMaxId(id) + 1;

            _authorityClassifyReopsitory.Insert(auClassify);
        }

        /// <summary>
        /// 得到所有的权限分组信息
        /// </summary>
        /// <returns>权限信息</returns>
        /// <author>谢锐 2014-4-3 21:14</author>
        public IQueryable<SettingsAuthorityClassify> GetAuthorityClassify()
        {
            var query = _authorityClassifyReopsitory.GetAll();

            return query;
        }

        /// <summary>
        /// 插入一条权限信息
        /// </summary>
        /// <param name="authority">权限信息</param>
        /// <author>谢锐 2014-4-3</author>
        public void InsertAuthority(SettingsAuthority authority)
        {
            if (authority == null)
            {
                throw new ArgumentNullException("authority");
            }

            long id = _authorityReopsitory.GetAll().Count() == 0 ? 0 : _authorityReopsitory.GetAll().Max(p => p.Id);

            authority.Id = _auxString.GetMaxId(id) + 1;

            _authorityReopsitory.Insert(authority);

        }

        /// <summary>
        /// 通过分组Id得到该分组的权限
        /// </summary>
        /// <param name="AuthorityClassifyId">分组Id</param>
        /// <returns>该分组的权限</returns>
        /// <author>谢锐 2014-4-3</author>
        public IQueryable<SettingsAuthority> GetAuthorityByAuthorityClassifyId(long AuthorityClassifyId)
        {
            var query = _authorityReopsitory.GetAll().Where(p => p.AuthorityClassifyId == AuthorityClassifyId);

            return query;
        }

        #endregion

        #endregion
    }
}
