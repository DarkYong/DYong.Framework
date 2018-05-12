
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 系统模块
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_Module", PhysicalID = "F_Id", TableDescribtion = "系统模块")]
    public class ModuleEntity : IEntity<ModuleEntity>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 模块主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 父级
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
        /// 图标
        /// </summary>
        [DataMember]
        public string F_Icon { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        [DataMember]
        public string F_UrlAddress { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        [DataMember]
        public string F_Target { get; set; }
        /// <summary>
        /// 是否菜单菜单
        /// </summary>
        [DataMember]
        public bool? F_IsMenu { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        [DataMember]
        public bool? F_IsExpand { get; set; }
        /// <summary>
        /// 是否公共
        /// </summary>
        [DataMember]
        public bool? F_IsPublic { get; set; }
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
        [DataMember]
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
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public string F_CreatorUserId { get; set; }
        /// <summary>
        /// 创建用户名称
        /// </summary>
        [DataMember]
        [NotMapped]
        public string F_CreatorUserName { get; set; }
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
        /// 最后修改用户名称
        /// </summary>
        [DataMember]
        [NotMapped]
        public string F_LastModifyUserName { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [DataMember]
        public DateTime? F_DeleteTime { get; set; }
        /// <summary>
        /// 删除用户
        /// </summary>
        [DataMember]
        public string F_DeleteUserId { get; set; }
        /// <summary>
        /// 删除用户名称
        /// </summary>
        [DataMember]
        [NotMapped]
        public string F_DeleteUserName { get; set; }
    }
}
