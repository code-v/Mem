using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace Mem.Framework.AuxString
{
    /// <summary>
    /// 字符串处理的相关辅助类
    /// </summary>
    public class AuxString : IAuxString
    {
        /// <summary>
        /// 将信息转换成Json
        /// </summary>
        /// <param name="obj">信息集合</param>
        /// <returns>json</returns>
        /// <author>谢锐 2014-1-26 03:48</author>
        public string ObjectToJson(object obj)
        {
            string json = "";

            json = JsonConvert.SerializeObject(obj);

            return json;
        }

        /// <summary>
        /// 处理主键ID
        /// </summary>
        /// <param name="id">Id值，如何还没有Id值，则初始Id为yyyyMMdd00000000</param>
        /// <returns>初始Id</returns>
        /// <author>谢锐 2014-3-26 02:05</author>
        public long GetMaxId(long id)
        {
            if (id == 0)
            {
                id = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd")) * 100000000;
            }

            return id;
        }
    }
}
