using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DYong.Data.Contract;
using System.Web;
using System.Web.Http.Cors;
using DYong.Entity.Entitys;
using DYong.Business;

namespace DYong.WebApi.Controllers
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [App_Start.WebApiRequestAuthorize]//头部身份验证【只能应用于控制器类中，方法中只能用[AllowAnonymous]】
    [App_Start.WebApiExceptionFilter]//异常抛出捕抓【控制器和方法都能使用】
    [EnableCors(origins: "*", headers: "*", methods: "GET,POST,PUT,DELETE")]//跨域访问设置
    public class TestController : ApiController
    {
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="username">用户账号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ResultClass<string> CheckLogin(string username, string password)
        {
            ResultClass<string> _ret = new ResultClass<string>();
            try
            {
                _ret.Result = true;
                #region 生成且保存用户票据
                _ret.ResultData = "20171229"; //System.DateTime.Now.ToString("yyyyMMddHHmmss");//把票据返回给前端
                HttpContext.Current.Session[_ret.ResultData] = _ret.ResultData;//保存票据信息
                #endregion
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            finally
            {
            }
            return _ret;
        }

        /// <summary>
        /// 获得信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ResultClass<List<OrderEntity>> GetList()
        {
            ResultClass<List<OrderEntity>> _ret = new ResultClass<List<OrderEntity>>();
            OrderApp bll = new OrderApp();
            try
            {
                _ret.ResultData = bll.GetList();
                _ret.Result = true;
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
        /// 获得一条信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ResultClass<OrderEntity> GetEntity(string keyValue)
        {
            ResultClass<OrderEntity> _ret = new ResultClass<OrderEntity>();
            OrderApp bll = new OrderApp();
            try
            {
                _ret = bll.GetForm(keyValue);
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
        /// 添加一条实体信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultClass<int>AddEntity(OrderEntity entity)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            OrderApp bll = new OrderApp();
            try
            {
                _ret = bll.SubmitForm(entity, entity.OrderID);
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
        /// 编辑一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultClass<int>EditEntity(OrderEntity entity)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            OrderApp bll = new OrderApp();
            try
            {
                _ret = bll.SubmitForm(entity, entity.OrderID);
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
        /// 删除一条信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        public ResultClass<int>DelEntity(string keyValue)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            OrderApp bll = new OrderApp();
            try
            {
                _ret = bll.DeleteForm(keyValue);
            }catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            finally
            {
                bll.Dispose();
            }
            return _ret;
        }
    }
}
