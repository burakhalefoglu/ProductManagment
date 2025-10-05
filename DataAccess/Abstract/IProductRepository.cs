
using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace DataAccess.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        Task<IEnumerable<Product>> GetListWithColorAsync(Expression<Func<Product, bool>> expression = null);
        Task<Product> GetWithColorAsync(Expression<Func<Product, bool>> expression = null);

    }
}