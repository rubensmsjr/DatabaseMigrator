namespace DatabaseMigrator.Database
{
    public interface IViewMigration
    {
        IDBConnection DBConnectionSource { get; set; }
        IDBConnection DBConnectionTarget { get; set; }

        void Execute();
    }
}
