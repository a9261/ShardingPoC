﻿namespace ShardingPoC
{
    public class UnsynchronizedCounter : ICounter
    {
        private long _count = 0;
        private static readonly object _lock = new object();

        public long Count
        {
            get
            {
//                lock (_lock) { }
                return _count;
            }
        }

        public void Increase(long amount)
        {
            lock (_lock)
            {
                _count += amount;
            }
        }
    }
}