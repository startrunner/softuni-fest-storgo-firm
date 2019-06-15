using System;
using System.Collections.Generic;

namespace StorgoFirm.Extensions
{
    public static class EnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            var set = new HashSet<T>();
            foreach (T item in items) set.Add(item);
            return set;
        }

        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> items)
        {
            var set = new SortedSet<T>();
            foreach (T item in items) set.Add(item);
            return set;
        }

        public static IEnumerable<TItem> DistinctBy<TItem, TDistinctBy>(
            this IEnumerable<TItem> items,
            Func<TItem, TDistinctBy> selector
        )
        {
            var unique = new Dictionary<TDistinctBy, TItem>();

            foreach (TItem item in items)
            {
                TDistinctBy selected = selector.Invoke(item);
                if (!unique.ContainsKey(selected))
                    unique.Add(selected, item);
            }

            return unique.Values;
        }
    }
}
