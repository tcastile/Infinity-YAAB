using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity_YAAB.Models.Repositories.Concrete.SQL_Server.Util
{
    public class SqlNullValueException : Exception
    {
        public SqlNullValueException(string message)
            : base(message)
        { }
    }
}