
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
    public class DbBackupApp
    {
        private IDbBackupRepository service = new DbBackupRepository(); 
        /// <summary>
        /// 获得备份信息列表【全部】
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<DbBackupEntity> GetList(string queryJson)
        {
            var expression = ExtLinq.True<DbBackupEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "DbName":  
                        expression = expression.And(t => t.F_DbName.Contains(keyword));
                        break;
                    case "FileName":
                        expression = expression.And(t => t.F_FileName.Contains(keyword));
                        break;
                }
            }
            ResultClass<IQueryable<DbBackupEntity>> _ret = service.IQueryable(expression);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderByDescending(t => t.F_BackupTime).ToList();
            }
            throw new System.Exception(_ret.ErrorMessage);
        }
        /// <summary>
        /// 获得备份信息列表【分页】
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<DbBackupEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<DbBackupEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "DbName":
                        expression = expression.And(t => t.F_DbName.Contains(keyword));
                        break;
                    case "FileName":
                        expression = expression.And(t => t.F_FileName.Contains(keyword));
                        break;
                }
            }
            ResultClass<List<DbBackupEntity>> _ret = service.FindList(expression,pagination);
            if (_ret.Result)
            {
                return _ret.ResultData.OrderByDescending(t => t.F_BackupTime).ToList();
            }
            throw new System.Exception(_ret.ErrorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public DbBackupEntity GetForm(string keyValue)
        {
            //return service.FindEntity(keyValue).ResultData;
            ResultClass<DbBackupEntity> _ret = service.FindEntity(keyValue);
            if (_ret.Result)
            {
                return _ret.ResultData;
            }
            return new DbBackupEntity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            //service.DeleteForm(keyValue);
            ResultClass<int> _ret = service.Delete(t => t.F_Id == keyValue);
            if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
        }
        public void SubmitForm(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.F_Id = Common.GuId();
            dbBackupEntity.F_EnabledMark = true;
            dbBackupEntity.F_BackupTime = DateTime.Now;
            service.ExecuteDbBackup(dbBackupEntity);
        }
    }
}
