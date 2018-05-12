using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Web.Mvc;

namespace DYong.Web.App_Start.Handler
{
    /// <summary>
    /// Ajax请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HandlerAjaxOnlyAttribute: ActionMethodSelectorAttribute
    {
        public bool Ignore { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ignore"></param>
        public HandlerAjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }
        /// <summary>
        /// 重写基类的验证方式
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            if (Ignore) return true;
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}