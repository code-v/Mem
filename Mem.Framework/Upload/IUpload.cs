using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Mem.Core;

namespace Mem.Framework.Upload
{
    public interface IUpload :IDependency
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file">图片文件流</param>
        /// <param name="ImageUrl">返回上传图片路径</param>
        /// <returns></returns>
        /// <author>谢锐 2014-2-5 16:40</author>
        bool UploadImage(HttpPostedFileBase file, out string ImageUrl);
    }
}
