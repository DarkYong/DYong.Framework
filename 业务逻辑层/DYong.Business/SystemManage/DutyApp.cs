
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
    /// 岗位管理业务实现类
    /// </summary>
    public class DutyApp
    {
        private IRoleRepository service = new RoleRepository();
        /// <summary>
        /// 获得所有岗位信息列表【全部】
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
            expression = expression.And(t => t.F_Category == 2);//设置类型
            ResultClass<IQueryable<RoleEntity>> _ret = service.IQueryable(expression);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<RoleEntity>();
        }

        /// <summary>
        /// 获得所有岗位信息列表【分页】
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
            expression = expression.And(t => t.F_Category == 2);//设置类型
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
            if (_ret.Result) return _ret.ResultData;
            return new RoleEntity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<int> _ret = service.Delete(t => t.F_Id == keyValue);
            if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(roleEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else
            {
                roleEntity.Create();
                roleEntity.F_Category = 2;
                ResultClass<int> _ret = service.Insert(roleEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
