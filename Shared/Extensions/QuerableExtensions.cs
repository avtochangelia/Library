using Shared.Abstractions;
using System.Linq.Expressions;

namespace Shared.Extensions;

public static class QuerableExtensions
{
    public static IQueryable<TSource> ContainsIgnoreCase<TSource>(this IQueryable<TSource> source, string? candidate, params string[] columnNames)
    {
        if (string.IsNullOrEmpty(candidate) || columnNames == null || columnNames.Length == 0)
        {
            return source;
        }

        var parameterExp = Expression.Parameter(typeof(TSource), "x");
        var value = Expression.Constant(candidate.ToLower(), typeof(string));
        var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)])!;
        var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;

        var containsMethodExpressions = new List<Expression>();

        foreach (var columnName in columnNames)
        {
            Expression propInfo = parameterExp;

            foreach (var item in columnName.Split('.'))
            {
                propInfo = Expression.PropertyOrField(propInfo, item);
            }

            var lowerMethodExp = Expression.Call(propInfo, toLowerMethod);
            var containsMethodExp = Expression.Call(lowerMethodExp, containsMethod, value);

            containsMethodExpressions.Add(containsMethodExp);
        }

        Expression? conditionExpression = null;

        foreach (var methodCallExpression in containsMethodExpressions)
        {
            conditionExpression = conditionExpression == null
                ? methodCallExpression
                : Expression.OrElse(conditionExpression, methodCallExpression);
        }

        var predicate = Expression.Lambda<Func<TSource, bool>>(conditionExpression!, parameterExp);
        return And(source, predicate);
    }

    public static IQueryable<TSource> And<TSource, TFilter>(this IQueryable<TSource> source, TFilter? filter, Expression<Func<TSource, bool>> predicate)
        where TFilter : struct
    {
        if (predicate == null)
        {
            return source;
        }

        if (!filter.HasValue)
        {
            return source;
        }

        return And(source, predicate);
    }

    private static IQueryable<TSource> And<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
        if (predicate == null)
        {
            return source;
        }

        return source.Where(predicate);
    }

    public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, IPaginationRequest request)
    {
        if (request == null)
        {
            return source;
        }

        request.PageSize ??= 10;
        request.Page ??= 1;

        return source.Skip(request.PageSize.Value * (request.Page.Value - 1)).Take(request.PageSize.Value);
    }

    public static async Task<IQueryable<TSource>> PaginationAsync<TSource>(this IQueryable<TSource> source, IPaginationRequest request)
    {
        if (request == null)
        {
            return source;
        }

        request.PageSize ??= 10;
        request.Page ??= 1;

        return await Task.FromResult(source.Skip(request.PageSize.Value * (request.Page.Value - 1)).Take(request.PageSize.Value));
    }
}