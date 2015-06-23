using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mem.Core;
using Mem.Core.Domain.Catelog;
using Mem.Core.Domain.Settings;

namespace Mem.Service.Settings
{
    public interface ISettingsService : IDependency
    {
        /// <summary>
        /// 通过父ID得到菜单集合
        /// </summary>
        /// <param name="parentId">父Id</param>
        /// <returns>菜单集合</returns>
        /// <author>谢锐 2014-1-25 22:41</author>
        List<SettingsSiteMenu> GetSiteMenuByParentId(long parentId);

        /// <summary>
        /// 插入菜单信息
        /// </summary>
        /// <param name="siteMenu">菜单信息</param>
        /// <author>谢锐 2014-2-7 17:37</author>
        void InsertSiteMenu(SettingsSiteMenu siteMenu);

        /// <summary>
        /// 得到父ID菜单信息，此为下拉列表提供需要有分级处理
        /// </summary>
        /// <param name="parentId">父Id</param>
        /// <returns>菜单信息</returns>
        /// <author>谢锐 2014-2-7 18:50</author>
        List<SettingsSiteMenu> GetSiteMenuInfo(long parentId);

        /// <summary>
        /// 通过Id得到一条菜单信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>一条菜单信息</returns>
        /// <author>谢锐 2014-2-7 19:33</author>
        SettingsSiteMenu GetSiteMenuById(long id);


         /// <summary>
        /// 删除相应的菜单
        /// </summary>
        /// <param name="ids">需要删除的菜单Id</param>
        /// <author>谢锐 2014-2-11 18:28</author>
        void DeleteSiteMenuByIds(List<long> ids);

        /// <summary>
        /// 更新一条菜单信息
        /// </summary>
        /// <param name="model">菜单信息</param>
        /// <author>谢锐 2014-3-15 00:27</author>
        void UpdateSiteMenu(SettingsSiteMenu model);

        /// <summary>
        /// 插入权限分组信息
        /// </summary>
        /// <param name="siteMenu">权限分组信息</param>
        /// <author>谢锐 2014-4-3 17:37</author>
        void InsertAuthorityClassify(SettingsAuthorityClassify auClassify);

        /// <summary>
        /// 得到所有的权限分组信息
        /// </summary>
        /// <returns>权限信息</returns>
        /// <author>谢锐 2014-4-3 21:14</author>
        IQueryable<SettingsAuthorityClassify> GetAuthorityClassify();

        /// <summary>
        /// 插入一条权限信息
        /// </summary>
        /// <param name="authority">权限信息</param>
        /// <author>谢锐 2014-4-3</author>
        void InsertAuthority(SettingsAuthority authority);

        /// <summary>
        /// 通过分组Id得到该分组的权限
        /// </summary>
        /// <param name="AuthorityClassifyId">分组Id</param>
        /// <returns>该分组的权限</returns>
        /// <author>谢锐 2014-4-3</author>
        IQueryable<SettingsAuthority> GetAuthorityByAuthorityClassifyId(long AuthorityClassifyId);
    }
}
