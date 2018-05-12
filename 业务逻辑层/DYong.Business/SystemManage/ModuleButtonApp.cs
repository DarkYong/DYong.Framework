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
    /// 菜单按钮管理业务操作类
    /// </summary>
    public class ModuleButtonApp
    {
        private IModuleButtonRepository service = new ModuleButtonRepository();
        /// <summary>
        /// 获得菜单按钮列表信息【全部】
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            ResultClass<IQueryable<ModuleButtonEntity>> _ret = service.IQueryable(expression);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleButtonEntity>();
        }
        /// <summary>
        /// 获得菜单按钮列表信息【分页】
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetList(Pagination pagination,string moduleId = "")
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            ResultClass<List<ModuleButtonEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleButtonEntity>();
        }
        /// <summary>
        /// 获得菜单按钮列表信息【全部】
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetListByRoleID(string roleID)
        {
            ResultClass<List<ModuleButtonEntity>> _ret = service.GetModuleButtonByObjectID("1", roleID);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleButtonEntity>();
        }
        /// <summary>
        /// 获得菜单按钮列表信息【分页】
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetListByRoleID(Pagination pagination, string roleID)
        {
            ResultClass<List<ModuleButtonEntity>> _ret = service.GetModuleButtonByObjectID(pagination,"1", roleID);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleButtonEntity>();
        }
        /// <summary>
        /// 获得菜单按钮信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ModuleButtonEntity GetForm(string keyValue)
        {
            //return service.FindEntity(keyValue).ResultData;
            ResultClass<ModuleButtonEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result) return _ret.ResultData;
            return new ModuleButtonEntity();
        }
        /// <summary>
        /// 删除菜单按钮信息
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<List<ModuleButtonEntity>> _ret = service.FindList(new ModuleButtonEntity() { F_ParentId = keyValue });
            if (!_ret.Result)
            {
                ResultClass<int> _rets = service.Delete(t => t.F_Id == keyValue);
                if (!_rets.Result) throw new Exception(_rets.ErrorMessage);
            }
            else
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
        }
        /// <summary>
        /// 修改菜单按钮信息
        /// </summary>
        /// <param name="moduleButtonEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleButtonEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(moduleButtonEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else
            {
                moduleButtonEntity.Create();
                ResultClass<int> _ret = service.Insert(moduleButtonEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="Ids"></param>
        public void SubmitCloneButton(string moduleId, string Ids)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            List<ModuleButtonEntity> entitys = new List<ModuleButtonEntity>();
            foreach (string item in ArrayId)
            {
                ModuleButtonEntity moduleButtonEntity = data.Find(t => t.F_Id == item);
                moduleButtonEntity.F_Id = Common.GuId();
                moduleButtonEntity.F_ModuleId = moduleId;
                entitys.Add(moduleButtonEntity);
            }
            service.SubmitCloneButton(entitys);
        }
    }
}
