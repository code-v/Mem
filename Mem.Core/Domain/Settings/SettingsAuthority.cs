using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mem.Core.Domain.Settings
{
    /// <summary>
    /// 权限表 采用集中权限控制，当路由访问后，验证路由的权限，这样更加方便
    /// </summary>
    public class SettingsAuthority : BaseEntity<long>
    {
        public SettingsAuthority()
        {
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属权限分组
        /// </summary>
        public long AuthorityClassifyId { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
