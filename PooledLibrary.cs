using System;
using System.Collections.Generic;
using System.Linq;

namespace Ama.ObjectPools
{
    class PooledLibrary : IDisposable
    {
        SampleObject[] collection;
        static List<SampleObject> pool = new List<SampleObject>();

        public PooledLibrary(int count, int size)
        {
            collection = new SampleObject[count];
            for (int i = 0; i < count; i++) 
            {
                if (pool.Any())
                {
                    collection[i] = pool.First();
                    pool.RemoveAt(0);
                }
                else
                {
                    collection[i] = new SampleObject(size);
                }
            }
        }

        void IDisposable.Dispose()
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i] != null)
                {
                    pool.Add(collection[i]);
                    collection[i] = null;
                }
            }
            collection = null;
        }
    }
}