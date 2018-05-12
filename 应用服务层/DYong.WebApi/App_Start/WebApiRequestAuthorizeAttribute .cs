using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DYong.WebApi.App_Start
{
    /// <summary>
    /// WebApi 接口验证类
    /// </summary>
    public class WebApiRequestAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 重写基类的验证方式
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {     
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);//判断接口是否需要验证
            if (!isAnonymous)//定义的接口需要验证
            {
                var authorization = actionContext.Request.Headers.Authorization;//获得身份证信息
                if (authorization != null)
                {
                    var encryptTicket = authorization.ToString();
                    if (ValidateTicket(encryptTicket))
                    {
                        base.IsAuthorized(actionContext);
                    }
                    else
                    {
                        HandleUnauthorizedRequest(actionContext);
                    }
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else //定义的接口不需要进行验证
            {
                base.OnAuthorization(actionContext);
            }
            #region 旧的用户自定义验证
            ////从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            //var authorization = actionContext.Request.Headers.Authorization;
            //if (authorization != null)
            //{
            //    //获得接口头部身份验证信息
            //    var encryptTicket = authorization.ToString();
            //    if (ValidateTicket(encryptTicket))
            //    {
            //        base.IsAuthorized(actionContext);
            //    }
            //    else
            //    {
            //        HandleUnauthorizedRequest(actionContext);
            //    }
            //}
            ////如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            //else
            //{
            //    var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            //    bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
            //    if (isAnonymous) base.OnAuthorization(actionContext);
            //    else HandleUnauthorizedRequest(actionContext);
            //}
            #endregion
        }

        /// <summary>
        /// 校验用户名密码（正式环境中应该是数据库校验）
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        private bool ValidateTicket(string encryptTicket)
        {         
            var strTicket = encryptTicket;
            if (HttpContext.Current.Session[strTicket] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重写HandleUnauthorizedRequest返回错误信息
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();

            response.StatusCode = HttpStatusCode.Forbidden;//定义错误码
            response.ReasonPhrase = "This Tocken is Error";//定义错误码对应信息
            response.Content = new StringContent("Tocken值错误");//body 显示错误内容
            
        }
    }
}