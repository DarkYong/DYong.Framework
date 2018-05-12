using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using DYong.IService.SystemManage;
using System.Collections.Generic;
using DYong.Data.Contract;
using System;
using System.Text;
using DYong.Code;
using DYong.Data;

namespace DYong.Service.SystemManage
{
    public class ModuleButtonRepository : RepositoryBase<ModuleButtonEntity>, IModuleButtonRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public ResultClass<List<ModuleButtonEntity>> GetModuleButtonByObjectID(string objectType, string objectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select t.*
                            from Sys_ModuleButton t,Sys_RoleAuthorize r
                            where t.F_Id=r.F_ItemId and r.F_ItemType=2
                                  and r.F_ObjectType=@F_ObjectType and r.F_ObjectId=@F_ObjectId");
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("F_ObjectType", objectType);
            parameter.Add("F_ObjectId", objectID);
            return this.FindList(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public ResultClass<List<ModuleButtonEntity>> GetModuleButtonByObjectID(Pagination pagination,string objectType, string objectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select t.*
                            from Sys_ModuleButton t,Sys_RoleAuthorize r
                            where t.F_Id=r.F_ItemId and r.F_ItemType=2
                                  and r.F_ObjectType=@F_ObjectType and r.F_ObjectId=@F_ObjectId");
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("F_ObjectType", objectType);
            parameter.Add("F_ObjectId", objectID);
            string sql = DatabasePage.GetStrSql(strSql.ToString(), parameter, pagination);
            return this.FindList(sql, new Dictionary<string, object>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entitys"></param>
        public void SubmitCloneButton(List<ModuleButtonEntity> entitys)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                foreach (var item in entitys)
                {
                    db.Insert(item);
                }
                ResultClass<int>_ret=db.Commit();
                if (!_ret.Result) throw new Exception(_ret.ErrorMessage);
            }
        }
    }
}
