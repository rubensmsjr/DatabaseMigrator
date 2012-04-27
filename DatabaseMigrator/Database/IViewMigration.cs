namespace DatabaseMigrator.Database
{
    public interface IViewMigration
    {
        IDBConnetion DBConnectionSource { get; set; }
        IDBConnetion DBConnectionTarget { get; set; }

        void Execute();
    }
}
