using System;
using System.Collections.Generic;
using System.Threading;

namespace ShardingPoC
{
    public class ShardedCounter : ICounter
    {
        private readonly object _lock = new object();

        private List<Shard> _shards = new List<Shard>();

        private readonly LocalDataStoreSlot _slot = Thread.AllocateDataSlot();

        public long Count
        {
            get
            {
                long sum = 0;
                lock (_lock)
                {
                    foreach (Shard shard in _shards)
                    {
                        sum += shard.Count;
                    }
                }

                return sum;
            }
        }

        public void Increase(long amount)
        {
            Shard counter = Thread.GetData(_slot) as Shard;
            if (counter == null)
            {
                counter = new Shard()
                {
                    Owner = Thread.CurrentThread
                };
                Thread.SetData(_slot,counter);
                lock (_lock) { _shards.Add(counter);}
            }
            counter.Increase(amount);
        }
    }
}