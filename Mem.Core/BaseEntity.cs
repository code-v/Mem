using System;
using System.Collections.Generic;
using System.Linq;
//创建时间： 2013-9-26
//创建人： 谢锐
//描述： 实体公共属性
namespace Mem.Core
{
    /// <summary>
    /// 实体基本的类
    /// </summary>
    public class BaseEntity<T>
    {
        /// <summary>
        /// 设置实体标识符 ，所有的表的标示符都是Id，都被Mem.Domain 所继承来集体创建主键
        /// </summary>
        public long Id { get; set; }

    }
}
