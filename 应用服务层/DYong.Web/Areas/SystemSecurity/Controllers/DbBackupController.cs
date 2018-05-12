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
    /// 数据库备份管理控制器
    /// </summary>
    public class DbBackupController : App_Start.Handler.ControllerBase
    {
        private DbBackupApp dbBackupApp = new DbBackupApp();
        /// <summary>
        /// 获得数据库备份列表信息【全部】
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllGridJson(string queryJson)
        {
            var data = dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获得数据库备份列表信息【部分】
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = dbBackupApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}