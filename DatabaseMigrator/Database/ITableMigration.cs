using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMigrator.Database
{
    public interface ITableMigration
    {
        IDBConnetion DBConnectionSource { get; set; }
        IDBConnetion DBConnectionTarget { get; set; }

        void Execute();
    }
}
