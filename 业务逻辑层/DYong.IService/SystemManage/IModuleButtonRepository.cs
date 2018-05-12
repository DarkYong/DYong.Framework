
using DYong.Code;
using DYong.Data.Contract;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using System.Collections.Generic;

namespace DYong.IService.SystemManage
{
    public interface IModuleButtonRepository : IRepositoryBase<ModuleButtonEntity>
    {
        ResultClass<List<ModuleButtonEntity>> GetModuleButtonByObjectID(string objectType, string objectID);
        ResultClass<List<ModuleButtonEntity>> GetModuleButtonByObjectID(Pagination pagination, string objectType, string objectID);
        void SubmitCloneButton(List<ModuleButtonEntity> entitys);
    }
}
