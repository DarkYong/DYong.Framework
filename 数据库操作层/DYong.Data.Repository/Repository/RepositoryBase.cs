using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using DYong.Data.Contract;
using System.Data.Entity;
using System.Reflection;
using System.Linq.Expressions;
using DYong.Code;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

namespace DYong.Data.Repository
{
    /// <summary>
    /// 仓储操作【主要用于事务执行】
    /// </summary>
    public class RepositoryBase: IRepositoryBase, IDisposable
    {
        private SqlServerDbContext dbcontext = new SqlServerDbContext();
        private DbTransaction dbTransaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IRepositoryBase BeginTrans()
        {
            DbConnection dbConnection = ((IObjectContextAdapter)dbcontext).ObjectContext.Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public ResultClass<int> Commit()
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                _ret.ResultData = dbcontext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                _ret.Result = true;
            }
            catch (Exception ex)
            {
                if (dbTransaction != null)
                {
                    this.dbTransaction.Rollback();
                }
                _ret.Result = false;
                _ret.ErrorMessage = ex.Message;
            }
            finally
            {
                this.Dispose();
            }
            return _ret;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<int> Insert<TEntity>(TEntity entity) where TEntity : class
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
            }
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
                return _ret;
            }
            return dbTransaction == null ? this.Commit() : _ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public ResultClass<int> Insert<TEntity>(List<TEntity> entitys) where TEntity : class
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
            }
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
                return _ret;
            }
           
            return dbTransaction == null ? this.Commit() : _ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<int> Update<TEntity>(TEntity entity) where TEntity : class
        {
            ResultClass<int> _ret = new ResultClass<int>();
            try
            {
                dbcontext.Set<TEntity>().Attach(entity);
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
            catch(Exception ex)
            {
                _ret.ErrorMessage = ex.Message;
                return _ret;
            }       
            return dbTransaction == null ? this.Commit() : _ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultClass<int> Delete<TEntity>(TEntity entity) where TEntity : class
        {
            ResultClass<int> _ret = new ResultClass<int>();
            dbcontext.Set<TEntity>().Attach(entity);
            dbcontext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return dbTransaction == null ? this.Commit() : _ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResultClass<int> Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            ResultClass<int> _ret = new ResultClass<int>();
            var entitys = dbcontext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => dbcontext.Entry<TEntity>(m).State = EntityState.Deleted);
            return dbTransaction == null ? this.Commit() : _ret;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ResultClass<TEntity> FindEntity<TEntity>(object keyValue) where TEntity : class
        {
            ResultClass<TEntity> _ret = new ResultClass<TEntity>();
            try
            {
                _ret.ResultData = dbcontext.Set<TEntity>().Find(keyValue);
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
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResultClass<TEntity> FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
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
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ResultClass<IQueryable<TEntity>> IQueryable<TEntity>() where TEntity : class
        {
            ResultClass<IQueryable<TEntity>> _ret = new ResultClass<IQueryable<TEntity>>();
            try
            {
                //DbSet<TEntity> tmp= dbcontext.Set<TEntity>();
                _ret.ResultData = dbcontext.Set<TEntity>();
                if (_ret.ResultData != null && _ret.ResultData.Count() > 0) _ret.Result = true;
                else _ret.ErrorMessage = "没有数据!";
            }
            catch (Exception ex)
            {
                _ret.ErrorMessage = ex.InnerException.Message;
            }

            return _ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResultClass<IQueryable<TEntity>> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
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
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList<TEntity>(string strSql) where TEntity : class
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
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList<TEntity>(string strSql, Dictionary<string, object> dbParameters) where TEntity : class
        {
            ResultClass<List<TEntity>> _ret = new ResultClass<List<TEntity>>();
            try
            {
                List<DbParameter> dbParameter = DbParameters.ToDbParameter(dbParameters);
                _ret.ResultData = dbcontext.Database.SqlQuery<TEntity>(strSql, dbParameter).ToList<TEntity>();
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
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList<TEntity>(Pagination pagination) where TEntity : class, new()
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
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public ResultClass<List<TEntity>> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class, new()
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

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (dbTransaction != null)
            {
                this.dbTransaction.Dispose();
            }
            this.dbcontext.Dispose();
        }
    }
}
