using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoblibliotecario.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity, bool> condition = null);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}