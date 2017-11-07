using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.SS
{
    /// <summary>
    /// 整数主键基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SSIPKBaseLogic<TEntity> :SSCommonLogic where TEntity: class ,new ()
    {
        
        #region Add
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        public virtual void Add(TEntity model)
        {
            this.SSDB.Insertable<TEntity>(model).ExecuteReturnIdentity();
        }
        /// <summary>
        /// 返回受影响行数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int AddReturnColumnNumber(TEntity model)
        {
            return this.SSDB.Insertable<TEntity>(model).ExecuteCommand();
        }
        /// <summary>
        /// 返回自增列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int AddReutrnIdentity(TEntity model)
        {
            return this.SSDB.Insertable(model).ExecuteReturnIdentity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual long AddReutrnBigIdentity(TEntity model)
        {
            return this.SSDB.Insertable(model).ExecuteReturnBigIdentity(); //4.5.0.2 +
        }
        /// <summary>
        ///  4.2.3插入并返回实体 ,  只是自identity 添加到 参数的实体里面并返回，没有查2次库，所以有些默认值什么的变动是取不到的你们需要手动进行2次查询获取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TEntity AddReturnEntity(TEntity model)
        {
            return this.SSDB.Insertable(model).ExecuteReturnEntity();
        }
        /// <summary>
        /// 4.5.0.2 插入并返回bool, 并将identity赋值到实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool AddReturnBoolean(TEntity model)
        {
            return this.SSDB.Insertable(model).ExecuteCommandIdentityIntoEntity();
        }
        #endregion

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(int id)
        {
            return SSDB.Deleteable<TEntity>().In(id).ExecuteCommand();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual int BatchDelete(List<int> ids)
        {
            return SSDB.Deleteable<TEntity>().In(ids).ExecuteCommand();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int BatchDelete(Expression<Func<TEntity, bool>> where)
        {
            return SSDB.Deleteable<TEntity>().Where(where).ExecuteCommand();
        }

        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            return SSDB.Updateable(entity).ExecuteCommand();
        }
        #endregion

        #region Count
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this.SSDB.Queryable<TEntity>().Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return this.SSDB.Queryable<TEntity>().Count(where);
        } 
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity,bool>> expression)
        {
            return this.SSDB.Queryable<TEntity>().Any(expression);
        }
        #region GetEntity
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntity GetEntity(int Id)
        {
            return this.SSDB.Queryable<TEntity>().InSingle(Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity GetEntity(Expression<Func<TEntity, bool>> expression)
        {
            var res = this.SSDB.Queryable<TEntity>().Where(expression);
            if (res.Count() > 0)
            {
                return res.First();
            }
            else
            {
                return null;
            }

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISugarQueryable<TEntity> GetMany(Expression<Func<TEntity,bool>> expression)
        {

            return this.SSDB.Queryable<TEntity>().Where(expression);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            return this.SSDB.Queryable<TEntity>().ToList();
        }


    }
}
