using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Infinity_YAAB.Models.Repositories.Abstract;
using Infinity_YAAB.Models.Repositories.Concrete.SQL_Server.Util;

namespace Infinity_YAAB.Models.Repositories.Concrete.SQL_Server
{
    public class SQLServerUnitDataRepository : BaseSQLServerRepository, IUnitDataRepository
    {
        public SQLServerUnitDataRepository(string ConnectionStringID) : base(ConnectionStringID) { }
        

        public string testFunction()
        {
            SqlParameter[] Params = 
            {
                new SqlParameter("@UnitID", System.Data.SqlDbType.Int) { Value = 1 }
            };

            return (string)ExecuteQueryProcedure("spGetUnitData", Params, (SqlDataReader l_objReader) =>
            {
                while(l_objReader.Read())
                {
                    //Should probably throw an exception...    
                }
                return "testCompreeto";

            });
        }
    }
}