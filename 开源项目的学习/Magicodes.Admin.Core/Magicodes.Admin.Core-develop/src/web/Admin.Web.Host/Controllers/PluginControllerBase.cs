// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : PluginControllerBase.cs
//           description :
// 
//           created by 雪雁 at  2019-06-14 11:22
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Magicodes.Admin.Web.Host.Controllers
{
    public class PluginControllerBase : AbpController
    {
        protected PluginControllerBase(string localizationSourceName)
        {
            LocalizationSourceName = localizationSourceName;
        }

        public string PlusName { get; protected set; }

        /// <summary>
        ///     返回插件视图
        /// </summary>
        /// <returns></returns>
        public new ViewResult View()
        {
            return PluginView(PlusName);
        }

        /// <summary>
        ///     返回插件视图
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public new ViewResult View(string viewName)
        {
            return PluginView(PlusName, viewName);
        }

        /// <summary>
        ///     返回插件视图
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public new ViewResult View(object model)
        {
            return PluginView(PlusName, null, model);
        }

        /// <summary>
        ///     返回插件视图
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public new ViewResult View(string viewName, object model)
        {
            return PluginView(PlusName, viewName, model);
        }

        /// <summary>
        ///     返回插件分布视图
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public new PartialViewResult PartialView(string viewName, object model)
        {
            return PluginPartialView(PlusName, viewName, model);
        }

        /// <summary>
        ///     返回插件分布视图
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public new PartialViewResult PartialView(string viewName)
        {
            return PluginPartialView(PlusName, viewName);
        }

        /// <summary>
        ///     返回插件分布视图
        /// </summary>
        /// <returns></returns>
        public new PartialViewResult PartialView()
        {
            return PluginPartialView(PlusName);
        }

        /// <summary>
        ///     插件视图
        /// </summary>
        /// <param name="plusName">插件短名</param>
        /// <param name="view">视图名称</param>
        /// <param name="model">模型</param>
        /// <returns></returns>
        public virtual ViewResult PluginView(string plusName, string view = null, object model = null)
        {
            var viewPath = view;
            if (string.IsNullOrWhiteSpace(viewPath))
                viewPath = $"{RouteData.Values["controller"]}/{RouteData.Values["action"]}.cshtml";
            else if (viewPath.IndexOf("/", StringComparison.Ordinal) == -1)
                viewPath =
                    $"{RouteData.Values["controller"]}/{view}{(view.EndsWith(".cshtml") ? string.Empty : ".cshtml")}";
            if (!viewPath.StartsWith("~/wwwroot"))
                viewPath = "~/wwwroot/PlugIns/" + plusName + "/Views/" + viewPath.TrimStart('~').TrimStart('/');
            return model == null ? base.View(viewPath) : base.View(viewPath, model);
        }

        /// <summary>
        ///     插件部分视图
        /// </summary>
        /// <param name="plusName">插件短名</param>
        /// <param name="view">视图名称</param>
        /// <param name="model">模型</param>
        /// <returns></returns>
        public virtual PartialViewResult PluginPartialView(string plusName, string view = null, object model = null)
        {
            var viewPath = view;
            if (string.IsNullOrWhiteSpace(viewPath))
                viewPath = $"{RouteData.Values["controller"]}/{RouteData.Values["action"]}.cshtml";
            else if (viewPath.IndexOf("/", StringComparison.Ordinal) == -1)
                viewPath =
                    $"{RouteData.Values["controller"]}/{view}{(view.EndsWith(".cshtml") ? string.Empty : ".cshtml")}";

            if (!viewPath.StartsWith("~/wwwroot"))
                viewPath = "~/wwwroot/PlugIns/" + plusName + "/Views/" + viewPath.TrimStart('~').TrimStart('/');
            return model == null ? base.PartialView(viewPath) : base.PartialView(viewPath, model);
        }
    }
}