using System.Data;
using System.Data.Common;
using System;
using System.Collections.Generic;

namespace DatabaseMigrator.Database
{
    public class ColumnMigrator:IColumnMigrator
    {
        private IConvertName convertName;

        public ColumnMigrator(IConvertName convertName)
        {
            this.convertName = convertName;
        }

        public string GetSQLCreateColumnsInTable(DbConnection dbConnection, string tableName)
        {
            string sqlColumns = "";
            string convertedColumnName;

            DataTable dataTable = dbConnection.GetSchema("COLUMNS", new string[] { null, null, tableName, null });

            foreach (DataRow dataRow in dataTable.Rows)
            {
                convertedColumnName = convertName.Column(tableName, dataRow["COLUMN_NAME"].ToString());
                sqlColumns += GetSQLColumn(convertedColumnName, dataRow);
            }

            sqlColumns = sqlColumns.Remove(sqlColumns.Length - 1);

            return string.Format("({0})", sqlColumns);
        }

        private string GetSQLColumn(string convertedColumnName, DataRow dataRow)
        {
            return string.Format("{0} {1} {2},", convertedColumnName, GetColumnType(dataRow), GetNullable(dataRow));
        }

        private string GetNullable(DataRow dataRow)
        {
            if (!Convert.IsDBNull(dataRow["IS_NULLABLE"]))
            {
                if (!Convert.ToBoolean(dataRow["IS_NULLABLE"]))
                {
                    return "NOT NULL";
                }
            }
            return "";
        }

        private string GetColumnType(DataRow dataRow)
        {
            switch (Convert.ToInt32(dataRow["DATA_TYPE"]))
            {
                case 1:
                case 2:
                    return "INTEGER";

                case 3:
                {
                    switch (GetColumnSize(dataRow))
                    {
                        case -2:
                            return "FLOAT";

                        default:
                            return "INTEGER";
                    }
                }

                case 4:
                    return "SMALLINT";

                case 5:
                case 6:
                    return "NUMERIC";

                case 7:
                    return "TIMESTAMP";

                case 11:
                    return "CHAR(1)";
                
                //Oracle Type
                case 17:
                    return "BLOB";

                case 72:
                    return "VARCHAR(2000)";

                case 128:
                case 130:
                    if (GetColumnSize(dataRow) > 0)
                        return string.Format(" VARCHAR({0})", GetColumnSize(dataRow));
                    else
                        return "VARCHAR(2000)";

                case 131:
                    return string.Format("FLOAT");
            }
            return "VARCHAR(2000)";
        }

        private int GetColumnSize(DataRow dataRow)
        {
            if (!Convert.IsDBNull(dataRow["CHARACTER_MAXIMUM_LENGTH"]))
            {
                return Convert.ToInt32(dataRow["CHARACTER_MAXIMUM_LENGTH"]);
            }
            else
            {
                if (!Convert.IsDBNull(dataRow["CHARACTER_MAXIMUM_LENGTH"]))
                {
                    switch (Convert.ToInt32(dataRow["COLUMN_FLAGS"]))
                    {
                        case 122:
                            return -1;

                        case 90:
                            return -2;

                        default:
                            return -3;
                    }
                }
            }
            return 0;
        }

        private int GetNumericPrecision(DataRow dataRow)
        {
            if (!Convert.IsDBNull(dataRow["NUMERIC_PRECISION"]))
            {
                return Convert.ToInt32(dataRow["NUMERIC_PRECISION"]);
            }
            return 0;
        }

        private int GetNumericScale(DataRow dataRow)
        {
            if (!Convert.IsDBNull(dataRow["NUMERIC_SCALE"]))
            {
                return Convert.ToInt32(dataRow["NUMERIC_SCALE"]);
            }
            return 0;
        }

        public string GetSQLSelectColumnsInView(DbConnection dbConnection, string viewSelect)
        {
            string[] arrayViewSelect = GetArrayViewSelect(viewSelect);
            List<string> listTableName = GetListTableNameInSelectView(dbConnection, arrayViewSelect);

            foreach (string item in arrayViewSelect)
            {
                if (item.Length > 30)
                {
                    
                }
            }

            return viewSelect;
        }

        private List<string> GetListTableNameInSelectView(DbConnection dbConnection, string[] arrayViewSelect)
        {
            List<string> listTableName = new List<string>();
            DataTable dataTable = dbConnection.GetSchema("TABLES", new string[] { null, null, null, "TABLE" });

            foreach (DataRow dataRow in dataTable.Rows)
            {
                listTableName.Add(dataRow["TABLE_NAME"].ToString());
            }
            return listTableName;
        }

        private string[] GetArrayViewSelect(string viewSelect)
        {
            List<string> split = new List<string>();
            split.AddRange(GetCharacterSplitList(viewSelect, "\r\n"));
            split.AddRange(GetCharacterSplitList(viewSelect, " "));
            split.AddRange(GetCharacterSplitList(viewSelect, ","));
            split.AddRange(GetCharacterSplitList(viewSelect, "("));
            split.AddRange(GetCharacterSplitList(viewSelect, ")"));
            split.AddRange(GetCharacterSplitList(viewSelect, "="));
            split.AddRange(GetCharacterSplitList(viewSelect, "<"));
            split.AddRange(GetCharacterSplitList(viewSelect, ">"));
            split.AddRange(GetCharacterSplitList(viewSelect, ";"));

            return viewSelect.Split(split.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private List<string> GetCharacterSplitList(string text, string character)
        {
            List<string> split = new List<string>();
            if (text.Contains(character))
            {
                split.Add(character);
            }
            return split;
        }
    }
}
