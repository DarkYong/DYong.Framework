using DYong.Code;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DYong.Data.Contract;

namespace DYong.Data.Repository
{
    /// <summary>
    /// 仓储接口【主要用于事务执行】
    /// </summary>
    public interface IRepositoryBase : IDisposable
    {
        IRepositoryBase BeginTrans();
        ResultClass<int> Commit();


        ResultClass<int> Insert<TEntity>(TEntity entity) where TEntity : class;
        //ResultClass<int> Insert(string strSql, Dictionary<string, object> dbParameter);
        ResultClass<int> Insert<TEntity>(List<TEntity> entitys) where TEntity : class;
        ResultClass<int> Update<TEntity>(TEntity entity) where TEntity : class;
        ResultClass<int> Delete<TEntity>(TEntity entity) where TEntity : class;
        ResultClass<int> Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;



        ResultClass<TEntity> FindEntity<TEntity>(object keyValue) where TEntity : class;
        ResultClass<TEntity> FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        ResultClass<IQueryable<TEntity>> IQueryable<TEntity>() where TEntity : class;
        ResultClass<IQueryable<TEntity>> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;




        ResultClass<List<TEntity>> FindList<TEntity>(string strSql) where TEntity : class;
        ResultClass<List<TEntity>> FindList<TEntity>(string strSql, Dictionary<string, object> dbParameter) where TEntity : class;
        ResultClass<List<TEntity>> FindList<TEntity>(Pagination pagination) where TEntity : class, new();
        ResultClass<List<TEntity>> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class, new();
    }
}
