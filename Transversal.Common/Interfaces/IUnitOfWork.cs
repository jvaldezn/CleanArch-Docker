using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Common.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
