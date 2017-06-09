namespace Ama.ObjectPools
{
    class SampleObject
    {
        byte[] data;
        internal static int Allocated { get; private set; }

        public SampleObject(int size)
        {
            data = new byte[size];
            Allocated++;
        }
    }
}