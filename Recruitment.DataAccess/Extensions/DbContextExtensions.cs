using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.DataAccess.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task AssertIsExist<TEntity>(this DbContext context, Guid id) where TEntity : BaseEntity
        {
            var set = context.Set<TEntity>();

            var exist = await set.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                throw new InvalidOperationException($"Not found {typeof(TEntity).Name}. Id: {id}");
            }
        }
    }
}
