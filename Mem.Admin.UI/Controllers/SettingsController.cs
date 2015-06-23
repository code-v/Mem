using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mem.Service.Settings;
using Mem.Plugin;
using Mem.Web.Models;
using Mem.Admin.UI.Models.Settings;
using Mem.Framework.AuxString;
using Mem.Core.Domain.Settings;
using Mem.Admin.UI.Models.Common;

namespace Mem.Admin.UI.Controllers
{
    public class SettingsController : Controller
    {
        #region 对象

        private readonly ISettingsService _settingsService;
        private readonly IAuxString _auxString;

        #endregion

        #region 构造函数

        public SettingsController(ISettingsService settingsService
                                 , IAuxString auxString)
        {
            this._settingsService = settingsService;
            this._auxString = auxString;
        }

        #endregion

        #region 方法

        #region 菜单

        /// <summary>
        /// 得到头部的菜单
        /// </summary>
        /// <returns>菜单集合</returns>
        /// <author>谢锐 2014-1-26 00:21</author>
        public ActionResult TopMenu()
        {
            var menu = _settingsService.GetSiteMenuByParentId(0);

            var model =
               menu.Select(p =>
               {
                   var siteMenuModel = new SiteMenuModel();
                   siteMenuModel.Id = p.Id;
                   siteMenuModel.Name = p.Name;
                   siteMenuModel.SrcUrl = p.SrcUrl;
                   siteMenuModel.ParentId = p.ParentId;

                   return siteMenuModel;
               });

            return View(model);
        }

        /// <summary>
        /// 通过父Id得到相关的菜单json信息
        /// </summary>
        /// <param name="parentId">父Id</param>
        /// <returns>菜单json字符串</returns>
        /// <author>谢锐 2014-1-26 03:10</author>
        [HttpPost]
        public string GetMenuJsonByParentId(int parentId)
        {
            var menu = _settingsService.GetSiteMenuByParentId(parentId);

            string json = _auxString.ObjectToJson(menu);

            json = "{\"menu\":" + json + "}";

            return json;
        }

        /// <summary>
        /// 进入添加菜单视图
        /// </summary>
        /// <returns>菜单信息</returns>
        /// <author>谢锐 2014-2-7 19:51</author>
        public ActionResult AddSiteMenu()
        {
            var menu = _settingsService.GetSiteMenuInfo(0);

            var model =
              menu.Select(p =>
              {
                  var siteMenuModel = new SiteMenuModel();
                  siteMenuModel.Id = p.Id;
                  siteMenuModel.Name = p.Name;

                  return siteMenuModel;
              });


            return View(model);
        }

        /// <summary>
        /// 添加一条菜单信息
        /// </summary>
        /// <param name="model">菜单信息</param>
        /// <returns>成功信息</returns>
        /// <author>谢锐 2014-2-5 21:55</author>
        [HttpPost]
        public ActionResult AddSiteMenuInfo(SiteMenuModel model)
        {
            ResultSet resultSet = new ResultSet();
            resultSet.Result = false;

            try
            {
                var siteMenuModel = new SettingsSiteMenu();

                siteMenuModel.SrcUrl = model.SrcUrl;
                siteMenuModel.Name = model.Name;
                siteMenuModel.ParentId = model.ParentId;
                siteMenuModel.ImageUrl = model.ImageUrl;
                siteMenuModel.Content = model.Content;

                _settingsService.InsertSiteMenu(siteMenuModel);

                resultSet.Result = true;
                resultSet.msg = "添加成功";
            }
            catch
            {
                resultSet.msg = "添加失败";
            }
            return Json(resultSet);
        }

        /// <summary>
        /// 进入菜单管理页面
        /// </summary>
        /// <returns>所有菜单信息</returns>
        /// <author>谢锐 2014-2-9 01:31</author>
        public ActionResult SiteMenuList()
        {
            var menu = _settingsService.GetSiteMenuInfo(0);

            var model =
             menu.Select(p =>
             {
                 var siteMenuModel = new SiteMenuModel();
                 siteMenuModel.Id = p.Id;
                 siteMenuModel.Name = p.Name;
                 siteMenuModel.SrcUrl = p.SrcUrl;

                 return siteMenuModel;
             });

            return View(model);
        }

        /// <summary>
        /// 删除相应的菜单选项
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns>成功信息</returns>
        /// <author>谢锐 2014-2-11 18:26</author>
        public ActionResult DeleteMenu(long id)
        {
            ResultSet resultSet = new ResultSet();
            resultSet.Result = false;

            try
            {
                var ids = new List<long>();

                ids.Add(id);

                _settingsService.DeleteSiteMenuByIds(ids);

                resultSet.Result = true;
                resultSet.msg = "删除成功";
            }
            catch
            {
                resultSet.msg = "删除失败";
            }

            return Json(resultSet);
        }

        /// <summary>
        /// 进入更新页面
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns>菜单信息</returns>
        /// <author>谢锐 2014-3-14 23:28</author>
        public ActionResult EditSiteMenu(int id)
        {
            var model = new SiteMenuModel();

            var info = _settingsService.GetSiteMenuById(id);

            var menuList = _settingsService.GetSiteMenuInfo(0);

            var ddList = new List<DropDownSiteMenu>();

            //下拉菜单绑定
            foreach (var DDInfo in menuList)
            {
                var ddSiteMenu = new DropDownSiteMenu();

                ddSiteMenu.Id = DDInfo.Id;
                ddSiteMenu.Name = DDInfo.Name;

                ddList.Add(ddSiteMenu);
            }

            model.Id = info.Id;
            model.ImageUrl = info.ImageUrl;
            model.Name = info.Name;
            model.ParentId = info.ParentId;
            model.SrcUrl = info.SrcUrl;
            model.Content = info.Content;
            model.DDSiteMenuList = ddList;

            return View(model);
        }

