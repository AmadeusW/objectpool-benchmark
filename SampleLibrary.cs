using System;

namespace A6z.ObjectPools
{
    class SampleLibrary
    {
        public SampleLibrary(int count, int size)
        {
            var collection = new SampleObject[count];
            for (int i = 0; i < count; i++) 
            {
                collection[i] = new SampleObject(size);
            }
        }
    }

    class SampleObject
    {
        byte[] data;

        public SampleObject(int size)
        {
            data = new byte[size];
        }
    }
}
