using System.Threading;

namespace ShardingPoC
{
    internal class Shard : InterlockedCounter
    {
        public Thread Owner { get; set; }
    }
}