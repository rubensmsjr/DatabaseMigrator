using DatabaseMigrator.Config;

namespace DatabaseMigrator
{
    public interface IMigrator
    {
        void Execute();

        void Execute(string fileName);

        void Execute(DBConfig dbSource, DBConfig dbTarget);
    }
}
