using DYong.Code;
using DYong.Data.Contract;
using DYong.Entity.Entitys.SystemManage;
using DYong.Entity.ViewModel;
using DYong.IService.SystemManage;
using DYong.Service.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DYong.Business.SystemManage
{
    /// <summary>
    /// 角色权限【菜单，按钮】业务操作类
    /// </summary>
    public class RoleAuthorizeApp
    {
        private IRoleAuthorizeRepository service = new RoleAuthorizeRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();
        /// <summary>
        /// 获得用户的所有权限信息列表【全部】
        /// </summary>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public List<RoleAuthorizeEntity> GetList(string ObjectId)
        {
            ResultClass<IQueryable<RoleAuthorizeEntity>> _ret = service.IQueryable(t => t.F_ObjectId == ObjectId);
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            return new List<RoleAuthorizeEntity>();
        }
        /// <summary>
        /// 获得用户的所有权限信息列表【分页】
        /// </summary>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public List<RoleAuthorizeEntity> GetList(Pagination pagination, string ObjectId)
        {
            var expression = ExtLinq.True<RoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(ObjectId))
            {
                expression = expression.And(t => t.F_ObjectId.Contains(ObjectId));
            }
            ResultClass<List<RoleAuthorizeEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<RoleAuthorizeEntity>();
        }

        /// <summary>
        /// 根据角色ID获得菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<ModuleEntity> GetMenuList(string roleId)
        {
            var data = new List<ModuleEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleApp.GetList();
            }
            else
            {
                data = moduleApp.GetList(roleId);
                //var moduledata = moduleApp.GetList();
                //var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 1).ResultData.ToList();
                //foreach (var item in authorizedata)
                //{
                //    ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                //    if (moduleEntity != null)
                //    {
                //        data.Add(moduleEntity);
                //    }
                //}
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }
        /// <summary>
        /// 根据角色ID获得菜单按钮信息列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetButtonList(string roleId)
        {
            var data = new List<ModuleButtonEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleButtonApp.GetList();
            }
            else
            {
                data = moduleButtonApp.GetListByRoleID(roleId);
                //var buttondata = moduleButtonApp.GetList();
                //var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 2).ResultData.ToList();
                //foreach (var item in authorizedata)
                //{
                //    ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                //    if (moduleButtonEntity != null)
                //    {
                //        data.Add(moduleButtonEntity);
                //    }
                //}
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }

        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(string roleId, string moduleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            if (cachedata == null)
            {
                var moduledata = moduleApp.GetList(roleId);//获得当前用户菜单信息列表
                foreach(var item in moduledata)
                {
                    authorizeurldata.Add(new AuthorizeActionModel { F_Id = item.F_Id, F_UrlAddress = item.F_UrlAddress });
                }
                var buttondata = moduleButtonApp.GetListByRoleID(roleId);//获得当前用户菜单按钮列表
                foreach(var item in buttondata)
                {
                    authorizeurldata.Add(new AuthorizeActionModel { F_Id = item.F_ModuleId, F_UrlAddress = item.F_UrlAddress });
                }
                #region 旧方法
                //var moduledata = moduleApp.GetList();//获得所有菜单信息列表
                //var buttondata = moduleButtonApp.GetList();//获得所有菜单按钮列表
                //var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId).ResultData.ToList();//获得用户权限列表
                //foreach (var item in authorizedata)
                //{
                //    if (item.F_ItemType == 1)
                //    {
                //        ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                //        authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleEntity.F_Id, F_UrlAddress = moduleEntity.F_UrlAddress });
                //    }
                //    else if (item.F_ItemType == 2)
                //    {
                //        ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                //        authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleButtonEntity.F_ModuleId, F_UrlAddress = moduleButtonEntity.F_UrlAddress });
                //    }
                //}
                #endregion
                CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
            }
            else
            {
                authorizeurldata = cachedata;
            }
            authorizeurldata = authorizeurldata.FindAll(t => t.F_Id.Equals(moduleId));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.F_UrlAddress))
                {
                    string[] url = item.F_UrlAddress.Split('?');
                    if (item.F_Id == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
