﻿using System.Data.Common;

namespace DatabaseMigrator.Database
{
    public interface IColumnMigrator
    {
        string GetSQLCreateColumnsInTable(DbConnection dbConnection, string tableName);

        string GetSQLSelectColumnsInView(string viewSelect, string viewName);
    }
}
