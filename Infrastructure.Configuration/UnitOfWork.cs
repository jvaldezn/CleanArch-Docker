using System.Data;
using System;
using Transversal.Common.Interfaces;
using Infrastructure.Configuration.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
