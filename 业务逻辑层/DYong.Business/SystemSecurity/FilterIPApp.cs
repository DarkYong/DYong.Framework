using DYong.Code;
using DYong.Data.Contract;
using DYong.Entity.Entitys.SystemSecurity;
using DYong.IService.SystemSecurity;
using DYong.Service.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DYong.Business.SystemSecurity
{
    public class FilterIPApp
    {
        private IFilterIPRepository service = new FilterIPRepository();
        /// <summary>
        /// 获得过滤IP信息列表【全部】
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<FilterIPEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            ResultClass<IQueryable<FilterIPEntity>> _ret = service.IQueryable(expression);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderByDescending(t => t.F_DeleteTime).ToList();
            }
            throw new System.Exception(_ret.ErrorMessage);
        }
        /// <summary>
        /// 获得过滤IP信息列表【分页】
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<FilterIPEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            ResultClass<List<FilterIPEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderByDescending(t => t.F_DeleteTime).ToList();
            }
            throw new System.Exception(_ret.ErrorMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FilterIPEntity GetForm(string keyValue)
        {
            //return service.FindEntity(keyValue).ResultData;
            ResultClass<FilterIPEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result)
            {
                return _ret.ResultData;
            }
            return new FilterIPEntity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            //service.Delete(t => t.F_Id == keyValue);
            ResultClass<int> _ret = service.Delete(t => t.F_Id == keyValue);
            if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterIPEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //filterIPEntity.Modify(keyValue);
                //service.Update(filterIPEntity);

                filterIPEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(filterIPEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else
            {
                //filterIPEntity.Create();
                //service.Insert(filterIPEntity);

                filterIPEntity.Create();
                ResultClass<int> _ret = service.Insert(filterIPEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
