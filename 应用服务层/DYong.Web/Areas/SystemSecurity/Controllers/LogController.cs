using DYong.Business.SystemSecurity;
using DYong.Code;
using DYong.Web.App_Start.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYong.Web.Areas.SystemSecurity.Controllers
{
    /// <summary>
    /// 日志操作控制器
    /// </summary>
    public class LogController : App_Start.Handler.ControllerBase
    {
        private LogApp logApp = new LogApp();
        /// <summary>
        /// 跳转到移除页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RemoveLog()
        {
            return View();
        }
        /// <summary>
        /// 获得日志信息列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllGridJson(string queryJson)
        {
             var data = logApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获得日志信息列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = logApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 移除日志信息
        /// </summary>
        /// <param name="keepTime"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRemoveLog(string keepTime)
        {
            try
            {
                logApp.RemoveLog(keepTime);
                return Success("清空成功。");
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }          
        }
    }
}