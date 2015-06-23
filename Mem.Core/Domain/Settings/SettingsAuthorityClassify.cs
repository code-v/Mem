using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mem.Core.Domain.Settings
{
    /// <summary>
    /// 权限分组表
    /// </summary>
    public class SettingsAuthorityClassify : BaseEntity<long>
    {
        public SettingsAuthorityClassify()
        {
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 权限分组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
