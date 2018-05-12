
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 角色表
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_Role", PhysicalID = "F_Id", TableDescribtion = "角色表")]
    public class RoleEntity : IEntity<RoleEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 组织主键
        /// </summary>
        [DataMember]
        public string F_OrganizeId { get; set; }
        /// <summary>
        /// 分类:1-角色2-岗位
        /// </summary>
        [DataMember]
        public int? F_Category { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string F_EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string F_FullName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public string F_Type { get; set; }
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
        /// 最后修改用户
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
