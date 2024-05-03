using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TechQ.MgaFiltering;

public static class EfExtensions
{
    public static IEnumerable<TReference> LoadReferences<T, TReference>(this DbContext context, T entity, Expression<Func<T, IEnumerable<TReference>>> selector)
        where T : class
        where TReference : class
    {
        context.Entry(entity).Reference(selector).Load();
        return new[] { entity }.AsQueryable().SelectMany(selector);
    }

    public static async Task<IEnumerable<TReference>> LoadReferencesAsync<T, TReference>(this DbContext context, T entity, Expression<Func<T, IEnumerable<TReference>>> selector, CancellationToken cancellationToken = default)
        where T : class
        where TReference : class
    {
        await context.Entry(entity).Reference(selector).LoadAsync(cancellationToken);
        return new[] { entity }.AsQueryable().SelectMany(selector);
    }


    public static TReference LoadReference<T, TReference>(this DbContext context, T entity, Expression<Func<T, TReference>> selector)
	    where T : class
	    where TReference : class
    {
	    context.Entry(entity).Reference(selector).Load();
	    return new[] { entity }.AsQueryable().Select(selector).Single();
    }
}