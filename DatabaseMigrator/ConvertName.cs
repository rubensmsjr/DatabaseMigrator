using System.Collections.Generic;
namespace DatabaseMigrator
{
    public class ConvertName:IConvertName
    {
        private List<string> listNameTable = new List<string>();
        private List<Column> listNameColumn = new List<Column>();
        private int auxCountName;

        public string Table(string tableName)
        {
            if (tableName.Length > 30)
            {
                auxCountName = 0;

                string name = ChangeExistingTableName(tableName.Substring(0, 30));
                listNameTable.Add(name);

                return name;
            }
            return tableName;
        }

        private string ChangeExistingTableName(string tableName)
        {
            if (listNameTable.Contains(tableName))
            {
                auxCountName++;
                tableName = ChangeExistingTableName(string.Format("{0}_{1}", tableName.Substring(0, 29 - auxCountName.ToString().Length), auxCountName));
            }

            return tableName;
        }

        public string Column(string nameTable, string nameColumn)
        {
            if (nameColumn.Length > 30)
            {
                auxCountName = 0;

                string name = ChangeExistingColumnName(nameTable, nameColumn.Substring(0, 30));
                listNameColumn.Add(new Column(nameTable, name));

                return name;
            }
            return nameColumn;
        }

        private string ChangeExistingColumnName(string tableName, string nameColumn)
        {
            if (listNameColumn.Contains(new Column(tableName, nameColumn)))
            {
                auxCountName++;
                nameColumn = ChangeExistingColumnName(tableName, string.Format("{0}_{1}", nameColumn.Substring(0, 29 - auxCountName.ToString().Length), auxCountName));
            }

            return nameColumn;
        }
    }

    public class Column
    {
        public Column(){}
        public Column(string nameTable, string nameColumn) 
        {
            this.NameTable = nameTable;
            this.NameColumn = nameColumn;
        }
        
        public string NameTable { get; set; }

        public string NameColumn { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Column))
            {
                return false;
            }

            var column = obj as Column;
            return ((this.NameTable == column.NameTable) && (this.NameColumn == column.NameColumn));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
