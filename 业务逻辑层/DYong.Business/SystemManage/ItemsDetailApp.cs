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
    /// 数据字典业务操作类
    /// </summary>
    public class ItemsDetailApp
    {
        private IItemsDetailRepository service = new ItemsDetailRepository();
        /// <summary>
        /// 获得数据字典列表信息【全部】
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            ResultClass<IQueryable<ItemsDetailEntity>> _ret = service.IQueryable(expression);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderBy(t => t.F_SortCode).ToList();
            }
            return new List<ItemsDetailEntity>();
        }
        /// <summary>
        /// 获得数据字典列表信息【分页】
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> GetList(Pagination pagination,string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            ResultClass<List<ItemsDetailEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            return new List<ItemsDetailEntity>();
        }
        /// <summary>
        /// 获得数据字典列表信息【全部】
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }
        /// <summary>
        /// 获得数据字典列表信息【分页】
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> GetItemList(Pagination pagination, string enCode)
        {
            return service.GetItemList(pagination,enCode);
        }

        /// <summary>
        /// 获得数据字典详细信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ItemsDetailEntity GetForm(string keyValue)
        {
            ResultClass<ItemsDetailEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result) return _ret.ResultData;
            return new ItemsDetailEntity();
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
        /// <param name="itemsDetailEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsDetailEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(itemsDetailEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else
            {
                itemsDetailEntity.Create();
                ResultClass<int> _ret = service.Insert(itemsDetailEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
