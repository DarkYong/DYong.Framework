
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 模块按钮
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_ModuleButton", PhysicalID = "F_Id", TableDescribtion = "模块按钮")]
    public class ModuleButtonEntity : IEntity<ModuleButtonEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 按钮主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        [DataMember]
        public string F_ModuleId { get; set; }
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
        /// 位置
        /// </summary>
        [DataMember]
        public int? F_Location { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        [DataMember]
        public string F_JsEvent { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        [DataMember]
        public string F_UrlAddress { get; set; }
        /// <summary>
        /// 分开线
        /// </summary>
        [DataMember]
        public bool? F_Split { get; set; }
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
        /// 删除用户
        /// </summary>
        [DataMember]
        public string F_DeleteUserId { get; set; }
    }
}
