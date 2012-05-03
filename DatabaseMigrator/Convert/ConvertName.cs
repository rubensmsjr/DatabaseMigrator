using System.Collections.Generic;
namespace DatabaseMigrator
{
    public class ConvertName:IConvertName
    {
        private int auxCountName;

        private List<ITableName> ListTableName { get; set; }
        private List<IColumnName> ListColumnName { get; set; }

        public ConvertName()
        {
            ListTableName = new List<ITableName>();
            ListColumnName = new List<IColumnName>();
        }

        public string Table(string tableName)
        {
            if (ListTableName != null)
            {
                ITableName tableNameObject = ListTableName.FindLast(delegate(ITableName tn) { return tn.From == tableName; });
                if (tableNameObject != null)
                    return tableNameObject.To;
            }

            if (tableName.Length > 30)
            {
                auxCountName = 0;

                string convertedTableName = ChangeExistingTableName(tableName.Substring(0, 30));
                ListTableName.Add(new TableName(tableName, convertedTableName));

                return convertedTableName;
            }
            ListTableName.Add(new TableName(tableName, tableName));
            return tableName;
        }

        private string ChangeExistingTableName(string tableName)
        {
            if (ListTableName.Contains(new TableName(null, tableName)))
            {
                auxCountName++;
                tableName = ChangeExistingTableName(string.Format("{0}_{1}", tableName.Substring(0, 29 - auxCountName.ToString().Length), auxCountName));
            }

            return tableName;
        }

        public string Column(string tableName, string columnName)
        {
            if (ListColumnName != null)
            {
                IColumnName columnNameObject = ListColumnName.FindLast(delegate(IColumnName cn) { return (cn.TableName == tableName) && (cn.From == columnName); });
                if (columnNameObject != null)
                    return columnNameObject.To;
            }

            if (columnName.Length > 30)
            {
                auxCountName = 0;

                string convertedColumnName = ChangeExistingColumnName(tableName, columnName.Substring(0, 30));
                ListColumnName.Add(new ColumnName(tableName, columnName, convertedColumnName));

                return convertedColumnName;
            }

            ListColumnName.Add(new ColumnName(tableName, columnName, columnName));
            return columnName;
        }

        private string ChangeExistingColumnName(string tableName, string columnName)
        {
            if (ListColumnName.Contains(new ColumnName(tableName, null, columnName)))
            {
                auxCountName++;
                columnName = ChangeExistingColumnName(tableName, string.Format("{0}_{1}", columnName.Substring(0, 29 - auxCountName.ToString().Length), auxCountName));
            }

            return columnName;
        }
    }
}
