using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Base
{
    public abstract class EntityService<T> : IService<T> where T : class
    {
        protected RdsContext context { get; set; }
        protected DbSet<T> DbSet => context.Set<T>();
        public virtual Task<int> Count => DbSet.CountAsync();

        protected EntityService(RdsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        public virtual async Task<List<T>> GetAll()
        {
            try
            {
                var entities = await DbSet.ToListAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Фильтровать записи по предикату
        /// </summary>
        /// <param name="predicate">Типизированный предикат</param>
        public virtual async Task<List<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Фильтровать запись по предикату
        /// </summary>
        /// <param name="predicate">Типизированный предикат</param>
        public virtual async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Проверка наличия элемента в наборе данных
        /// </summary>
        /// <param name="predicate">Типизированный предикат</param>
        public async Task<bool> Contains(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.CountAsync(predicate) > 0;
        }

        /// <summary>
        /// Сохранить типизированный параметр
        /// </summary>
        /// <param name="TObject">Типизированный параметр</param>
        /// <returns>Типизированный результат</returns>
        public virtual async Task<T> Create(T entity)
        {
            try
            {
                var entry = DbSet.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="t">Типизированный параметр</param>
        /// <returns>Целочисленный результат SaveChangesAsync</returns>
        public virtual async Task<int> Delete(T t)
        {
            try
            {
                DbSet.Remove(t);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="predicate">Выражение предиката</param>
        /// <returns>Целочисленный результат SaveChangesAsync</returns>
        public virtual async Task<int> Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var objects = await Filter(predicate);
                foreach (var obj in objects)
                    DbSet.Remove(obj);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="t">Типизированный параметр</param>
        /// <returns>Целочисленный результат SaveChangesAsync</returns>
        public virtual async Task<int> Update(T t)
        {
            try
            {
                var entry = context.Entry(t);
                DbSet.Attach(t);
                entry.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
