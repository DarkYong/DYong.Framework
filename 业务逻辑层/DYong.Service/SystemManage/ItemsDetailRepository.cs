using DYong.Code;
using DYong.Data;
using DYong.Data.Contract;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using DYong.IService.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DYong.Service.SystemManage
{
    /// <summary>
    /// 数据字典仓储类
    /// </summary>
    public class ItemsDetailRepository : RepositoryBase<ItemsDetailEntity>, IItemsDetailRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> GetItemList(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_ItemsDetail d
                                    INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId
                            WHERE   1 = 1
                                    AND i.F_EnCode = @enCode
                                    AND d.F_EnabledMark = 1
                                    AND d.F_DeleteMark = 0
                            ORDER BY d.F_SortCode ASC");
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("enCode", enCode);
            ResultClass<List<ItemsDetailEntity>> _ret = this.FindList(strSql.ToString(), parameter);
            if (_ret.Result)
            {
                return _ret.ResultData;
            }
            return new List<ItemsDetailEntity>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> GetItemList(Pagination pagination,string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_ItemsDetail d
                                    INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId
                            WHERE   1 = 1
                                    AND i.F_EnCode = @enCode
                                    AND d.F_EnabledMark = 1
                                    AND d.F_DeleteMark = 0 ");
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("enCode", enCode);
            string sql = DatabasePage.GetStrSql(strSql.ToString(), parameter, pagination);
            ResultClass<List<ItemsDetailEntity>> _ret = this.FindList(sql, new Dictionary<string, object>());
            if (_ret.Result)
            {
                return _ret.ResultData;
            }
            return new List<ItemsDetailEntity>();
        }
    }
}
