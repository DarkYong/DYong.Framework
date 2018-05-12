using DYong.Code;
using DYong.Data.Contract;
using DYong.Entity.Entitys.SystemManage;
using DYong.IService.SystemManage;
using DYong.Service.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DYong.Business.SystemManage
{
    /// <summary>
    /// 角色管理业务实现类
    /// </summary>
    public class RoleApp
    {
        private IRoleRepository service = new RoleRepository(); 
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();
        /// <summary>
        /// 获得所有角色信息【全部】
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<RoleEntity> GetList(string keyword = "") 
        {         
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);//设置类型
            ResultClass<IQueryable<RoleEntity>> _ret = service.IQueryable(expression);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<RoleEntity>();
        }
        /// <summary>
        /// 获得所有角色信息【分页】
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<RoleEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);//设置类型
            ResultClass<List<RoleEntity>> _ret = service.FindList(expression,pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<RoleEntity>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public RoleEntity GetForm(string keyValue)
        {
            //return service.FindEntity(keyValue).ResultData;
            ResultClass<RoleEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result)
            {
                return _ret.ResultData;
            }
            return new RoleEntity(); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            //service.DeleteForm(keyValue);
            ResultClass<int> _ret = service.Delete(t => t.F_Id == keyValue);
            if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
        }
        /// <summary>
        /// 提交角色信息
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_Id = keyValue;
            }
            else
            {
                roleEntity.F_Id = Common.GuId();
            }
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            List<RoleAuthorizeEntity> roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                RoleAuthorizeEntity roleAuthorizeEntity = new RoleAuthorizeEntity();
                roleAuthorizeEntity.F_Id = Common.GuId();
                roleAuthorizeEntity.F_ObjectType = 1;
                roleAuthorizeEntity.F_ObjectId = roleEntity.F_Id;
                roleAuthorizeEntity.F_ItemId = itemId;
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            service.SubmitForm(roleEntity, roleAuthorizeEntitys, keyValue);
        }
    }
}
