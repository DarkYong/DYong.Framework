using DYong.Business.SystemManage;
using DYong.Code;
using DYong.Entity.Entitys.SystemManage;
using DYong.Web.App_Start.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYong.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 数据字典控制器
    /// </summary>
    public class ItemsDataController : App_Start.Handler.ControllerBase
    {
        private ItemsDetailApp itemsDetailApp = new ItemsDetailApp();
        /// <summary>
        /// 获得数据字典列表数据
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllGridJson(string itemId, string keyword)
        {
            var data = itemsDetailApp.GetList(itemId, keyword);
            string result = data.ToJson();
            return Content(result);
        }

        /// <summary>
        /// 获得数据字典列表数据
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination,string itemId, string keyword)
        {
            var data = itemsDetailApp.GetList(pagination,itemId, keyword);
            string result = data.ToJson();
            return Content(result);
        }
        /// <summary>
        /// 获得数据字典下拉选择树状列表
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (ItemsDetailEntity item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 操作数据字典信息
        /// </summary>
        /// <param name="itemsDetailEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            try
            {
                itemsDetailApp.SubmitForm(itemsDetailEntity, keyValue);
                return Success("操作成功。");
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }       
        }
        /// <summary>
        /// 获得数据字典详细信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除数据字典信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            try
            {
                itemsDetailApp.DeleteForm(keyValue);
                return Success("删除成功。");
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }         
        }
    }
}