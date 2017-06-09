namespace Ama.ObjectPools
{
    class SampleObject
    {
        byte[] data;

        public SampleObject(int size)
        {
            data = new byte[size];
        }
    }
}