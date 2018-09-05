using System.Threading;

namespace ShardingPoC
{
   
        public class InterlockedCounter :ICounter
        {
            private long _count = 0;
            public long Count => Interlocked.CompareExchange(ref _count, 0, 0);
            public void Increase(long amount)
            {
                Interlocked.Add(ref _count, amount);
            }
        }
}