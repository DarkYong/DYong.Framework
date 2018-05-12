using DYong.Code;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
namespace DYong.Data
{
    public static class DatabasePage
    {
        private static string DbType = "SqlServer";
        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public static string GetStrSql(string strSql,Dictionary<string,object> dbParameter,Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            int pageSize = pagination.rows;
            int pageIndex = pagination.page;
            string result = "";
            #region
            switch (DbType)
            {
                case "SqlServer":
                    result = SqlPageSql(strSql, dbParameter, pagination.sidx, isAsc, pageSize, pageIndex).ToString();
                    break;
                case "MySql":
                    result = MySqlPageSql(strSql, dbParameter, pagination.sidx, isAsc, pageSize, pageIndex).ToString();
                    break;
                case "Access":
                    result = MySqlPageSql(strSql, dbParameter, pagination.sidx, isAsc, pageSize, pageIndex).ToString();
                    break;
                case "Oracle":
                    result = OraclePageSql(strSql, dbParameter, pagination.sidx, isAsc, pageSize, pageIndex).ToString();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            #endregion
            return result;
        }


        /// <summary>
        /// MySqlPageSql
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="orderField"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static StringBuilder MySqlPageSql(string strSql, Dictionary<string,object> dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder builder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string str = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                if ((orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC")) > 0)
                {
                    str = " Order By " + orderField;
                }
                else
                {
                    str = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            builder.Append(strSql + str);
            builder.Append(string.Concat(new object[] { " limit ", num, ",", pageSize }));
            return builder;
        }
        /// <summary>
        /// OraclePageSql
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="orderField"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static StringBuilder OraclePageSql(string strSql, Dictionary<string, object> dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder builder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            string str = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                if ((orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC")) > 0)
                {
                    str = " Order By " + orderField;
                }
                else
                {
                    str = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            builder.Append("Select * From (Select ROWNUM,");
            builder.Append(string.Concat(new object[] { " T.* From (", strSql, str, ")  T )  N Where rowNum > ", num, " And rowNum <= ", num2 }));
            return builder;
        }
        /// <summary>
        /// SqlPageSql        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="orderField"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static StringBuilder SqlPageSql(string strSql, Dictionary<string, object> dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder builder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            string str = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                if ((orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC")) > 0)
                {
                    str = " Order By " + orderField;
                }
                else
                {
                    str = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                str = "order by (select 0)";
            }
            builder.Append("Select * From (Select ROW_NUMBER() Over (" + str + ")");
            builder.Append(string.Concat(new object[] { " As rowNum, * From (", strSql, ")  T ) As N Where rowNum > ", num, " And rowNum <= ", num2 }));
            return builder;
        }

    }
}

