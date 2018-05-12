using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace DYong.WebApi
{
    /// <summary>
    /// WebAPI路由注册类
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            //// Web API 配置和服务
            //// 将 Web API 配置为仅使用不记名令牌身份验证。
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            //跨域配置
            var allowerOrign = System.Configuration.ConfigurationManager.AppSettings["cors.allowerdOrigin"];
            var allowerHeader = System.Configuration.ConfigurationManager.AppSettings["cors.allowerHeaders"];
            var allowerMethods = System.Configuration.ConfigurationManager.AppSettings["cors.allowerMethods"];
            config.EnableCors(new EnableCorsAttribute(allowerOrign, allowerHeader, allowerMethods));


            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
