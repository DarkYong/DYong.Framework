using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DYong.Entity.Entitys 
{
    /// <summary>
    /// 数据表映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableClassInfo : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 物理主键
        /// </summary>
        public string PhysicalID { get; set; }
        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDescribtion { get; set; }
    }
    /// <summary>
    /// 数据属性映射
    /// </summary>
    public class TableCloumInfo : Attribute
    {
        /// <summary>
        /// 列属性名
        /// </summary>
        public string CloumName { get; set; }
        /// <summary>
        /// 列属性类型
        /// </summary>
        public string CloumType { get; set; }
    }


    /// <summary>
    /// 实体扩展类
    /// </summary>
    public static class EntityExt
    {
        /// <summary>
        /// 实体类转化为查询语句
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public static string EntityToSql<T>(this object value, Dictionary<string, object> pas) where T : class, new()
        {
            T entity = value as T;

            StringBuilder sql = new StringBuilder();//SQL语句
            StringBuilder strQuery = new StringBuilder();//查询字段
            StringBuilder strCondition = new StringBuilder();//条件字段
            var classAttribute = (TableClassInfo)Attribute.GetCustomAttribute(entity.GetType(), typeof(TableClassInfo));//利用反射获得类的映射
            PropertyInfo[] pros = entity.GetType().GetProperties();//利用反射获得属性的所有公共属性
            foreach (PropertyInfo pi in pros)
            {
                if (pi.GetCustomAttributes(typeof(NotMappedAttribute), false).Length > 0) { continue; }
                strQuery.Append(pi.Name + ",");//拼接字段
                if(pi.GetValue(entity, null) != null)
                {
                    strCondition.Append(pi.Name+" =@"+ pi.Name + " and ");
                    pas.Add(pi.Name, pi.GetValue(entity, null).ToString());
                }
            }
            sql.Append(" Select " + strQuery.ToString().Trim().TrimEnd(','));
            sql.Append(" From " + classAttribute.TableName + "  ");
            if (strCondition.ToString() != "")
            {
                string a = strCondition.ToString().Trim();
                sql.Append(" Where "+ a.Substring(0, a.Length-3));
            }
            return sql.ToString();
        }
       /// <summary>
       /// 实体类转化为插入语句
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="value"></param>
       /// <param name="pas"></param>
       /// <returns></returns>
        public static string EntityToAddSql<T>(this object value, Dictionary<string, object> pas) where T : class, new()
        {
            T entity = value as T;

            StringBuilder sql = new StringBuilder();//SQL语句
            StringBuilder strField = new StringBuilder();//数据字段
            StringBuilder strValue = new StringBuilder();//数据参数
            PropertyInfo[] pros = entity.GetType().GetProperties();//利用反射获得属性的所有公共属性
            var classAttribute = (TableClassInfo)Attribute.GetCustomAttribute(entity.GetType(), typeof(TableClassInfo));//利用反射获得类的映射
            foreach (PropertyInfo pi in pros)
            {
                if (pi.GetCustomAttributes(typeof(NotMappedAttribute), false).Length > 0) { continue; }
                //判断属性值是否为空
                if (pi.GetValue(entity, null) != null)
                {
                    strField.Append(pi.Name + ",");//拼接字段
                    strValue.Append("@" + pi.Name + ",");//声明参数
                    pas.Add(pi.Name, pi.GetValue(entity, null));//对参数赋值
                }
            }
            sql.Append("insert into " + classAttribute.TableName + "(");
            sql.Append(strField.ToString().Trim(','));
            sql.Append(") values (");
            sql.Append(strValue.ToString().Trim(','));
            sql.Append(") ");
            return sql.ToString();
        }
        /// <summary>
        /// 实体类转化为编辑语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public static string EntityToEditSql<T>(this object value, Dictionary<string, object> pas) where T : class, new()
        {
            T entity = value as T;

            string PhysicalIDValue = "";
            StringBuilder sql = new StringBuilder();//SQL语句
            StringBuilder strCondition = new StringBuilder();//更新字段
            var classAttribute = (TableClassInfo)Attribute.GetCustomAttribute(entity.GetType(), typeof(TableClassInfo));//利用反射获得类的映射
            PropertyInfo[] pros = entity.GetType().GetProperties();//利用反射获得属性的所有公共属性
            foreach (PropertyInfo pi in pros)
            {
                if (pi.GetCustomAttributes(typeof(NotMappedAttribute), false).Length > 0) { continue; }
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals(classAttribute.PhysicalID))
                {
                    //判断属性值是否为空
                    if (pi.GetValue(entity, null) != null)
                    {
                        strCondition.Append(pi.Name + " =@" + pi.Name + ", ");//拼接字段
                        pas.Add(pi.Name, pi.GetValue(entity, null));//对参数赋值
                    }
                }
                else
                {
                    PhysicalIDValue = pi.GetValue(entity, null).ToString();
                }
            }
            sql.Append(" UPDATE " + classAttribute.TableName + " SET ");
            sql.Append(strCondition.ToString().Trim().Trim(','));
            sql.Append(" where " + classAttribute.PhysicalID + "=@" + classAttribute.PhysicalID);
            pas.Add(classAttribute.PhysicalID, PhysicalIDValue);
            return sql.ToString();
        }
        /// <summary>
        /// 实体类转化为删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="pas"></param>
        /// <returns></returns>
        public static string EntityToDelSql<T>(this object value, Dictionary<string, object> pas) where T : class, new()
        {
            T entity = value as T;
            StringBuilder sql = new StringBuilder();//SQL语句
            StringBuilder strCondition = new StringBuilder();//删除字段
            var classAttribute = (TableClassInfo)Attribute.GetCustomAttribute(entity.GetType(), typeof(TableClassInfo));//利用反射获得类的映射
            PropertyInfo[] pros = entity.GetType().GetProperties();//利用反射获得属性的所有公共属性
            foreach (PropertyInfo pi in pros)
            {
                //判断属性值是否为空
                if (pi.GetValue(entity, null) != null)
                {
                    strCondition.Append(pi.Name + " =@" + pi.Name + " and ");//拼接字段
                    pas.Add(pi.Name, pi.GetValue(entity, null));//对参数赋值
                }
            }
            sql.Append(" DELETE FROM " + classAttribute.TableName + " where ");
            sql.Append(strCondition.ToString().Trim().Substring(0, strCondition.Length - 4));
            return sql.ToString();
        }
    }
}
