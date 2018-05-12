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
    /// 组织机构业务实现类
    /// </summary>
    public class OrganizeApp
    {
        private IOrganizeRepository service = new OrganizeRepository();
        /// <summary>
        /// 获得组织信息列表数据【全部】
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetList()
        {
            ResultClass<IQueryable<OrganizeEntity>> _ret = service.IQueryable();
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_CreatorTime).ToList();
            }
            return new List<OrganizeEntity>();       
        }
        /// <summary>
        /// 获得组织信息列表数据【分页】
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetList(Pagination pagination)
        {
            var expression = ExtLinq.True<OrganizeEntity>();
            ResultClass<List<OrganizeEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_CreatorTime).ToList();
            }
            return new List<OrganizeEntity>();
        }
        /// <summary>
        /// 获得一个组织信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public OrganizeEntity GetForm(string keyValue)
        {
            ResultClass<OrganizeEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result)
            {
                return _ret.ResultData;
            }
            return new OrganizeEntity(); ;
        }
        /// <summary>
        /// 删除一个组织信息
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<List<OrganizeEntity>> _ret = service.FindList(new OrganizeEntity() {F_ParentId= keyValue});
            if (!_ret.Result)
            {
                service.Delete(t => t.F_Id == keyValue);
                return;
            }
            throw new Exception("删除失败！操作的对象包含了下级数据。");
        }
        /// <summary>
        /// 操作一个实体信息
        /// </summary>
        /// <param name="organizeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(organizeEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else
            {
                organizeEntity.Create();
                ResultClass<int> _ret = service.Insert(organizeEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
