
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
    /// 区域管理业务操作类
    /// </summary>
    public class AreaApp
    {
        private IAreaRepository service = new AreaRepository();
        /// <summary>
        /// 获得所有区域信息列表【全部】
        /// </summary>
        /// <returns></returns>
        public List<AreaEntity> GetList()
        {
            ResultClass<IQueryable<AreaEntity>> _ret = service.IQueryable();
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            return new List<AreaEntity>();
        }
        /// <summary>
        /// 获得所有区域信息列表【分页】
        /// </summary>
        /// <returns></returns>
        public List<AreaEntity> GetList(Pagination pagination)
        {
            var expression = ExtLinq.True<AreaEntity>();
            ResultClass<List<AreaEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            return new List<AreaEntity>();
        }



        /// <summary>
        /// 操作区域信息
        /// </summary>
        /// <param name="areaEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(AreaEntity areaEntity, string keyValue)
        {   
            if (!string.IsNullOrEmpty(keyValue))//编辑区域信息
            {
                areaEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(areaEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else//新增区域信息
            {
                areaEntity.Create();
                ResultClass<int> _ret = service.Insert(areaEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        } 
        /// <summary>
        /// 获得区域信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public AreaEntity GetForm(string keyValue)
        {
            ResultClass<AreaEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result) return _ret.ResultData;
            return new AreaEntity();
        }

        /// <summary>
        /// 删除区域信息
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<List<AreaEntity>> _ret= service.FindList(new AreaEntity() { F_ParentId = keyValue });
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
       
    }
}
