using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mem.Admin.UI.Models.Settings
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public class SiteMenuModel
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单说明
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 菜单图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 链接Url
        /// </summary>
        public string SrcUrl { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 下拉菜单集合
        /// </summary>
        public List<DropDownSiteMenu> DDSiteMenuList { get; set; }

    }

    /// <summary>
    /// 下拉菜单模型
    /// </summary>
    public class DropDownSiteMenu
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }
    }
}