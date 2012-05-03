using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMigrator
{
    public interface IColumnName
    {
        string TableName { get; set; }

        string From { get; set; }
        string To { get; set; }
    }
}
