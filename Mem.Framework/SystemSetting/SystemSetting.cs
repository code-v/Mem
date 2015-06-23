using System;
using System.Configuration;


namespace Mem.Framework.SystemSetting
{
    /// <summary>
    ///  得到 webconfig 相关参数
    /// </summary>
    public class SystemSetting
    {
        /// <summary>
        /// 单域上传路径
        /// </summary>
        /// <author>谢锐 2014-2-5 15:33</author>
        public static string UploadPhotoPath
        {
            get { return ConfigurationManager.AppSettings["Image.xuexue520.com"]; }
        }

        /// <summary>
        /// 上传图片的机子的登录名
        /// </summary>
        public static string UserName
        {
            get { return ConfigurationManager.AppSettings["UserName"]; }
        }

        /// <summary>
        /// 上传图片的机子的密码
        /// </summary>
        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        /// <summary>
        /// 上传图片的机子工作组名称
        /// </summary>
        public static string ComputerDomain
        {
            get { return ConfigurationManager.AppSettings["Domain"]; }
        }
    }
}
