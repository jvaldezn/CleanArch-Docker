using Domain.Entities;
using Transversal.Common.Interfaces;

namespace Infrastructure.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // Métodos adicionales específicos para productos (si son necesarios)
    }
}
