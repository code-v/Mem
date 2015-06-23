using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mem.Admin.UI.Models.Common
{
    /// <summary>
    /// 结果集
    /// </summary>
    public class ResultSet
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public string data { get; set; }
    }
}