using DYong.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DYong.Data;
using DYong.Data.Contract;

namespace DYong.Data.Repository
{
    /// <summary>
    /// 仓储接口【主要用于单体执行】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 插入一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultClass<int> Insert(TEntity entity);
        /// <summary>
        /// 插入一条信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        ResultClass<int> Insert(string strSql, Dictionary<string, object> dbParameter);
        /// <summary>
        /// 插入多条信息
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        ResultClass<int> Insert(List<TEntity> entitys);
        /// <summary>
        /// 更新一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultClass<int> Update(TEntity entity);
        /// <summary>
        /// 更新一条信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        ResultClass<int> Update(string strSql, Dictionary<string, object> dbParameter);
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultClass<int> Delete(TEntity entity);
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        ResultClass<int> Delete(string strSql, Dictionary<string, object> dbParameter);
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ResultClass<int> Delete(Expression<Func<TEntity, bool>> predicate);



        /// <summary>
        /// 根据主键查询一个对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        ResultClass<TEntity> FindEntity(object keyValue);
        /// <summary>
        /// 根据实体字段获得一个对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultClass<TEntity> FindEntity(TEntity entity);
        /// <summary>
        /// 根据SQL语句查询一个对象
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        ResultClass<TEntity> FindEntity(string strSql, Dictionary<string, object> dbParameter);
        /// <summary>
        /// 根据树状表达式查询一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ResultClass<TEntity> FindEntity(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询所有对象列表
        /// </summary>
        /// <returns></returns>
        ResultClass<IQueryable<TEntity>> IQueryable();
        /// <summary>
        /// 根据树状表达式查询所有对象列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ResultClass<IQueryable<TEntity>> IQueryable(Expression<Func<TEntity, bool>> predicate);



        /// <summary>
        /// 根据SQL语句获得对象列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        ResultClass<List<TEntity>> FindList(string strSql);
        /// <summary>
        /// 根据动态条件获得对象列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultClass<List<TEntity>> FindList(TEntity entity);
        /// <summary>
        /// 根据SQL语句获得对象列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        ResultClass<List<TEntity>> FindList(string strSql, Dictionary<string,object> dbParameter);
        /// <summary>
        /// 根据SQL语句获得对象列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        ResultClass<List<TEntity>> FindList(string strSql, Dictionary<string, object> dbParameter, Pagination pagination);
        /// <summary>
        /// 获得对象列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        ResultClass<List<TEntity>> FindList(Pagination pagination);
        /// <summary>
        /// 获得对象列表数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        ResultClass<List<TEntity>> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
    }
}
