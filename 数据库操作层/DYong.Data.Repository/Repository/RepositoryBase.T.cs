using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DYong.Data.Contract;
using System.Linq.Expressions;
using DYong.Code;
using System.Data.Entity;
using System.Data.Common;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using DYong.Entity.Entitys;

namespace DYong.Data.Repository
{
    /// <summary>
    /// 仓储操作【主要用于单体执行】
    /// </summary>
    public class RepositoryBase<TEntity>: IRepositoryBase<TEntity> where TEntity : class, new()
    {
        private SqlServerDbContext dbcontext = new SqlServerDbContext();
        /// <summary>
        /// 插入一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<int> Insert(TEntity entity)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                //将改对象放入EF容器中，默认会为该对象加一个封装类对象（代理类对象）
                //用户对对象的操作，实际上是对代理类的操作
                //DbEntityEntry保存着实体状态，当对象被加入时，EF默认为该对象设置State的属性为unchanged
                dbcontext.Entry<TEntity>(entity).State = EntityState.Added;    //设置对象的标志位Added   
                PropertyInfo[] props = entity.GetType().GetProperties();       //获得插入对象的所有属性
                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetCustomAttributes(typeof(NotMappedAttribute), false).Length > 0) { continue; }//对非映射字段进行过滤
                    if (prop.GetValue(entity, null) == null)//对空字段进行过滤
                    {
                        dbcontext.Entry(entity).Property(prop.Name).IsModified = false;
                    }
                }
                _ret.ResultData= dbcontext.SaveChanges();//当调用SaveChanges()时，EF会遍历所有的代理类对象，并根据标志生成相应的sql语句

                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "插入数据失败!";
            }
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }        
            return _ret;
        }
        /// <summary>
        /// 插入一条信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public ResultClass<int> Insert(string strSql, Dictionary<string, object> dbParameters)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                List<DbParameter> dbParameter = DbParameters.ToDbParameter(dbParameters);
                _ret.ResultData = dbcontext.Database.ExecuteSqlCommand(strSql,dbParameter.ToArray());

                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "插入数据失败!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 插入多条信息
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public ResultClass<int> Insert(List<TEntity> entitys)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                foreach (var entity in entitys)
                {
                    dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
                    PropertyInfo[] props = entity.GetType().GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.GetCustomAttributes(typeof(NotMappedAttribute), false).Length > 0) { continue; }//对非映射字段进行过滤
                        if (prop.GetValue(entity, null) == null)
                        {
                            dbcontext.Entry(entity).Property(prop.Name).IsModified = false;
                        }
                    }
                }
                _ret.ResultData= dbcontext.SaveChanges();
                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "插入数据失败!";
            }
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 更新一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<int> Update(TEntity entity)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                dbcontext.Entry<TEntity>(entity).State = EntityState.Modified;
                PropertyInfo[] props = entity.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetCustomAttributes(typeof(NotMappedAttribute), false).Length>0){continue;}//对非映射字段进行过滤
                    if (prop.GetValue(entity, null) == null)
                    {
                        dbcontext.Entry(entity).Property(prop.Name).IsModified = false;
                    }
                }
                _ret.ResultData = dbcontext.SaveChanges();
                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "更新数据失败!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 更新一条信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public ResultClass<int> Update(string strSql, Dictionary<string, object> dbParameters)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                List<DbParameter> dbParameter = DbParameters.ToDbParameter(dbParameters);
                _ret.ResultData = dbcontext.Database.ExecuteSqlCommand(strSql, dbParameter.ToArray());

                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "更新数据失败!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<int> Delete(TEntity entity)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                dbcontext.Entry<TEntity>(entity).State = EntityState.Deleted;
                _ret.ResultData = dbcontext.SaveChanges();
                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "删除数据失败!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public ResultClass<int> Delete(string strSql, Dictionary<string, object> dbParameters)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                List<DbParameter> dbParameter = DbParameters.ToDbParameter(dbParameters);
                _ret.ResultData = dbcontext.Database.ExecuteSqlCommand(strSql, dbParameter.ToArray());
                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "删除数据失败!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResultClass<int> Delete(Expression<Func<TEntity, bool>> predicate)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                var entitys = dbcontext.Set<TEntity>().Where(predicate).ToList();
                entitys.ForEach(m => dbcontext.Entry<TEntity>(m).State = EntityState.Deleted);
                _ret.ResultData = dbcontext.SaveChanges();
                if (_ret.ResultData > 0) _ret.Result = true;
                else _ret.ErrorMessage = "删除数据失败!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }



        /// <summary>
        /// 根据主键查询一个对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ResultClass<TEntity> FindEntity(object keyValue)
        {
            ResultClass<TEntity> _ret = new ResultClass<TEntity>();
            try
            {
                _ret.ResultData= dbcontext.Set<TEntity>().Find(keyValue);
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 根据实体字段获得一个对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<TEntity> FindEntity(TEntity entity)
        {
            Dictionary<string, object> pas = new Dictionary<string, object>();
            string strSql = entity.EntityToSql<TEntity>(pas);
            return FindEntity(strSql, pas);
        }
        /// <summary>
        /// 根据SQL语句查询一个对象
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public ResultClass<TEntity> FindEntity(string strSql, Dictionary<string, object> dbParameters)
        {
            ResultClass<TEntity> _ret = new ResultClass<TEntity>();
            try
            {
                List<DbParameter> dbParameter = DbParameters.ToDbParameter(dbParameters);
                _ret.ResultData = dbcontext.Database.SqlQuery<TEntity>(strSql, dbParameter.ToArray()).FirstOrDefault();
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage="没有任何数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 根据树状表达式查询一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResultClass<TEntity> FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            ResultClass<TEntity> _ret = new ResultClass<TEntity>();
            try
            {
                _ret.ResultData = dbcontext.Set<TEntity>().FirstOrDefault(predicate);
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 查询所有对象列表
        /// </summary>
        /// <returns></returns>
        public ResultClass<IQueryable<TEntity>> IQueryable()
        { 
            ResultClass<IQueryable<TEntity>> _ret = new ResultClass<IQueryable<TEntity>>();
            try
            {
                //DbSet<TEntity> tmp= dbcontext.Set<TEntity>();
                _ret.ResultData = dbcontext.Set<TEntity>();
                if (_ret.ResultData != null&& _ret.ResultData.Count()>0) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.InnerException.Message;
            }
            
            return _ret;
        }
        /// <summary>
        /// 根据树状表达式查询所有对象列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResultClass<IQueryable<TEntity>> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            ResultClass<IQueryable<TEntity>> _ret = new ResultClass<IQueryable<TEntity>>();
            try
            {
                _ret.ResultData = dbcontext.Set<TEntity>().Where(predicate);
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }



        /// <summary>
        /// 根据SQL语句获得对象列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList(string strSql)
        {
            ResultClass<List<TEntity>> _ret = new ResultClass<List<TEntity>>();
            try
            {
                _ret.ResultData = dbcontext.Database.SqlQuery<TEntity>(strSql).ToList<TEntity>();
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 根据动态条件获得对象列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList(TEntity entity)
        {
            Dictionary<string, object> pas = new Dictionary<string, object>();
            string strSql = entity.EntityToSql<TEntity>(pas);
            return FindList(strSql, pas);
        }
        /// <summary>
        /// 根据SQL语句获得对象列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList(string strSql, Dictionary<string, object> dbParameters)
        {
            ResultClass<List<TEntity>> _ret = new ResultClass<List<TEntity>>();
            try
            {
                List<DbParameter> dbParameter = DbParameters.ToDbParameter(dbParameters);
                _ret.ResultData = dbcontext.Database.SqlQuery<TEntity>(strSql, dbParameter.ToArray()).ToList<TEntity>();
                if (_ret.ResultData != null&& _ret.ResultData.Count>0) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 根据SQL语句获得对象列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList(string strSql, Dictionary<string, object> dbParameters, Pagination pagination)
        {
            ResultClass<List<TEntity>> _ret = new ResultClass<List<TEntity>>();
            try
            {
                ResultClass<List<TEntity>> tmp = FindList(strSql, dbParameters);
                pagination.records = tmp.Result ? tmp.ResultData.Count : 0;
                if (pagination.records > 0)
                {
                    string StrSql = DatabasePage.GetStrSql(strSql, dbParameters, pagination);
                    _ret.ResultData = dbcontext.Database.SqlQuery<TEntity>(strSql,new List<DbParameter>()).ToList<TEntity>();
                    if (_ret.ResultData != null) _ret.Result = true;
                    else _ret.ErrorMessage = "没有数据!";
                }
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 获得对象列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList(Pagination pagination)
        {
            ResultClass<List<TEntity>> _ret = new ResultClass<List<TEntity>>();
            try
            {
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                MethodCallExpression resultExp = null;
                var tempData = dbcontext.Set<TEntity>().AsQueryable();
                foreach (string item in _order)
                {
                    string _orderPart = item;
                    _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                    string[] _orderArry = _orderPart.Split(' ');
                    string _orderField = _orderArry[0];
                    bool sort = isAsc;
                    if (_orderArry.Length == 2)
                    {
                        isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                    }
                    var parameter = Expression.Parameter(typeof(TEntity), "t");
                    var property = typeof(TEntity).GetProperty(_orderField);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                }
                tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
                pagination.records = tempData.Count();
                tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
                _ret.ResultData = tempData.ToList();
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
        /// <summary>
        /// 获得对象列表数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            ResultClass<List<TEntity>> _ret = new ResultClass<List<TEntity>>();
            try
            {
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                MethodCallExpression resultExp = null;
                var tempData = dbcontext.Set<TEntity>().Where(predicate);
                foreach (string item in _order)
                {
                    string _orderPart = item;
                    _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                    string[] _orderArry = _orderPart.Split(' ');
                    string _orderField = _orderArry[0];
                    bool sort = isAsc;
                    if (_orderArry.Length == 2)
                    {
                        isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                    }
                    var parameter = Expression.Parameter(typeof(TEntity), "t");
                    var property = typeof(TEntity).GetProperty(_orderField);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                }
                tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
                pagination.records = tempData.Count();
                tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
                _ret.ResultData = tempData.ToList();
                if (_ret.ResultData != null) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
            }
            return _ret;
        }
    }
}
