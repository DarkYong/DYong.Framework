using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace DYong.WebApi.App_Start
{
    /// <summary>
    /// WebApi 异常处理类
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 重写基类的异常处理方法
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
                actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
                actionExecutedContext.Exception.StackTrace);

            //2.返回调用方具体的异常信息
            #region 异常具体方法
            switch (actionExecutedContext.Exception.Message)
            {
                case "100":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Continue, showMessage, ReasonPhrase);
                        break;
                    }
                case "101":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.SwitchingProtocols, showMessage, ReasonPhrase);
                        break;
                    }
                case "200":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.OK, showMessage, ReasonPhrase);
                        break;
                    }
                case "201":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Created, showMessage, ReasonPhrase);
                        break;
                    }
                case "202":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Accepted, showMessage, ReasonPhrase);
                        break;
                    }
                case "203":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.NonAuthoritativeInformation, showMessage, ReasonPhrase);
                        break;
                    }
                case "204":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.NoContent, showMessage, ReasonPhrase);
                        break;
                    }
                case "205":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.ResetContent, showMessage, ReasonPhrase);
                        break;
                    }
                case "206":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.PartialContent, showMessage, ReasonPhrase);
                        break;
                    }
                case "300":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.MultipleChoices, showMessage, ReasonPhrase);
                        break;
                    }
                case "301":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.MovedPermanently, showMessage, ReasonPhrase);
                        break;
                    }
                case "302":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Found, showMessage, ReasonPhrase);
                        break;
                    }
                case "303":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.RedirectMethod, showMessage, ReasonPhrase);
                        break;
                    }
                case "304":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.NotModified, showMessage, ReasonPhrase);
                        break;
                    }
                case "305":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.UseProxy, showMessage, ReasonPhrase);
                        break;
                    }
                case "306":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Unused, showMessage, ReasonPhrase);
                        break;
                    }
                case "307":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.RedirectKeepVerb, showMessage, ReasonPhrase);
                        break;
                    }
                case "400":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.BadRequest, showMessage, ReasonPhrase);
                        break;
                    }
                case "401":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Unauthorized, showMessage, ReasonPhrase);
                        break;
                    }
                case "402":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.PaymentRequired, showMessage, ReasonPhrase);
                        break;
                    }
                case "403":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Forbidden, showMessage, ReasonPhrase);
                        break;
                    }
                case "404":
                    {
                        string showMessage = "指示请求的资源不在服务器上";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.NotFound, showMessage, ReasonPhrase);
                        break;
                    }
                case "405":
                    {                     
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.MethodNotAllowed, showMessage, ReasonPhrase);
                        break;
                    }
                case "406":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.NotAcceptable, showMessage, ReasonPhrase);
                        break;
                    }
                case "407":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.ProxyAuthenticationRequired, showMessage, ReasonPhrase);
                        break;
                    }
                case "408":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.RequestTimeout, showMessage, ReasonPhrase);
                        break;
                    }
                case "409":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Conflict, showMessage, ReasonPhrase);
                        break;
                    }
                case "410":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.Gone, showMessage, ReasonPhrase);
                        break;
                    }
                case "411":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.LengthRequired, showMessage, ReasonPhrase);
                        break;
                    }
                case "412":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.PreconditionFailed, showMessage, ReasonPhrase);
                        break;
                    }
                case "413":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.RequestEntityTooLarge, showMessage, ReasonPhrase);
                        break;
                    }
                case "414":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.RequestUriTooLong, showMessage, ReasonPhrase);
                        break;
                    }
                case "415":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.UnsupportedMediaType, showMessage, ReasonPhrase);
                        break;
                    }
                case "416":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.RequestedRangeNotSatisfiable, showMessage, ReasonPhrase);
                        break;
                    }
                case "417":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.ExpectationFailed, showMessage, ReasonPhrase);
                        break;
                    }
                case "426":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.UpgradeRequired, showMessage, ReasonPhrase);
                        break;
                    }
                case "500":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.InternalServerError, showMessage, ReasonPhrase);
                        break;
                    }
                case "501":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.NotImplemented, showMessage, ReasonPhrase);
                        break;
                    }
                case "502":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.BadGateway, showMessage, ReasonPhrase);
                        break;
                    }
                case "503":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.ServiceUnavailable, showMessage, ReasonPhrase);
                        break;
                    }
                case "504":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.GatewayTimeout, showMessage, ReasonPhrase);
                        break;
                    }
                case "505":
                    {
                        string showMessage = "";
                        string ReasonPhrase = "";
                        SetException(actionExecutedContext, HttpStatusCode.HttpVersionNotSupported, showMessage, ReasonPhrase);
                        break;
                    }
            }
            #endregion
            base.OnException(actionExecutedContext);
        }
        /// <summary>
        /// 设置错误异常信息
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="codeType"></param>
        /// <param name="content"></param>
        /// <param name="reasonPhrase"></param>
        private void SetException(HttpActionExecutedContext actionExecutedContext,HttpStatusCode codeType,string content,string reasonPhrase)
        {
            var oResponse = new HttpResponseMessage(codeType);
            oResponse.Content = new StringContent(content);
            oResponse.ReasonPhrase = reasonPhrase;
            actionExecutedContext.Response = oResponse;
        }
    }
}