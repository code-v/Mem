using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;

using Mem.Framework.SystemSetting;

namespace Mem.Framework.Upload
{
    public class Upload : IUpload
    {
        private List<string> strCorrectFormat = new List<string>() { "png", "gif", "bmp", "jpg", "jpeg" };

        private readonly Random rn = new Random();

        /// <summary>
        /// 创建新文件名,例如：2012022010594659463085 (yyyyMMddhhmmsfff)
        /// </summary>
        /// <returns></returns>
        public string CreateNewFileName()
        {
            return DateTime.Now.ToString("yyyyMMddhhmms") + rn.Next(0, 999);
        }

        /// <summary>
        /// 可允许的上传后缀名。默认值：gif,jpg,jpeg,png,bmp
        /// </summary>
        public List<string> StrCorrectFormat
        {
            get
            {
                return strCorrectFormat;
            }
            set
            {
                strCorrectFormat = value;
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file">图片文件流</param>
        /// <param name="ImageUrl">返回上传图片路径</param>
        /// <returns></returns>
        /// <author>谢锐 2014-2-5 16:40</author>
        public bool UploadImage(HttpPostedFileBase file, out string ImageUrl)
        {
            ImageUrl = "";
            try
            {
                //得到文件的后缀名
                string ext = Path.GetExtension(file.FileName).TrimStart('.').ToLower();

                string newName = CreateNewFileName() + ".jpg";

                //本地全路径
                string serverFullUrl = HttpContext.Current.Server.MapPath("/Upload/Image");

                //得到年月
                string Date = DateTime.Now.ToString("MM/dd");

                //新文件夹路径
                string NewDirectory = serverFullUrl + "/" + Date;

                //不存在文件夹就创建
                CreateDirectory(NewDirectory);

                file.SaveAs(NewDirectory + "/" + newName);

                ImageUrl = SystemSetting.SystemSetting.UploadPhotoPath + "/" + Date + "/" + newName;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 创建文件夹。
        /// </summary>
        /// <param name="directoryName"></param>
        private void CreateDirectory(string directoryName)
        {
            // 判断上传的目标目录是否存在。不存在则创建。
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }


    }
}
