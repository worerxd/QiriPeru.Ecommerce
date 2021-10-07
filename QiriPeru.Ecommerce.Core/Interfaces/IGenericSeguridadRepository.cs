using Microsoft.AspNetCore.Identity;
using QiriPeru.Ecommerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Interfaces
{
    public interface IGenericSeguridadRepository<T> where T : IdentityUser
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

        Task<int> Add(T entity);

        Task<int> Update(T entity);
    }
}
