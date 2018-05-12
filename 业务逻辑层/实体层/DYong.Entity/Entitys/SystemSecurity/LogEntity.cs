
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemSecurity
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_Log", PhysicalID = "F_Id", TableDescribtion = "系统日志")]
    public class LogEntity : IEntity<LogEntity>, ICreationAudited
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public DateTime? F_Date { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        [DataMember]
        public string F_Account { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        public string F_NickName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public string F_Type { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public string F_IPAddress { get; set; }
        /// <summary>
        /// IP所在城市
        /// </summary>
        [DataMember]
        public string F_IPAddressName { get; set; }
        /// <summary>
        /// 系统模块Id
        /// </summary>
        [DataMember]
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 系统模块
        /// </summary>
        [DataMember]
        public string F_ModuleName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        [DataMember]
        public bool? F_Result { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public string F_CreatorUserId { get; set; }
    }
}
