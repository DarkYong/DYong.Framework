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
    /// 数据字典业务实现类
    /// </summary>
    public class ItemsApp
    {
        private IItemsRepository service = new ItemsRepository();
        /// <summary>
        /// 获得所有数据字典类型信息【全部】
        /// </summary>
        /// <returns></returns>
        public List<ItemsEntity> GetList()
        {
            ResultClass<IQueryable<ItemsEntity>> _ret = service.IQueryable();
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            return new List<ItemsEntity>();
        }
        /// <summary>
        /// 获得所有数据字典类型信息【分页】
        /// </summary>
        /// <returns></returns>
        public List<ItemsEntity> GetList(Pagination pagination)
        {
            var expression = ExtLinq.True<ItemsEntity>();
            ResultClass<List<ItemsEntity>> _ret = service.FindList(expression, pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            return new List<ItemsEntity>();
        }


        /// <summary>
        /// 操作数据字典类型信息
        /// </summary>
        /// <param name="itemsEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//数据字典类型信息
            {
                itemsEntity.Modify(keyValue);
                ResultClass<int> _ret = service.Update(itemsEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
            else//新数据字典类型信息
            {
                itemsEntity.Create();
                ResultClass<int> _ret = service.Insert(itemsEntity);
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }

        /// <summary>
        ///获得字段类型信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ItemsEntity GetForm(string keyValue)
        {
            //return service.FindEntity(keyValue).ResultData;
            ResultClass<ItemsEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result) return _ret.ResultData;
            return new ItemsEntity();
        }
        /// <summary>
        /// 删除数据字典信息
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<List<ItemsEntity>> _ret = service.FindList(new ItemsEntity() { F_ParentId = keyValue });
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
