
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DYong.IService;
using DYong.Service;
using DYong.Entity.Entitys;
using DYong.Code;
using DYong.Data.Contract;

namespace DYong.Business
{
    public class OrderApp:IDisposable
    {
        IOrderRepository service = new OrderRepository();

        /// <summary>
        /// 获得所有信息列表
        /// </summary>
        /// <returns></returns>
        public List<OrderEntity> GetList()
        {
            ResultClass<IQueryable<OrderEntity>> _ret = service.IQueryable();
            if (_ret.Result)
            {
                return _ret.ResultData.ToList();
            }
            else
            {
                throw new Exception(_ret.ErrorMessage);
            }
        }
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ResultClass<OrderEntity>GetForm(string keyValue)
        {
            //return service.FindEntity(keyValue);
            OrderEntity areaEntity = new OrderEntity();
            areaEntity.OrderID = keyValue;

            Dictionary<string, object> pas = new Dictionary<string, object>();
            string sql = areaEntity.EntityToSql<OrderEntity>(pas);
            return service.FindEntity(sql, pas);
        }
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        public ResultClass<int> DeleteForm(string keyValue)
        {
            //return service.Delete(t => t.OrderID == keyValue);
            OrderEntity areaEntity = new OrderEntity();
            areaEntity.OrderID = keyValue;

            Dictionary<string, object> pas = new Dictionary<string, object>();
            string sql = areaEntity.EntityToDelSql<OrderEntity>(pas);
            return service.Delete(sql, pas);
        }
        /// <summary>
        /// 操作一个实体【修改、新增】
        /// </summary>
        /// <param name="areaEntity"></param>
        /// <param name="keyValue"></param>
        public ResultClass<int> SubmitForm(OrderEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {               
                areaEntity.OrderID = keyValue;
                //return  service.Update(areaEntity);

                Dictionary<string,object> pas = new Dictionary<string, object>();
                string sql = areaEntity.EntityToEditSql<OrderEntity>(pas);
                return service.Update(sql, pas);
            }
            else
            {
                areaEntity.OrderID = Common.GuId();
                //return service.Insert(areaEntity);

                Dictionary<string, object> pas = new Dictionary<string, object>();
                string sql = areaEntity.EntityToAddSql<OrderEntity>(pas);
                return service.Insert(sql,pas);
            }
        }

        /// <summary>
        /// 释放函数
        /// </summary>
        public void Dispose()
        {
            GC.Collect(0);
        }
    }
}
