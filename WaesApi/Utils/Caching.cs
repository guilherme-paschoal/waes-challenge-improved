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
        // Improvement: Instead of this Caching Singleton, have a Repository that will abstract the storage details away from this layer of code
        static readonly Dictionary<int, string> lefts = new Dictionary<int, string>();
        static readonly Dictionary<int, string> rights = new Dictionary<int, string>();

        public static void Add(int id, string data, CacheDirection direction) 
        {   
            // The reason I'm using the accessor to add an item to the dictionary instead of .Add is to overwrite the value if the key already exists
            // instead of throwing an exception. 
            if (direction == CacheDirection.LEFT) { lefts[id] = data; }
            if (direction == CacheDirection.RIGHT) { rights[id] = data; }
        }

        public static string Get(int id, CacheDirection direction)
        {
            if (direction == CacheDirection.LEFT) { return lefts[id]; }
            if (direction == CacheDirection.RIGHT) { return rights[id]; }
            return null;
        }

        public static bool KeyExists(int id) {
            return lefts.ContainsKey(id) && rights.ContainsKey(id);
        }

    }
}
