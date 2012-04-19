using System.Data;
using System.Data.Common;
using System;

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
                InsertRows(dataRow["TABLE_NAME"].ToString());
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
            try
            {
                DbCommand dbCommandSource = DBConnectionSource.ProviderFactory.CreateCommand();
                dbCommandSource.CommandText = "SELECT * FROM " + tableName;
                dbCommandSource.CommandType = CommandType.Text;
                dbCommandSource.Connection = DBConnectionSource.Connection;

                DbDataAdapter dbDataAdapterSource = DBConnectionSource.ProviderFactory.CreateDataAdapter();
                dbDataAdapterSource.SelectCommand = dbCommandSource;

                DataSet dataSet = new DataSet();
                dbDataAdapterSource.Fill(dataSet);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    dataRow.SetAdded();

                DbCommand dbCommandTarget = DBConnectionTarget.ProviderFactory.CreateCommand();
                dbCommandTarget.CommandText = "SELECT * FROM " + tableName;
                dbCommandTarget.CommandType = CommandType.Text;
                dbCommandTarget.Connection = DBConnectionTarget.Connection;

                DbDataAdapter dbDataAdapterTarget = DBConnectionTarget.ProviderFactory.CreateDataAdapter();
                dbDataAdapterTarget.SelectCommand = dbCommandTarget;

                DbCommandBuilder dbCommandBuilder = DBConnectionTarget.ProviderFactory.CreateCommandBuilder();
                dbCommandBuilder.DataAdapter = dbDataAdapterTarget;

                dbDataAdapterTarget.InsertCommand = dbCommandBuilder.GetInsertCommand();
                dbDataAdapterTarget.Update(dataSet);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
