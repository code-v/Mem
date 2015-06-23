using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mem.Admin.UI.Models.Settings
{
    /// <summary>
    /// 权限模型
    /// </summary>
    public class AuthoritiesModel
    {
        /// <summary>
        /// 权限主键
        /// </summary>
        public long Id { get; set; }

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
        /// 下拉列表
        /// </summary>
        public List<AuthoritiesClassfyModel> AuList { get; set; }

    }
}