using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GuildfordBoroughCouncil.Linq
{
    public static partial class QueryExtensions
    {
        public static IEnumerable<TSource> TakeIf<TSource>(this IEnumerable<TSource> source, bool condition, int count)
        {
            if (condition)
                return source.Take(count);
            else
                return source;
        }

        public static IQueryable<TSource> TakeIf<TSource>(this IQueryable<TSource> source, bool condition, int count)
        {
            if (condition)
                return source.Take(count);
            else
                return source;
        }

        public static IEnumerable<TSource> TakeN<TSource>(this IEnumerable<TSource> source, int? count)
        {
            if (count.HasValue)
                return source.Take(count.Value);
            else
                return source;
        }

        public static IQueryable<TSource> TakeN<TSource>(this IQueryable<TSource> source, int? count)
        {
            if (count.HasValue)
                return source.Take(count.Value);
            else
                return source;
        }

        public static IEnumerable<TSource> TakeNIf<TSource>(this IEnumerable<TSource> source, bool condition, int? count)
        {
            if (condition && count.HasValue)
                return source.Take(count.Value);
            else
                return source;
        }

        public static IQueryable<TSource> TakeNIf<TSource>(this IQueryable<TSource> source, bool condition, int? count)
        {
            if (condition && count.HasValue)
                return source.Take(count.Value);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
    }
}
