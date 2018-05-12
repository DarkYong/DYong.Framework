using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DYong.Web.App_Start.Handler;

namespace DYong.Web.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    [HandlerLogin]
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页面跳转
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 默认页面跳转
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 关于页面跳转
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}