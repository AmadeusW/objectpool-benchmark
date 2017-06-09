using System;
using System.Collections.Generic;
using System.Linq;

namespace Ama.ObjectPools
{
    class PooledLibrary : IDisposable
    {
        SampleObject[] collection;
        static LinkedList<SampleObject> pool = new LinkedList<SampleObject>();

        public PooledLibrary(int count, int size)
        {
            collection = new SampleObject[count];
            for (int i = 0; i < count; i++) 
            {
                if (pool.Any())
                {
                    collection[i] = pool.First();
                    pool.RemoveFirst();
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
                    pool.AddLast(collection[i]);
                    collection[i] = null;
                }
            }
            collection = null;
        }
    }
}