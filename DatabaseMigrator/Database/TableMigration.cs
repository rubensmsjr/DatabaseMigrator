using System.Data;
using System.Data.Common;

namespace DatabaseMigrator.Database
{
    public class TableMigration:ITableMigration
    {
        public IDBConnetion DBConnectionSource { get; set; }
        public IDBConnetion DBConnectionTarget { get; set; }

        private IColumnMigrator columnMigrator;
        public TableMigration(IColumnMigrator columnMigrator)
        {
            this.columnMigrator = columnMigrator;
        }

        public void Execute()
        {
            if (!ConnectionsIsInitialized())
            {
                throw new DataException("Connections are not initialized.");
            }

            DataTable dataTable = (DBConnectionSource.Connection).GetSchema("TABLES",new string[]{null,null,null,"TABLE"} );
            
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DeleteTable(dataRow["TABLE_NAME"].ToString());
                CreateTable(dataRow["TABLE_NAME"].ToString());
            }
        }

        private bool ConnectionsIsInitialized()
        {
            if ((DBConnectionSource != null) && (DBConnectionTarget != null))
            {
                if ((!DBConnectionSource.IsInitialized) || (!DBConnectionTarget.IsInitialized))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private void DeleteTable(string tableName)
        {
            try
            {
                DbCommand dbCommand = DBConnectionTarget.Connection.CreateCommand();
                dbCommand.CommandText = string.Format("DROP TABLE {0}", tableName);
                dbCommand.CommandType = CommandType.Text;

                dbCommand.ExecuteNonQuery();
            }
            catch
            {

            }
        }

        private void CreateTable(string tableName)
        {
            try
            {
                DbCommand dbCommand = DBConnectionTarget.Connection.CreateCommand();
                dbCommand.CommandText = string.Format("CREATE TABLE {0} {1}", tableName, columnMigrator.GetSQLCreateColumnsInTable(DBConnectionSource.Connection,tableName));
                dbCommand.CommandType = CommandType.Text;

                dbCommand.ExecuteNonQuery();
            }
            catch
            {

            }
        }

        private void InsertRows(string tableName)
        {

            DbCommand dbCommand = DBConnectionSource.ProviderFactory.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM " + tableName;
            dbCommand.CommandType = CommandType.Text;

            DbDataAdapter dbDataAdapter = DBConnectionSource.ProviderFactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = dbCommand;

            DataSet dataSet = new DataSet();
            dbDataAdapter.Fill(dataSet);

            DbDataAdapter dbDataAdapterT = DBConnectionTarget.ProviderFactory.CreateDataAdapter();
            dbDataAdapterT.Update(dataSet);
        }
    }
}
