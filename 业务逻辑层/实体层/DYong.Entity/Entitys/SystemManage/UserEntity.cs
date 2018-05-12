
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_User", PhysicalID = "F_Id", TableDescribtion = "用户信息")]
    public class UserEntity : IEntity<UserEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [DataMember]
        public string F_Account { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember]
        public string F_RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public string F_NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [DataMember]
        public string F_HeadIcon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public bool? F_Gender { get; set; }
        /// <summary>
        /// 生日时间
        /// </summary>
        [DataMember]
        public DateTime? F_Birthday { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [DataMember]
        public string F_MobilePhone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [DataMember]
        public string F_Email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [DataMember]
        public string F_WeChat { get; set; }
        /// <summary>
        /// 主管主键ID
        /// </summary>
        [DataMember]
        public string F_ManagerId { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        [DataMember]
        public int? F_SecurityLevel { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        [DataMember]
        public string F_Signature { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        [DataMember]
        public string F_OrganizeId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [DataMember]
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [DataMember]
        public string F_RoleId { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        [DataMember]
        public string F_DutyId { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        [DataMember]
        public bool? F_IsAdministrator { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        [DataMember]
        public bool? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        [DataMember]
        public bool? F_EnabledMark { get; set; }
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
        ///创建人ID
        /// </summary>
        [DataMember]
        public string F_CreatorUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime? F_LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [DataMember]
        public string F_LastModifyUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [DataMember]
        public DateTime? F_DeleteTime { get; set; }
        /// <summary>
        /// 删除人ID
        /// </summary>
        [DataMember]
        public string F_DeleteUserId { get; set; }     
    }
}
