using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mem.Core;

namespace Mem.Framework.AuxString
{
    public interface IAuxString : IDependency
    {
        /// <summary>
        /// 将信息转换成Json
        /// </summary>
        /// <param name="obj">信息集合</param>
        /// <returns>json</returns>
        /// <author>谢锐 2014-1-26 03:48</author>
        string ObjectToJson(object obj);

        /// <summary>
        /// 处理主键ID
        /// </summary>
        /// <param name="id">Id值，如何还没有Id值，则初始Id为yyyyMMdd00000000</param>
        /// <returns>初始Id</returns>
        /// <author>谢锐 2014-3-26 02:05</author>
        long GetMaxId(long id);
    }
}
