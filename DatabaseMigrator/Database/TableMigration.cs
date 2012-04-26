using System;
using System.Data;
using System.Data.Common;
using DatabaseMigrator.Logger;
using DatabaseMigrator.Resources;

namespace DatabaseMigrator.Database
{
    public class TableMigration:ITableMigration
    {
        public IDBConnetion DBConnectionSource { get; set; }
        public IDBConnetion DBConnectionTarget { get; set; }

        private IConvertName convertName;
        private IColumnMigrator columnMigrator;
        private ILogger logger;

        public TableMigration(IConvertName convertName, IColumnMigrator columnMigrator, ILogger logger)
        {
            this.convertName = convertName;
            this.columnMigrator = columnMigrator;
            this.logger = logger;
        }

        public void Execute()
        {
            if (!ConnectionsIsInitialized())
            {
                throw new DataException(ResourceManager.GetMessage("ConnectNotInitialized"));
            }

            DataTable dataTable = (DBConnectionSource.Connection).GetSchema("TABLES",new string[]{null,null,null,"TABLE"} );
            
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string tableName = dataRow["TABLE_NAME"].ToString();
                string tableNameConvert = convertName.Table(tableName);

                logger.Info(string.Format(ResourceManager.GetMessage("MigratingTable"), tableName));

                DeleteTable(tableNameConvert);
                CreateTable(tableName, tableNameConvert);
                InsertRows(tableName, tableNameConvert);

                logger.Info(ResourceManager.GetMessage("MigrationCompleted"));
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
            catch(Exception ex)
            {
                this.logger.Error(string.Format(ResourceManager.GetMessage("LogDeleteTable") + " | {1}", tableName, ex.Message));
            }
        }

        private void CreateTable(string tableName, string tableNameConvert)
        {
            try
            {
                DbCommand dbCommand = DBConnectionTarget.Connection.CreateCommand();
                dbCommand.CommandText = string.Format("CREATE TABLE {0} {1}", tableNameConvert, columnMigrator.GetSQLCreateColumnsInTable(DBConnectionSource.Connection, tableName));
                dbCommand.CommandType = CommandType.Text;
                dbCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                this.logger.Error(string.Format(ResourceManager.GetMessage("LogCreateTable")+ " | {1}", tableName, ex.Message);
            }
        }

        private void InsertRows(string tableName, string tableNameConvert)
        {
            try
            {
                PutRowsTargetDatabase(tableNameConvert,GetRowsSouceDatabase(tableName));
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format(ResourceManager.GetMessage("LogInsertRows")+ " | {1}", tableName, ex.Message));
            }
        }

        private DataSet GetRowsSouceDatabase(string tableName)
        {
            DbCommand dbCommand = DBConnectionSource.Connection.CreateCommand();
            dbCommand.CommandText = string.Format("SELECT * FROM {0}" , tableName);
            dbCommand.CommandType = CommandType.Text;

            DbDataAdapter dbDataAdapter = DBConnectionSource.ProviderFactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = dbCommand;

            DataSet dataSet = new DataSet();
            dbDataAdapter.Fill(dataSet);

            return dataSet;
        }

        private void PutRowsTargetDatabase(string tableNameConvert, DataSet dataSet)
        {
            foreach (DataColumn dataColumn in dataSet.Tables[0].Columns)
                dataColumn.ColumnName = convertName.Column(tableNameConvert, dataColumn.ColumnName);

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                dataRow.SetAdded();

            DbCommand dbCommand = DBConnectionTarget.Connection.CreateCommand();
            dbCommand.CommandText = string.Format("SELECT * FROM {0}", tableNameConvert);
            dbCommand.CommandType = CommandType.Text;

            DbDataAdapter dbDataAdapter = DBConnectionTarget.ProviderFactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = dbCommand;

            DbCommandBuilder dbCommandBuilder = DBConnectionTarget.ProviderFactory.CreateCommandBuilder();
            dbCommandBuilder.DataAdapter = dbDataAdapter;

            dbDataAdapter.InsertCommand = dbCommandBuilder.GetInsertCommand();
            dbDataAdapter.Update(dataSet);
        }
    }
}
