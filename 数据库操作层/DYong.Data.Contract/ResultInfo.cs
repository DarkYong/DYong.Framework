using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DYong.Data.Contract
{
    /// <summary>
    /// 执行结果信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    [Serializable]
    public class ResultClass<T> : MarshalByRefObject, System.IDisposable
    {
        private string _ErrorMessage = "";
        /// <summary>
        /// 异常信息
        /// </summary>
        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
            }
        }

        private bool _Result = false;
        /// <summary>
        /// 执行结果(是否成功）
        /// </summary>
        [DataMember]
        public bool Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }
        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember]
        public T ResultData { get; set; }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            GC.Collect(0);
        }
        /// <summary>
        /// 指定T类型
        /// </summary>
        public ResultClass()
        {
            ResultData = default(T);
            ErrorMessage = "";
            Result = false;

        }

    }
}
