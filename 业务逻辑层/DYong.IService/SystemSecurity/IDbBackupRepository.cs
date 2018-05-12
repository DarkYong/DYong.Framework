using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemSecurity;

namespace DYong.IService.SystemSecurity
{
    public interface IDbBackupRepository : IRepositoryBase<DbBackupEntity>
    {
        void DeleteForm(string keyValue);
        void ExecuteDbBackup(DbBackupEntity dbBackupEntity);
    }
}
