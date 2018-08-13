using System;
using System.Linq;

namespace Model.Utilities
{
    public static class ObjectExtensions
    {
        public static bool In<T>(this T obj, params T[] args) where T : IComparable
        {
            return args.Contains(obj);
        }
    }
}
