using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;

namespace DYong.Data
{
    /// <summary>
    /// 版 本 V1.0
    /// Copyright (c) 2015-2017 金鹏电子信息机器有限公司
    /// 创建人：DarkYong
    /// 日 期：2017.11.28
    /// 描 述：SQL参数化
    /// </summary>
    public class DbParameters
    {
        private static string DbType = "SqlServer";
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public static string CreateDbParmCharacter()
        {
            string character = string.Empty;
            switch (DbType)
            {
                case "SqlServer":
                    character = "@";
                    break;
                case "MySql":
                    character = "?";
                    break;
                case ".Access":
                    character = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return character;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter()
        {
            return new SqlParameter();
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter param = DbParameters.CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            DbParameter param = DbParameters.CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public static List<DbParameter> ToDbParameter(Dictionary<string,object> dbParameter)
        {
            //int i = 0;
            //int size = dbParameter.Count;
            DbParameter[] _dbParameter = null;
            #region
            switch (DbType)
            {
                case "SqlServer":
                    _dbParameter = DictionaryToSQLpar(dbParameter); 
                    break;
                case "MySql":
                    _dbParameter = DictionaryToMySQLpar(dbParameter);
                    break;
                case "Access":
                    _dbParameter = DictionaryToAccesspar(dbParameter);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            #endregion
            return _dbParameter.ToList();
        }

        #region 字典类型转参数列表
        /// <summary>
        /// 字典类型转参数列表【SQLService】
        /// </summary>
        /// <param name="ldb"></param>
        /// <returns></returns>
        private static DbParameter[] DictionaryToSQLpar(Dictionary<string, object> ldbp)
        {
            List<SqlParameter> opas = new List<SqlParameter>();
            foreach (var item in ldbp)
            {
                SqlParameter op = new SqlParameter();
                string paName = "@" + item.Key;
                op.ParameterName = paName;
                string typename = item.Value.GetType().ToString().ToUpper();
                #region .NET数据类型转换数据库属性类型
                switch (typename)
                {
                    case "SYSTEM.STRING":
                        if (item.Value.ToString().Length >= 8000)
                        {
                            op.SqlDbType = SqlDbType.Text;
                        }
                        else
                        {
                            op.SqlDbType = SqlDbType.VarChar;
                        }
                        op.Value = item.Value;
                        break;
                    case "SYSTEM.BYTE[]":
                        op.SqlDbType = SqlDbType.Variant;
                        op.Value = item.Value;
                        break;
                    case "SYSTEM.DATETIME":
                        op.SqlDbType = SqlDbType.DateTime;
                        op.Value = item.Value;

                        break;
                    default:
                        op.SqlDbType = SqlDbType.VarChar;
                        op.Value = item.Value;
                        break;
                }
                #endregion
                opas.Add(op);
            }
            return opas.ToArray();
        }
        /// <summary>
        /// 字典类型转参数列表【MySQL】
        /// </summary>
        /// <param name="ldb"></param>
        /// <returns></returns>
        private static DbParameter[] DictionaryToMySQLpar(Dictionary<string, object> ldbp)
        {
            List<MySqlParameter> opas = new List<MySqlParameter>();
            foreach (var item in ldbp)
            {
                MySqlParameter op = new MySqlParameter();
                string paName = "?" + item.Key;
                op.ParameterName = paName;
                string typename = item.Value.GetType().ToString().ToUpper();
                #region .NET数据类型转换数据库属性类型
                switch (typename)
                {
                    case "SYSTEM.STRING":
                        if (item.Value.ToString().Length >= 255)
                        {
                            op.MySqlDbType = MySqlDbType.VarChar;
                        }
                        else
                        {
                            op.MySqlDbType = MySqlDbType.VarChar;
                        }
                        op.Value = item.Value;
                        break;
                    case "SYSTEM.BYTE[]":
                        op.MySqlDbType = MySqlDbType.Byte;
                        op.Value = item.Value;
                        break;
                    case "SYSTEM.DATETIME":
                        op.MySqlDbType = MySqlDbType.Date;
                        op.Value = item.Value;
                        break;
                    default:
                        op.MySqlDbType = MySqlDbType.VarChar;
                        op.Value = item.Value;
                        break;
                }
                #endregion
                opas.Add(op);
            }
            return opas.ToArray();
        }
        /// <summary>
        /// 字典类型转参数列表【Access】
        /// </summary>
        /// <param name="ldb"></param>
        /// <returns></returns>
        private static DbParameter[] DictionaryToAccesspar(Dictionary<string, object> ldbp)
        {
            List<OleDbParameter> opas = new List<OleDbParameter>();
            foreach (var item in ldbp)
            {
                OleDbParameter op = new OleDbParameter();
                string paName = "@" + item.Key;
                op.ParameterName = paName;
                string typename = item.Value.GetType().ToString().ToUpper();
                #region .NET数据类型转换数据库属性类型
                switch (typename)
                {
                    case "SYSTEM.STRING":
                        if (item.Value.ToString().Length >= 255)
                        {
                            op.OleDbType = OleDbType.BSTR;
                        }
                        else
                        {
                            op.OleDbType = OleDbType.VarChar;
                        }
                        op.Value = item.Value;
                        break;
                    case "SYSTEM.BYTE[]":
                        op.OleDbType = OleDbType.Variant;
                        op.Value = item.Value;
                        break;
                    case "SYSTEM.DATETIME":
                        op.OleDbType = OleDbType.Date;
                        op.Value = item.Value;
                        break;
                    default:
                        op.OleDbType = OleDbType.VarChar;
                        op.Value = item.Value;
                        break;
                }
                #endregion
                opas.Add(op);
            }
            return opas.ToArray();
        }
        #endregion
    }
}
