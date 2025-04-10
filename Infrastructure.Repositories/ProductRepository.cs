using Domain.Entities;
using Infrastructure.Configuration.Context;
using Infrastructure.Interface;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
