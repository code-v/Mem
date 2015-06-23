using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mem.Core.Domain.Settings
{
    /// <summary>
    /// 后台菜单
    /// </summary>
    public class SettingsSiteMenu : BaseEntity<long>
    {
        public SettingsSiteMenu()
        {
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 菜单图片Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 指向的路径
        /// </summary>
        public string SrcUrl { get; set; }
    
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
