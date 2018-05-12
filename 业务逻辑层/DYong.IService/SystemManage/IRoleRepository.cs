using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using System.Collections.Generic;

namespace DYong.IService.SystemManage
{
    /// <summary>
    /// 角色管理数据库操作接口类
    /// </summary>
    public interface IRoleRepository : IRepositoryBase<RoleEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);
    }
}
