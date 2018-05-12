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
    /// 组织管理控制器【集团，公司,部门，小组】
    /// </summary>
    public class OrganizeController : App_Start.Handler.ControllerBase
    {
        private OrganizeApp organizeApp = new OrganizeApp();
        /// <summary>
        /// 获得组织信息列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllTreeGridJson(string keyword)
        {
            var data = organizeApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (OrganizeEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        /// <summary>
        /// 获得组织树状信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string type="")
        {
            var data = organizeApp.GetList();
            if (!string.IsNullOrEmpty(type))
            {
                data = data.TreeWhere(t => t.F_CategoryId.Contains(type));
            }
            var treeList = new List<TreeSelectModel>();
            foreach (OrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            string result = treeList.TreeSelectJson();
            return Content(result);
        }

        /// <summary>
        /// 获得组织信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = organizeApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 操作组织信息
        /// </summary>
        /// <param name="organizeEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            try
            {
                organizeApp.SubmitForm(organizeEntity, keyValue);
                return Success("操作成功。");
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }       
        }
        /// <summary>
        /// 删除组织信息
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
                organizeApp.DeleteForm(keyValue);
                return Success("删除成功。");
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }         
        }
    }
}