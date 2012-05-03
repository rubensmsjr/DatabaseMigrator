using System.Collections.Generic;

namespace DatabaseMigrator
{
    public interface IConvertName
    {
        string Table(string tableName);
        string Column(string tableName, string columnName);
    }
}
