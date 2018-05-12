using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace DYong.WebApi.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeData"></param>
        public SessionableControllerHandler(RouteData routeData) : base(routeData) { }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SessionStateRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new SessionableControllerHandler(requestContext.RouteData);
        }
    }
}