        /// <summary>
        /// 更新一条菜单信息
        /// </summary>
        /// <param name="model">菜单信息</param>
        /// <returns>成功信息</returns>
        /// <author>谢锐 2014-3-15 00:24</author>
        public ActionResult UpdateSiteMenuInfo(SiteMenuModel model)
        {
            ResultSet resultSet = new ResultSet();

            try
            {


                var settingsSiteMenu = _settingsService.GetSiteMenuById(model.Id);

                settingsSiteMenu.Id = model.Id;
                settingsSiteMenu.Name = model.Name;
                settingsSiteMenu.ParentId = model.ParentId;
                settingsSiteMenu.SrcUrl = model.SrcUrl;
                settingsSiteMenu.ImageUrl = model.ImageUrl;
                settingsSiteMenu.Content = model.Content;

                _settingsService.UpdateSiteMenu(settingsSiteMenu);

                resultSet.Result = true;
                resultSet.msg = "成功执行";
            }
            catch
            {
                resultSet.Result = false;
                resultSet.msg = "执行失败";
            }

            return Json(resultSet);
        }

        #endregion

        #region 用户

        /// <summary>
        /// 进入用户列表
        /// </summary>
        /// <returns>用户信息</returns>
        /// <author>谢锐 2014-3-27</author>
        public ActionResult UsersList()
        {
            return View();
        }



        #endregion

        #region 权限

        /// <summary>
        /// 进入权限管理页面
        /// </summary>
        /// <returns>权限信息</returns>
        /// <author>谢锐 2014-3-27</author>
        public ActionResult AuthoritiesManage()
        {
            //加载权限分组
            var auClassfyInfo = _settingsService.GetAuthorityClassify().ToList();

            var auList =
            auClassfyInfo.Select(p =>
            {
                var auClassfy = new AuthoritiesClassfyModel();
                auClassfy.Id = p.Id;
                auClassfy.Name = p.Name;

                //取出分组权限信息
                auClassfy.authoritiesList = new List<AuthoritiesModel>();

                var authority = new List<SettingsAuthority>();

                authority = _settingsService.GetAuthorityByAuthorityClassifyId(auClassfy.Id).ToList();

                //循环添加相应的分组数据
                foreach (var info in authority)
                {
                    var authoritiesModel = new AuthoritiesModel();
                    authoritiesModel.Id = info.Id;
                    authoritiesModel.Name = info.Name;
                    authoritiesModel.Action = info.Action;
                    authoritiesModel.Controller = info.Controller;
                    auClassfy.authoritiesList.Add(authoritiesModel);
                }

                return auClassfy;
            });

            return View(auList);
        }

        /// <summary>
        /// 进入添加权限分组页面
        /// </summary>
        /// <returns>权限总体信息</returns>
        /// <author>谢锐 2014-4-3</author>
        public ActionResult AddAuthoritiesClassfy()
        {
            return View();
        }

        /// <summary>
        /// 插入一条权限分组信息
        /// </summary>
        /// <param name="model">权限分组信息</param>
        /// <returns>成功信息</returns>
        /// <author>谢锐 2014-4-3</author>
        [HttpPost]
        public ActionResult AddAuthoritiesClassfyInfo(AuthoritiesClassfyModel model)
        {
            ResultSet resultSet = new ResultSet();
            resultSet.Result = false;

            try
            {
                var authoritiesClassfyModel = new SettingsAuthorityClassify();

                authoritiesClassfyModel.Name = model.Name;

                _settingsService.InsertAuthorityClassify(authoritiesClassfyModel);

                resultSet.Result = true;
                resultSet.msg = "添加成功";
            }
            catch
            {
                resultSet.msg = "添加失败";
            }
            return Json(resultSet);
        }

        /// <summary>
        /// 进入添加权限页面
        /// </summary>
        /// <returns>权限分组信息</returns>
        /// <author>谢锐 2014-4-3</author>
        public ActionResult AddAuthorities()
        {
            //得到所有的权限分组信息
            var model = new AuthoritiesModel();

            var classFy = _settingsService.GetAuthorityClassify().ToList();

            var auList = new List<AuthoritiesClassfyModel>();

            foreach (var info in classFy)
            {
                var authoritiesClassfyModel = new AuthoritiesClassfyModel();

                authoritiesClassfyModel.Id = info.Id;
                authoritiesClassfyModel.Name = info.Name;

                auList.Add(authoritiesClassfyModel);
            }

            model.AuList = auList;

            return View(model);
        }

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model">权限信息</param>
        /// <returns>成功信息</returns>
        /// <author>谢锐 2014-4-3</author>
        [HttpPost]
        public ActionResult AddAuthoritiesInfo(AuthoritiesModel model)
        {
            ResultSet resultSet = new ResultSet();
            resultSet.Result = false;

            try
            {
                var authoritiesModel = new SettingsAuthority();

                authoritiesModel.Name = model.Name;
                authoritiesModel.Controller = model.Controller;
                authoritiesModel.Action = model.Action;
                authoritiesModel.AuthorityClassifyId = model.AuthorityClassifyId;

                _settingsService.InsertAuthority(authoritiesModel);

                resultSet.Result = true;
                resultSet.msg = "添加成功";
            }
            catch
            {
                resultSet.msg = "添加失败";
            }
            return Json(resultSet);
        }

        #endregion

        #endregion
    }
}
