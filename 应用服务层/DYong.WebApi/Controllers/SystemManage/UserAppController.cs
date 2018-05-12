using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using DYong.Data.Contract;
using DYong.Entity.Entitys.SystemManage;
using DYong.Business.SystemManage;
using System.Web.Http.Cors;
using System.Web.Security;

namespace DYong.WebApi.Controllers.SystemManage
{
    /// <summary>
    /// 用户信息管理接口
    /// </summary>
    [App_Start.WebApiRequestAuthorize]//头部身份验证【只能应用于控制器类中，方法中只能用[AllowAnonymous]】
    [App_Start.WebApiExceptionFilter]//异常抛出捕抓【控制器和方法都能使用】
    [EnableCors(origins: "*", headers: "*", methods: "GET,POST,PUT,DELETE")]//跨域访问设置
    public class UserAppController : ApiController
    {
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="username">用户账号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ResultClass<UserEntity> CheckLogin(string username, string password)
        {
            ResultClass<UserEntity> _ret = new ResultClass<UserEntity>();
            UserApp bll = new UserApp();
            try
            {
                _ret.ResultData = bll.CheckLogin(username, password);
                if (_ret.ResultData != null)
                {
                    _ret.Result = true;
                    #region 生成且保存用户票据
                    _ret.ErrorMessage = "20171225"; //System.DateTime.Now.ToString("yyyyMMddHHmmss");//把票据返回给前端
                    HttpContext.Current.Session[_ret.ErrorMessage] = _ret.ResultData;//保存票据信息
                    #endregion
                }
            }
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            finally
            {
                bll.Dispose();
            }
            return _ret;
        }

        /// <summary>
        /// 权限验证接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultClass<int> CheckAuthorize()
        {
            ResultClass<int> _ret = new ResultClass<int>();
            _ret.Result = true;
            return _ret;
        }
        /// <summary>
        /// 异常接口验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ResultClass<int> CheckException()
        {
            throw new Exception("404");
        }
    }
}