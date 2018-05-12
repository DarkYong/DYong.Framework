using DYong.Code;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using System.Collections.Generic;

namespace DYong.IService.SystemManage
{
    /// <summary>
    /// 数据字典仓储接口操作类
    /// </summary>
    public interface IItemsDetailRepository : IRepositoryBase<ItemsDetailEntity>
    {
        List<ItemsDetailEntity> GetItemList(string enCode);
        List<ItemsDetailEntity> GetItemList(Pagination pagination, string enCode);
    }
}
