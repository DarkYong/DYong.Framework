using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_Items", PhysicalID = "F_Id", TableDescribtion = "选项主表")]
    public class ItemsEntity : IEntity<ItemsEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 主表主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [DataMember]
        public string F_ParentId { get; set; }
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
        /// 树型
        /// </summary>
        [DataMember]
        public bool? F_IsTree { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
        [DataMember]
        public int? F_Layers { get; set; }
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
