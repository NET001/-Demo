using Blog.Core_CJL.Model;
using Blog.Core_CJL.Model.MessageCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Api.Controllers
{
    /// <summary>
    /// 控制器基础类用于实现api控制器中特意的业务功能,继承MVC控制器的父类
    /// </summary>
    public class BaseApiCpntroller : Controller
    {
        /*
         NonAction指定方法不为控制器中具体的行为只为类中的方法忽略路由的映射
         此基类控制器封装了返回类型的格式
         */
        [NonAction]
        public MessageModel<T> Success<T>(T data, string msg = "成功")
        {
            return new MessageModel<T>()
            {
                success = true,
                msg = msg,
                response = data,
            };
        }
        [NonAction]
        public MessageModel Success(string msg = "成功")
        {
            return new MessageModel()
            {
                success = true,
                msg = msg,
                response = null,
            };
        }
        [NonAction]
        public MessageModel<string> Failed(string msg = "失败", int status = 500)
        {
            return new MessageModel<string>()
            {
                success = false,
                status = status,
                msg = msg,
                response = null,
            };
        }
        [NonAction]
        public MessageModel<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new MessageModel<T>()
            {
                success = false,
                status = status,
                msg = msg,
                response = default,
            };
        }
        [NonAction]
        public MessageModel<PageModel<T>> SuccessPage<T>(int page, int dataCount, List<T> data, int pageCount, string msg = "获取成功")
        {

            return new MessageModel<PageModel<T>>()
            {
                success = true,
                msg = msg,
                response = new PageModel<T>()
                {
                    page = page,
                    dataCount = dataCount,
                    data = data,
                    pageCount = pageCount,
                }
            };
        }
        [NonAction]
        public MessageModel<PageModel<T>> SuccessPage<T>(PageModel<T> pageModel, string msg = "获取成功")
        {

            return new MessageModel<PageModel<T>>()
            {
                success = true,
                msg = msg,
                response = new PageModel<T>()
                {
                    page = pageModel.page,
                    dataCount = pageModel.dataCount,
                    data = pageModel.data,
                    pageCount = pageModel.pageCount,
                }
            };
        }
    }
}
