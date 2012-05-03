using System;
using System.Data;
using System.Data.Common;
using DatabaseMigrator.Logger;
using DatabaseMigrator.Resources;

namespace DatabaseMigrator.Database
{
    public class ViewMigration:IViewMigration
    {
        public IDBConnection DBConnectionSource { get; set; }
        public IDBConnection DBConnectionTarget { get; set; }

        private IConvertName convertName;
        private IColumnMigrator columnMigrator;
        private ILogger logger;

        public ViewMigration(IConvertName convertName, IColumnMigrator columnMigrator, ILogger logger)
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

            DataTable dataTable = DBConnectionSource.Connection.GetSchema("VIEWS");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string viewName = dataRow["TABLE_NAME"].ToString();
                string viewNameConvert = convertName.Table(viewName);

                logger.Info(string.Format(ResourceManager.GetMessage("MigratingView"), viewName));

                DeleteView(viewNameConvert);
                CreateView(viewNameConvert, dataRow["VIEW_DEFINITION"].ToString());

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

        private void DeleteView(string viewNameConvert)
        {
            try
            {
                DbCommand dbCommand = DBConnectionTarget.Connection.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = string.Format("DROP VIEW {0}", viewNameConvert);
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format(ResourceManager.GetMessage("LogDeleteView") + " | {1}", viewNameConvert, ex.Message));
            }
        }

        private void CreateView(string viewNameConvert, string viewSelect)
        {
            try
            {
                DbCommand dbCommand = DBConnectionTarget.Connection.CreateCommand();
                dbCommand.CommandText = string.Format("CREATE VIEW {0} AS {1}", viewNameConvert, columnMigrator.GetSQLSelectColumnsInView(DBConnectionSource.Connection, viewSelect));
                dbCommand.CommandType = CommandType.Text;
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format(ResourceManager.GetMessage("LogCreateTable") + " | {1}", viewNameConvert, ex.Message));
            }
        }
    }
}
