using System;
using System.Collections.Generic;
using DYong.Data.Contract;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using DYong.IService.SystemManage;
using System.Text;
using DYong.Code;
using DYong.Data;

namespace DYong.Service.SystemManage
{
    /// <summary>
    /// 菜单管理数据库操作类
    /// </summary>
    public class ModuleRepository : RepositoryBase<ModuleEntity>, IModuleRepository
    {
        public ResultClass<List<ModuleEntity>> GetModuleByObjectID(string objectType,string objectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select t.* 
                            from Sys_Module t,Sys_RoleAuthorize r 
                            where t.F_Id=r.F_ItemId and r.F_ItemType=1 
                                  and r.F_ObjectType=@F_ObjectType and r.F_ObjectId=@F_ObjectId");
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("F_ObjectType", objectType);
            parameter.Add("F_ObjectId", objectID);    
            return this.FindList(strSql.ToString(), parameter);
        }

        public ResultClass<List<ModuleEntity>> GetModuleByObjectID(Pagination pagination, string objectType, string objectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select t.* 
                            from Sys_Module t,Sys_RoleAuthorize r 
                            where t.F_Id=r.F_ItemId and r.F_ItemType=1 
                                  and r.F_ObjectType=@F_ObjectType and r.F_ObjectId=@F_ObjectId");
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("F_ObjectType", objectType);
            parameter.Add("F_ObjectId", objectID);
            string sql = DatabasePage.GetStrSql(strSql.ToString(), parameter, pagination);
            return this.FindList(sql, new Dictionary<string, object>());
        }
    }
}
