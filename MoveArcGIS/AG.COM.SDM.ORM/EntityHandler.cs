using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace AG.COM.SDM.DAL
{
    /// <summary>
    /// 实体处理类,采用单例模式实现
    /// </summary>
    public class EntityHandler
    {
        private static Dictionary<string, EntityHandler> m_EntityDict = new Dictionary<string, EntityHandler>();
        private string m_AssemblyName;
        static readonly object padlock = new object();

        /// <summary>
        /// 创建实体处理类实例
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns>返回实体处理类</returns>
        public static EntityHandler CreateEntityHandler(string AssemblyName)
        {
            EntityHandler m_Entity = null;
            if (!m_EntityDict.ContainsKey(AssemblyName))
            {
                lock (padlock)
                {
                    if (!m_EntityDict.ContainsKey(AssemblyName))
                    {
                        m_Entity = new EntityHandler();
                        m_Entity.m_AssemblyName = AssemblyName;
                        m_EntityDict.Add(AssemblyName, m_Entity);
                    }
                }
            }
            m_Entity = m_EntityDict[AssemblyName];
            return m_Entity;
        }

        /// <summary>
        /// 添加指定的实体对象到数据表
        /// </summary>
        /// <param name="entity">指定要添加的实体对象</param>
        public void AddEntity(Object entity)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            session.Save(entity);
            transaction.Commit();
            //IChange change = entity as IChange;
            //if (change != null)
            //{
            //    change.MakeAsDefault();
            //}
            //catch (Exception ex)
            //{
            //    transaction.Rollback();
            //    throw new Exception(ex.Message, ex);
            //}

            session.Close();

        }

        /// <summary>
        /// 添加指定的实体对象集合到数据表
        /// </summary>
        /// <param name="listEntity">指定要添加的实体对象集合</param>
        public void AddListEntity(IList<object> listEntity)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            foreach (object entity in listEntity)
            {
                session.Save(entity);
                IChange change = entity as IChange;
                if (change != null)
                {
                    change.MakeAsDefault();
                }
            }
            transaction.Commit();
            session.Close();

        }

        /// <summary>
        /// 添加指定的实体对象集合到数据表
        /// </summary>
        /// <param name="listEntity">指定要添加的实体对象集合</param>
        public void AddListEntity<T>(IList<T> listEntity)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            foreach (T entity in listEntity)
            {
                session.Save(entity);
                IChange change = entity as IChange;
                if (change != null)
                {
                    change.MakeAsDefault();
                }
            }
            transaction.Commit();

            session.Close();

        }

        /// <summary>
        /// 更新指定的实体对象
        /// </summary>
        /// <param name="entity">指定更新的实体对象</param>
        /// <param name="key">实体对象关键值</param>
        public void UpdateEntity(Object entity, Object key)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            session.Update(entity, key);
            transaction.Commit();
            //IChange change = entity as IChange;
            //if (change != null)
            //{
            //    change.MakeAsDefault();
            //}

            session.Close();

        }

        /// <summary>
        /// 更新指定的实体对象集合
        /// </summary>
        /// <param name="listEntity">指定更新的实体对象集合</param>       
        public void UpdateEntity(IDictionary<object, object> listEntity)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            foreach (KeyValuePair<object, object> dictEntity in listEntity)
            {
                session.Update(dictEntity.Value, dictEntity.Key);
            }

            transaction.Commit();
            session.Close();

        }

        /// <summary>
        /// 保存或更新实体对象集合
        /// </summary>
        /// <param name="listentity">实体对象</param>
        public void SaveOrUpdateEntity(IList<object> listentity)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            foreach (object tEntity in listentity)
            {
                session.SaveOrUpdate(tEntity);
            }

            transaction.Commit();


            session.Close();

        }

        /// <summary>
        /// 插入或更新实体（key小于等于0时插入，否则更新）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="key">主键</param>
        public void SaveOrUpdate(object entity, int key)
        {
            SaveOrUpdate(entity, (decimal)key);
        }

        /// <summary>
        /// 插入或更新实体（key小于等于0时插入，否则更新）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="key">主键</param>
        public void SaveOrUpdate(object entity, decimal key)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            if (key <= 0)
            {
                session.Save(entity);
            }
            else
            {
                session.Update(entity, key);
            }
            transaction.Commit();
            IChange change = entity as IChange;
            if (change != null)
            {
                change.MakeAsDefault();
            }
            session.Close();
        }

        /// <summary>
        /// 批量插入或更新实体（key小于等于0时插入，否则更新）
        /// entities保存实体，keys保存主键，两者的Count必须一样
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="keys"></param>
        public void SaveOrUpdateEntites(IList<object> entities, IList<decimal> keys)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            ITransaction transaction = session.BeginTransaction();

            for (int i = 0; i < entities.Count; i++)
            {
                object entity = entities[i];
                decimal key = keys[i];

                if (key <= 0)
                {
                    session.Save(entity);
                }
                else
                {
                    session.Update(entity, key);
                }
            }

            transaction.Commit();
            session.Close();

        }

        #region 批量添加实体（带进度条）
        /// <summary>
        /// 批量添加实体（带进度条）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="trackProgress"></param>
        /// <param name="headMsg"></param>
        //public void AddEntityListProgress<T>(IList<T> entities, ITrackProgress trackProgress, string headMsg)
        //{
        //    ISession session = SessionFactory.OpenSession(m_AssemblyName);
        //    ITransaction transaction = session.BeginTransaction();

        //    trackProgress.SubValue = 0;
        //    trackProgress.SubMax = entities.Count;

        //    try
        //    {
        //        foreach (T entity in entities)
        //        {
        //            trackProgress.SubValue++;
        //            if (trackProgress.SubValue % 100 == 0)
        //            {
        //                trackProgress.SubMessage = headMsg + trackProgress.SubValue + " / " + trackProgress.SubMax;
        //                //Application.DoEvents();
        //            }

        //            session.Save(entity);                  
        //        }
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        throw new Exception(ex.Message, ex);
        //    }
        //    finally
        //    {
        //        session.Close();
        //    }
        //}
        #endregion

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void DeleteEntity(object entity)
        {
            //打开会话连接
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            //开始事务处理
            ITransaction transaction = session.BeginTransaction();

            session.Delete(entity);
            transaction.Commit();           //提交
            session.Close();                //结束会话

        }

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strHQL"></param>
        public void DeleteEntity<T>(string strHQL)
        {
            ///先查找出实体
            IList<T> deleteEntities = GetEntities<T>(strHQL);

            if (deleteEntities.Count < 1) return;

            //打开会话连接
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            //开始事务处理
            ITransaction transaction = session.BeginTransaction();


            ///再逐个删除
            foreach (T deleteEntity in deleteEntities)
            {
                session.Delete(deleteEntity);
            }
            transaction.Commit();           //提交
            session.Close();                //结束会话

        }

        /// <summary>
        /// 删除指定的实体对象集合
        /// </summary>
        /// <param name="listEntity">指定的实体对象集合</param>
        public void DeleteListEntity(IList<object> listEntity)
        {
            //打开会话连接
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            //开始事务处理
            ITransaction transaction = session.BeginTransaction();

            foreach (object entity in listEntity)
            {
                session.Delete(entity);
            }
            transaction.Commit();

            session.Close();

        }

        /// <summary>
        /// 根据指定的HQL查询语句获取实体集合
        /// </summary>
        /// <param name="strHQL">HQL查询语句</param>
        /// <returns>返回实体集合</returns> 
        public IList GetEntities(string strHQL)
        {
            IList lst;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            IQuery tQuery = session.CreateQuery(strHQL);
            lst = tQuery.List();

            MakeAsDefault(lst);



            session.Close();


            return lst;
        }

        /// <summary>
        /// 根据指定的HQL查询语句获取实体集合
        /// </summary>
        /// <param name="strHQL">HQL查询语句</param>
        /// <returns>返回实体集合</returns>
        public IList<T> GetEntities<T>(string strHQL)
        {
            return GetEntities<T>(strHQL, null);
        }

        /// <summary>
        /// 根据指定的HQL查询语句获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strHQL">HQL查询语句</param>
        /// <param name="paramValues">查询参数</param>
        /// <returns></returns>
        public IList<T> GetEntities<T>(string strHQL, params object[] paramValues)
        {
            IList<T> lst = new List<T>();

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            IQuery tQuery = session.CreateQuery(strHQL);
            
            if (paramValues != null)
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    tQuery.SetParameter(i, paramValues[i]);
                }
            }

            lst = tQuery.List<T>();
            //for (int i = 0; i < lst.Count; i++)
            //{
            //    IChange change = lst[0] as IChange;
            //    if (change != null)
            //        change.MakeAsDefault();
            //}

            session.Close();


            return lst;
        }

        /// <summary>
        /// 查询获取单个实体（若查询到多个结果则返回第一个）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strHQL">HQL查询语句</param>
        /// <returns></returns>
        public T GetEntity<T>(string strHQL)
        {
            IList<T> result = GetEntities<T>(strHQL);
            if(result.Count==0)
            {
                return (T)(object)null;
            }
            return result[0];
        }

        /// <summary>
        /// 查询获取单个实体（若查询到多个结果则返回第一个）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strHQL">HQL查询语句</param>
        /// <param name="paramValues">查询参数</param>
        /// <returns></returns>
        public T GetEntity<T>(string strHQL, params object[] paramValues)
        {
            IList<T> result = GetEntities<T>(strHQL, paramValues);
            if (result != null && result.Count > 0)
            {
                object o = result[0];
                IChange change = o as IChange;
                if (change != null)
                {
                    change.MakeAsDefault();
                }
                return (T)o;
            }
            else
                return (T)(object)null;
        }

        /// <summary>
        /// 查询所有实体（Linq方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetEntitiesAll<T>()
        {
            IList<T> lst = null;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            lst = session.Query<T>().ToList();

            //for (int i = 0; i < lst.Count; i++)
            //{
            //    IChange change = lst[0] as IChange;
            //    if (change != null)
            //        change.MakeAsDefault();
            //}

            session.Close();
            return lst;
        }

        /// <summary>
        /// 查询多个实体（QueryOver方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">QueryOver<T>对象</param>
        /// <returns></returns>
        public IList<T> GetEntitiesQueryOver<T>(QueryOver<T> query)
        {
            IList<T> lst = null;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            lst = query.GetExecutableQueryOver(session).List();
            for (int i = 0; i < lst.Count; i++)
            {
                IChange change = lst[0] as IChange;
                if (change != null)
                    change.MakeAsDefault();
            }



            session.Close();


            return lst;
        }

        /// <summary>
        /// 查询单个实体（QueryOver方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">QueryOver<T>对象</param>
        /// <returns></returns>
        public T GetEntityQueryOver<T>(QueryOver<T> query)
        {
            IList<T> result = GetEntitiesQueryOver<T>(query);
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
                return (T)(object)null;
        }

        /// <summary>
        /// 查询多个实体（Linq方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<T> GetEntitiesLinq<T>(Expression<Func<T, bool>> where)
        {
            return GetEntitiesLinqOrder<T>(where, null, null);
        }

        /// <summary>
        /// 查询多个实体（Linq方式）
        /// 带有条件，正序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderAsc"></param>
        /// <returns></returns>
        public IList<T> GetEntitiesLinqOrderAsc<T>(Expression<Func<T, bool>> where, Func<T, object> orderAsc)
        {
            return GetEntitiesLinqOrder<T>(where, orderAsc, null);
        }

        /// <summary>
        /// 查询多个实体（Linq方式）
        /// 带有条件，倒序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderDesc"></param>
        /// <returns></returns>
        public IList<T> GetEntitiesLinqOrderDesc<T>(Expression<Func<T, bool>> where, Func<T, object> orderDesc)
        {
            return GetEntitiesLinqOrder<T>(where, null, orderDesc);
        }

        /// <summary>
        /// 查询多个实体（Linq方式）
        /// 带有条件，正序排序，倒序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderAsc"></param>
        /// <param name="orderDesc"></param>
        /// <returns></returns>
        public IList<T> GetEntitiesLinqOrder<T>(Expression<Func<T, bool>> where, Func<T, object> orderAsc, Func<T, object> orderDesc)
        {
            IList<T> lst = null;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            IEnumerable<T> query = session.Query<T>().Where(where);

            //排序不一定有
            if (orderAsc != null)
            {
                query = query.OrderBy(orderAsc);
            }

            if (orderDesc != null)
            {
                query = query.OrderByDescending(orderDesc);
            }

            lst = query.ToList();

            for (int i = 0; i < lst.Count; i++)
            {
                IChange change = lst[0] as IChange;
                if (change != null)
                    change.MakeAsDefault();
            }


            session.Close();


            return lst;
        }

        /// <summary>
        /// 查询单个实体（Linq方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetEntityLinq<T>(Expression<Func<T, bool>> where)
        {
            IList<T> result = GetEntitiesLinq<T>(where);
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
                return (T)(object)null;
        }

        /// <summary>
        /// 使用通用Sql并返回实体
        /// </summary>
        /// <param name="strHQL">HQL查询语句</param>
        /// <returns>返回实体集合</returns>
        public IList<T> GetEntitiesUseSql<T>(string sql)
        {
            IList<T> lst;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            ///未测试                
            IQuery tQuery = session.CreateSQLQuery(sql).AddEntity(typeof(T));
            lst = tQuery.List<T>();

            for (int i = 0; i < lst.Count; i++)
            {
                IChange change = lst[i] as IChange;
                if (change != null)
                    change.MakeAsDefault();
            }


            session.Close();


            return lst;
        }

        /// <summary>
        /// 使用通用Sql并返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSQL"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public IList<T> GeneralSQLQuery<T>(string strSQL, params object[] paramValues)
        {
            ISQLQuery query = null;
            IList<T> result = null;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            query = session.CreateSQLQuery(strSQL).AddEntity(typeof(T));

            if (paramValues != null)
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    query.SetParameter(i, paramValues[i]);
                }
            }

            result = query.List<T>();


            session.Close();


            return result;
        }

        /// <summary>
        /// 使用通用Sql并返回object类型对象
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public IList GeneralQuery(string strSQL, params object[] paramValues)
        {
            IList lst;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            ISQLQuery query = session.CreateSQLQuery(strSQL);

            if (paramValues != null)
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    query.SetParameter(i, paramValues[i]);
                }
            }

            lst = query.List();


            session.Close();


            return lst;
        }


        /// <summary>
        /// 使用通用Sql并返回dataset
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string strSQL)
        {
            ISession session = null;
            DataSet ds = new DataSet();

            session = SessionFactory.OpenSession(m_AssemblyName);
            IDbCommand command = session.Connection.CreateCommand();
            command.CommandText = strSQL;
            IDataReader reader = command.ExecuteReader();
            DataTable result = new DataTable();
            DataTable schemaTable = reader.GetSchemaTable();
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                result.Columns.Add(schemaTable.Rows[i][0].ToString());
            }
            while (reader.Read())
            {
                int fieldCount = reader.FieldCount;
                object[] values = new Object[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    values[i] = reader.GetValue(i);
                }
                result.Rows.Add(values);
            }
            ds.Tables.Add(result);

            session.Close();

            return ds;

        }

        /// <summary>
        /// 查询字段的所有唯一值
        /// </summary>
        /// <typeparam name="T">字段类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public IList<T> GetUniqueValues<T>(string tableName, string fieldName)
        {
            IList<T> lst;

            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            //SELECT t.Field FROM Table t GROUP BY t.Field
            lst = session.CreateQuery("SELECT t." + fieldName + " FROM " + tableName + " t GROUP BY t." + fieldName + "")
                .List<T>();


            session.Close();


            return lst;
        }

        /// <summary>
        /// 类似ADO.NET的ExecuteNonQuery，通常用于删除或添加大量数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            int result = -1;
            //打开会话连接
            ISession session = SessionFactory.OpenSession(m_AssemblyName);
            //开始事务处理
            ITransaction transaction = session.BeginTransaction();


            //获取Command
            DbCommand command = session.Connection.CreateCommand() as DbCommand;
            //System.Data.IDbCommand command = session.Connection.CreateCommand() as DbCommand;
            //把Command绑定到事务
            transaction.Enlist(command);
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text;

            result = command.ExecuteNonQuery();
            transaction.Commit();
            session.Close();
            return result;




        }

        /// <summary>
        /// 获取NHiberante实体的表名
        /// </summary>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        public string GetEntityTableName(Type EntityType)
        {
            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            IClassMetadata classMeta = session.SessionFactory.GetClassMetadata(EntityType);
            return ((SingleTableEntityPersister)classMeta).TableName;

        }

        /// <summary>
        /// 设置为默认值
        /// </summary>
        /// <param name="lst"></param>
        private void MakeAsDefault(IList lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                IChange change = lst[i] as IChange;
                if (change != null)
                {
                    change.MakeAsDefault();
                }
            }
        }

        /// <summary>
        /// Depiction:分页
        /// </summary>
        /// <param name="pageStart"></param>
        /// <param name="pageLimit"></param>
        /// <returns></returns>
        public IList GetCustomerPageModel(string strSQL, int pageStart, int pageLimit)
        {
            IList Lst;
            ISession session = SessionFactory.OpenSession(m_AssemblyName);

            Lst = session.CreateQuery(strSQL).List();//.SetFirstResult(pageStart).SetMaxResults(pageLimit).List();
            session.Close();

            return Lst;
        }

    }
}
