namespace Ama.ObjectPools
{
    class SampleObject
    {
        byte[] data;
        internal static int Allocated { get; private set; }

        public SampleObject(int size)
        {
            if (MemoryTests.CreationCost > 0)
            {
                System.Threading.Thread.Sleep(MemoryTests.CreationCost);
            }
            data = new byte[size];
            Allocated++;
        }
    }
}