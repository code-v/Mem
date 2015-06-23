using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mem.Admin.UI.Models.Settings
{
    /// <summary>
    /// 权限分组模型
    /// </summary>
    public class AuthoritiesClassfyModel
    {
        /// <summary>
        /// 权限分组主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 权限分组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分组模式下的详细权限信息
        /// </summary>
        public List<AuthoritiesModel> authoritiesList { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}