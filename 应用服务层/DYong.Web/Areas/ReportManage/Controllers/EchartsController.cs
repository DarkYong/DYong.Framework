using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYong.Web.Areas.ReportManage.Controllers
{
    public class EchartsController : Controller
    {
        /// <summary>
        /// 折线图
        /// </summary>
        /// <returns></returns>
        public ActionResult LineDemo()
        {
            return View();
        }
        /// <summary>
        /// 柱状图
        /// </summary>
        /// <returns></returns>
        public ActionResult BarDemo()
        {
            return View();
        }
        /// <summary>
        /// 散点图
        /// </summary>
        /// <returns></returns>
        public ActionResult ScatterDemo()
        {
            return View();
        }
        /// <summary>
        /// 地图
        /// </summary>
        /// <returns></returns>
        public ActionResult MapDemo()
        {
            return View();
        }
        /// <summary>
        /// 雷达图
        /// </summary>
        /// <returns></returns>
        public ActionResult RadarDemo()
        {
            return View();
        }
        /// <summary>
        /// 热力图
        /// </summary>
        /// <returns></returns>
        public ActionResult HeatmapDemo()
        {
            return View();
        }
        /// <summary>
        /// 关系树状图
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeDemo()
        {
            return View();
        }

        /// <summary>
        /// 仪表盘
        /// </summary>
        /// <returns></returns>
        public ActionResult GaugeDemo()
        {
            return View();
        }
    }
}