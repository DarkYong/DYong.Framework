
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using DYong.Code;

namespace DYong.Web.App_Start.Handler 
{
    /// <summary>
    /// 路由控制器基类
    /// </summary>
    [HandlerLogin]
    public class ControllerBase:Controller
    {     
        /// <summary>
        /// 跳转到主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 跳转到添加/编辑页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 跳转到详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Details()
        {
            return View();
        }


        /// <summary>
        /// 获得日志信息
        /// </summary>
        public Log FileLog
        {
            get { return LogFactory.GetLogger(this.GetType().ToString()); }
        }
        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}