namespace DatabaseMigrator
{
    public class TableName:ITableName
    {
        public TableName(string from, string to)
        {
            this.From = from;
            this.To = to;
        }

        public string From { get; set; }
        public string To { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TableName))
            {
                return false;
            }
            var tableName = obj as TableName;

            return (this.To == tableName.To);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
