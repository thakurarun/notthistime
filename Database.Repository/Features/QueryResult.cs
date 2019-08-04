namespace Database.Repository.Features
{
    public abstract class QueryResult
    {
        public bool Success { get; internal set; }
        public int AffectedRows { get; internal set; }
    }
}
