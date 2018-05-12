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
    /// IP过滤管理控制器
    /// </summary>
    public class FilterIPController : App_Start.Handler.ControllerBase
    {
        private FilterIPApp filterIPApp = new FilterIPApp();
        /// <summary>
        /// 获得IP过滤信息列表【全部】
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllGridJson(string keyword)
        {
            var data = filterIPApp.GetList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获得IP过滤信息列表【分页】
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = filterIPApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获得过滤IP详细信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = filterIPApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
    }
}