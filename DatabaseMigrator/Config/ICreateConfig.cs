namespace DatabaseMigrator.Config
{
    public interface ICreateConfig
    {
         DBConfig getBDConfig(string type);

         DBConfig getBDConfig(string type, string fileDBConfig);
    }
}
