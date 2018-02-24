using System.Collections.Generic;
using Woodman.Extensions.Internal;

namespace System.Linq
{
    public static class IEnumerableExtensions
    {
        public static List<TSource> ToListSafe<TSource>(this IEnumerable<TSource> source)
        {
            return source == null ? new List<TSource>() : source.ToList();
        }

        public static Dictionary<TKey, TSource> ToDictionarySafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source == null
                ? new Dictionary<TKey, TSource>()
                : source.ToDictionary(keySelector);
        }

        public static Dictionary<TKey, TElement> ToDictionarySafe<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return source == null
                ? new Dictionary<TKey, TElement>()
                : source.ToDictionary(keySelector, elementSelector);
        }

        public static HashSet<TSource> ToHashSetSafe<TSource>(this IEnumerable<TSource> source)
        {
            return source == null
                ? new HashSet<TSource>()
                : new HashSet<TSource>(source);
        }

        public static HashSet<TKey> ToHashSetSafe<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source == null
                ? new HashSet<TKey>()
                : new HashSet<TKey>(source.Select(item => keySelector(item)));
        }

        public static (List<TSource> Left, List<TSource> Right, List<TSource> Intersect) Compare<TSource>(this IEnumerable<TSource> a, IEnumerable<TSource> b)
        {
            var aList = a.ToListSafe();
            var bList = b.ToListSafe();

            var left = aList.Except(bList).ToListSafe();
            var right = bList.Except(aList).ToListSafe();
            var intersect = aList.Except(left).ToListSafe();

            return (left, right, intersect);
        }

        public static (List<TSource> Left, List<TSource> Right, List<TSource> Intersect) Compare<TSource, TKey>(this IEnumerable<TSource> a, IEnumerable<TSource> b, Func<TSource, TKey> keySelector)
        {
            var aList = a.ToListSafe();
            var bList = b.ToListSafe();

            var comparer = new KeyComparer<TSource, TKey>(keySelector);

            var left = aList.Except(bList, comparer).ToListSafe();
            var right = bList.Except(aList, comparer).ToListSafe();
            var intersect = aList.Except(left, comparer).ToListSafe();

            return (left, right, intersect);
        }
    }
}
