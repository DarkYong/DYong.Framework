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
    /// 菜单管理业务操作类
    /// </summary>
    public class ModuleApp
    {
        private IModuleRepository service = new ModuleRepository();
        /// <summary>
        /// 获得所有菜单列表信息【全部】
        /// </summary>
        /// <returns></returns>
        public List<ModuleEntity> GetList() 
        {
            ResultClass<IQueryable<ModuleEntity>> _ret = service.IQueryable();
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleEntity>();
        }
        /// <summary>
        /// 获得所有菜单列表信息【分页】
        /// </summary>
        /// <returns></returns>
        public List<ModuleEntity> GetList(Pagination pagination)
        {
            var expression = ExtLinq.True<ModuleEntity>();
            ResultClass<List<ModuleEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleEntity>();
        }
        /// <summary>
        /// 获得所有菜单列表信息【全部】
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public List<ModuleEntity> GetList(string objectID)
        {            
            ResultClass<List<ModuleEntity>> _ret = service.GetModuleByObjectID("1",objectID);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleEntity>();
        }
        /// <summary>
        /// 获得所有菜单列表信息【分页】
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public List<ModuleEntity> GetList(Pagination pagination,string objectID)
        {
            ResultClass<List<ModuleEntity>> _ret = service.GetModuleByObjectID(pagination,"1", objectID);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ModuleEntity>();
        }


        /// <summary>
        /// 获得菜单信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ModuleEntity GetForm(string keyValue)
        {
            ResultClass<ModuleEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result) return _ret.ResultData;
            return new ModuleEntity();
        }
        /// <summary>
        /// 删除一个菜单信息
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<List<ModuleEntity>> _ret = service.FindList(new ModuleEntity() { F_ParentId = keyValue });
            if (!_ret.Result)
            {
                ResultClass<int> _rets = service.Delete(t => t.F_Id == keyValue&&t.F_AllowDelete==true);
                if (!_rets.Result) throw new Exception(_rets.ErrorMessage);
            }
            else
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }          
        }

        /// <summary>
        ///操作菜单信息
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ModuleEntity moduleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                ResultClass<int>_ret= service.Update(moduleEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else
            {
                moduleEntity.Create();
                ResultClass<int> _ret=service.Insert(moduleEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
