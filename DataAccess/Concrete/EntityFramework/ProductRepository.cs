
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : EfEntityRepositoryBase<Product, ProjectDbContext>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context) : base(context)
        {
        }

        
        
        public async Task<IEnumerable<Product>> GetListWithColorAsync(Expression<Func<Product, bool>> expression = null)
        {
            return expression == null
                ? await Context.Set<Product>()
                    .Include(p => p.Colors).ToListAsync()
                : await Context.Set<Product>()
                    .Include(p => p.Colors).Where(expression).ToListAsync();
        }

        public async Task<Product> GetWithColorAsync(Expression<Func<Product, bool>> expression = null)
        {
            return await Context.Set<Product>().Include(p => p.Colors).AsQueryable().FirstOrDefaultAsync(expression);
        }
    }
}
