using System.Collections.Generic;

namespace DatabaseMigrator
{
    public interface IConvertName
    {
        List<ITableName> ListTableName { get; set; }
        List<IColumnName> ListColumnName { get; set; }

        string Table(string tableName);
        string Column(string tableName, string columnName);
    }
}
