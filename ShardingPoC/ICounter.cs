namespace ShardingPoC
{
    public interface ICounter
    {
        void Increase(long amount);
        long Count { get; }
    }
}