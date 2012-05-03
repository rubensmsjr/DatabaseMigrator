namespace DatabaseMigrator
{
    public interface ITableName
    {
        string From { get; set; }
        string To { get; set; }
    }
}
