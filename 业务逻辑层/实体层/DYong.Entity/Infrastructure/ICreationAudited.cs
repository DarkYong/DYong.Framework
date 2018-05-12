using System;

namespace DYong.Entity
{
    /// <summary>
    /// 创建接口
    /// </summary>
    
    public interface ICreationAudited
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        string F_Id { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        string F_CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? F_CreatorTime { get; set; }
    }
}