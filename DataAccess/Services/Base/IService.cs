using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Base
{
    public interface IService<T> where T : class
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        Task<List<T>> GetAll();

        /// <summary>
        /// Отфильтровать записи по параметру
        /// </summary>
        Task<List<T>> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Отфильтровать запись по параметру
        /// </summary>
        Task<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Проверка наличия элемента в наборе данных
        /// </summary>
        Task<bool> Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Создать запись
        /// </summary>
        Task<T> Create(T t);

        /// <summary>
        /// Удалить запись
        /// </summary>
        Task<int> Delete(T t);

        /// <summary>
        /// Удалить запись по параметру
        /// </summary>
        Task<int> Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="t">Запись</param>
        /// <returns>Идентификатор</returns>
        Task<int> Update(T t);

        /// <summary>
        /// Получить количество записей
        /// </summary>
        Task<int> Count { get; }
    }
}
