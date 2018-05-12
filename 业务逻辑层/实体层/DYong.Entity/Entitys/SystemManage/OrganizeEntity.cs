
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 组织表【集团，公司，部门，小组】
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_Organize", PhysicalID = "F_Id", TableDescribtion = "组织表")]
    public class OrganizeEntity : IEntity<OrganizeEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 组织主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [DataMember]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
        [DataMember]
        public int? F_Layers { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        public string F_EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string F_FullName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [DataMember]
        public string F_ShortName { get; set; }
        /// <summary>
        /// 分类ID【对应Sys_ItemsDetail表相应ID】
        /// </summary>
        [DataMember]
        public string F_CategoryId { get; set; }
        /// <summary>
        /// 负责人ID
        /// </summary>
        [DataMember]
        public string F_ManagerId { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string F_TelePhone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [DataMember]
        public string F_MobilePhone { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        [DataMember]
        public string F_WeChat { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        [DataMember]
        public string F_Fax { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string F_Email { get; set; }
        /// <summary>
        /// 归属区域ID
        /// </summary>
        [DataMember]
        public string F_AreaId { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [DataMember]
        public string F_Address { get; set; }
        /// <summary>
        /// 允许编辑
        /// </summary>
        [DataMember]
        public bool? F_AllowEdit { get; set; }
        /// <summary>
        /// 允许删除
        /// </summary>
        [DataMember]
        public bool? F_AllowDelete { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标志
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
        /// 创建用户ID
        /// </summary>
        [DataMember]
        public string F_CreatorUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime? F_LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        [DataMember]
        public string F_LastModifyUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [DataMember]
        public DateTime? F_DeleteTime { get; set; }
        /// <summary>
        /// 删除用户ID
        /// </summary>
        [DataMember]
        public string F_DeleteUserId { get; set; }
    }
}
