namespace DatabaseMigrator
{
    public class ColumnName:IColumnName
    {
        public ColumnName() { }
        public ColumnName(string tableName, string from, string to)
        {
            this.TableName = tableName;
            this.From = from;
            this.To = to;
        }

        public string TableName { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ColumnName))
            {
                return false;
            }

            var columnName = obj as ColumnName;
            return ((this.TableName == columnName.TableName) && (this.To == columnName.To));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
