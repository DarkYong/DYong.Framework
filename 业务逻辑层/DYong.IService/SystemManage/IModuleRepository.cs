using DYong.Code;
using DYong.Data.Contract;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using System.Collections.Generic;
using System.Linq;

namespace DYong.IService.SystemManage
{
    /// <summary>
    /// 菜单管理数据库接口操作类 
    /// </summary>
    public interface IModuleRepository : IRepositoryBase<ModuleEntity>
    {
        ResultClass<List<ModuleEntity>> GetModuleByObjectID(string objectType, string objectID);
        ResultClass<List<ModuleEntity>> GetModuleByObjectID(Pagination pagination,string objectType, string objectID);
    }
}
