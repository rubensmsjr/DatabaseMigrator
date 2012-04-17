namespace DatabaseMigrator.Config
{
    public class DBConfig
    {
        public DBConfig(){}
        public DBConfig(string client, string connectionString) 
        {
            this.Client= client;
            this.ConnectionString = connectionString;
        }
        
        public string Client { get; set; }

        public string ConnectionString { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is DBConfig))
            {
                return false;
            }

            var dbConfig = obj as DBConfig;
            return ((this.Client == dbConfig.Client) && (this.ConnectionString == dbConfig.ConnectionString));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
