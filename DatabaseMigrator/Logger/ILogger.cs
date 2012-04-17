using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMigrator.Logger
{
    public interface ILogger
    {
        void Debug(object message);

        void Error(object message);

        void Info(object message);

        void Fatal(object message);

        void Warn(object message);
    }
}
