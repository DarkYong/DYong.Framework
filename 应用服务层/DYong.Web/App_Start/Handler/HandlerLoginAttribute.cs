
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DYong.Code;
using System.Web.Mvc;

namespace DYong.Web.App_Start.Handler
{
    /// <summary>
    /// 登录验证特性
    /// </summary>
    public class HandlerLoginAttribute: AuthorizeAttribute
    {
        public bool Ignore = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ignore"></param>
        public HandlerLoginAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }
        /// <summary>
        /// 重写基类的验证方式
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Ignore == false)
            {
                return;
            }
            if (OperatorProvider.Provider.GetCurrent() == null)
            {
                WebHelper.WriteCookie("login_error", "overdue");
                filterContext.HttpContext.Response.Write("<script>top.location.href = '/Login/Index';</script>");
                return;
            }
        }
    }
}