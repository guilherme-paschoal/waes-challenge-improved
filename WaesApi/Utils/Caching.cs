using System;
using System.Collections.Generic;

namespace WaesApi.Utils
{
    public enum CacheDirection
    {
        LEFT,
        RIGHT
    }

    public static class CachingHelper
    {
        // Static/Singletons in a REST Api? Way to go huh?
        private static Dictionary<int, string> lefts = new Dictionary<int, string>();
        private static Dictionary<int, string> rights = new Dictionary<int, string>();

        public static void Add(int id, string data, CacheDirection direction)
        {
            if (direction == CacheDirection.LEFT) { lefts.Add(id, data); }
            if (direction == CacheDirection.RIGHT) { rights.Add(id, data); }
        }

        public static string Get(int id, CacheDirection direction)
        {
            if (direction == CacheDirection.LEFT) { return lefts[id]; }
            if (direction == CacheDirection.RIGHT) { return rights[id]; }
            return String.Empty;
        }

    }
}
