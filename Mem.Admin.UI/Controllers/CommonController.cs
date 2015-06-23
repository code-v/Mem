using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mem.Framework.Upload;

using Mem.Admin.UI.Models.Common;

namespace Mem.Admin.UI.Controllers
{
    /// <summary>
    /// 公用控制器
    /// </summary>
    public class CommonController : Controller
    {

        #region 对象

        private readonly IUpload _upload;

        #endregion

        #region 构造函数

        public CommonController(IUpload upload)
        {
            this._upload = upload;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 进入上传页面
        /// </summary>
        /// <returns></returns>
        /// <author>谢锐 2014-2-5 00:16</author>
        public ActionResult UploadImage(string ImageUrl)
        {
            ViewBag.Url = ImageUrl;

            return View();
        }

        /// <summary>
        /// 上传图片信息
        /// </summary>
        /// <returns></returns>
        /// <author>谢锐 2014-2-5 02:26</author>
        public ActionResult UploadImageInfo()
        {
            ResultSet resultSet = new ResultSet();

            HttpPostedFileBase fileData = Request.Files[0];

            string ImageUrl = "";

            resultSet.Result = _upload.UploadImage(fileData, out ImageUrl);

            if (resultSet.Result)
            {
                resultSet.data = ImageUrl;
            }

            return Json(resultSet);
        }

        #endregion

    }
}
