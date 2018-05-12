using System.Web;
using System.Web.Mvc;

namespace DYong.WebApi
{
    /// <summary>
    /// 过滤器注册类
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
