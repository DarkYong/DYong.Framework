using System;

namespace DYong.Entity
{
    /// <summary>
    /// 修改接口
    /// </summary>
    public interface IModificationAudited
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        string F_Id { get; set; }
        /// <summary>
        /// 修改用户ID
        /// </summary>
        string F_LastModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? F_LastModifyTime { get; set; }
    }
}