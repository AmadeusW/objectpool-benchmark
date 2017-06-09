using System;

namespace Ama.ObjectPools
{
    class SampleLibrary : IDisposable
    {
        SampleObject[] collection;

        public SampleLibrary(int count, int size)
        {
            collection = new SampleObject[count];
            for (int i = 0; i < count; i++) 
            {
                collection[i] = new SampleObject(size);
            }
        }

        void IDisposable.Dispose()
        {
            collection = null;
        }
    }
}
