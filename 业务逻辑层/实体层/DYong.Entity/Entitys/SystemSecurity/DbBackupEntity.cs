
using System;
using System.Runtime.Serialization;

namespace DYong.Entity.Entitys.SystemSecurity
{
    /// <summary>
    /// 数据库备份
    /// </summary>
    [DataContract]
    [Serializable]
    [TableClassInfo(TableName = "Sys_DbBackup", PhysicalID = "F_Id", TableDescribtion = "数据库备份")]
    public class DbBackupEntity : ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 备份主键
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }
        /// <summary>
        ///备份类型
        /// </summary>
        [DataMember]
        public string F_BackupType { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        [DataMember]
        public string F_DbName { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [DataMember]
        public string F_FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public string F_FileSize { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        public string F_FilePath { get; set; }
        /// <summary>
        /// 备份时间
        /// </summary>
        [DataMember]
        public DateTime? F_BackupTime { get; set; }
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
        /// 创建用户
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
