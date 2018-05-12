using DYong.Data.Contract;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using DYong.IService.SystemManage;
using System;
using System.Collections.Generic;

namespace DYong.Service.SystemManage
{
    /// <summary>
    /// 角色管理数据库操作类
    /// </summary>
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        /// <summary>
        /// 删除角色新
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            //using (var db = new RepositoryBase().BeginTrans())
            //{
            //    db.Delete<RoleEntity>(t => t.F_Id == keyValue);
            //    db.Delete<RoleAuthorizeEntity>(t => t.F_ObjectId == keyValue);
            //    db.Commit();
            //}
        }
        /// <summary>
        /// 提交角色信息
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(RoleEntity roleEntity, List<RoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(roleEntity);
                }
                else
                {
                    roleEntity.F_Category = 1;
                    db.Insert(roleEntity);
                }
                db.Delete<RoleAuthorizeEntity>(t => t.F_ObjectId == roleEntity.F_Id);
                db.Insert(roleAuthorizeEntitys);
                ResultClass<int>_ret= db.Commit();
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
