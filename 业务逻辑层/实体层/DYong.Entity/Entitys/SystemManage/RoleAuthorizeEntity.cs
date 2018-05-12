
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 角色授权【菜单,菜单按钮】信息表
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_RoleAuthorize", PhysicalID = "F_Id", TableDescribtion = "角色授权表")]
    public class RoleAuthorizeEntity : IEntity<RoleAuthorizeEntity>, ICreationAudited
    {
        /// <summary>
        /// 角色授权主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 项目类型1-模块2-按钮3-列表
        /// </summary>
        [DataMember]
        public int? F_ItemType { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>
        [DataMember]
        public string F_ItemId { get; set; }
        /// <summary>
        /// 对象分类1-角色2-部门-3用户
        /// </summary>
        [DataMember]
        public int? F_ObjectType { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        [DataMember]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? F_SortCode { get; set; }
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
