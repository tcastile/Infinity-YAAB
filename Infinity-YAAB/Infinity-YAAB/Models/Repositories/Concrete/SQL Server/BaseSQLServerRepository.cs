using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Infinity_YAAB.Models.Repositories.Concrete.SQL_Server
{
    public abstract class BaseSQLServerRepository
    {
        private string o_strConnectionString = String.Empty;

        /// <summary>
        /// Constructor, takes a connection string ID that should be set up in the configs.
        /// </summary>
        /// <param name="ConnectionStringID"></param>
        public BaseSQLServerRepository(string ConnectionStringID)
        {
            o_strConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringID].ConnectionString;
        }

        /// <summary>
        /// Executes given 'Procedure' as a non-query stored procedure with the specified parameters.  
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public int ExecuteNonQueryProcedure(string Procedure, SqlParameter[] Params)
        {
            using (SqlConnection l_objConn = new SqlConnection(o_strConnectionString))
            {
                using (SqlCommand l_objQuery = l_objConn.CreateCommand())
                {
                    l_objQuery.CommandType = System.Data.CommandType.StoredProcedure;
                    l_objQuery.CommandText = Procedure;
                    if (Params != null)
                    {
                        l_objQuery.Parameters.AddRange(Params);
                    }

                    foreach (SqlParameter l_objParam in l_objQuery.Parameters)
                    {
                        l_objParam.Value = l_objParam.Value == null ? DBNull.Value : l_objParam.Value;
                    }

                    try
                    {
                        l_objConn.Open();
                        return l_objQuery.ExecuteNonQuery();
                    }
                    catch(SqlException)
                    {
                        //TODO:
                        //log this here and rethrow?  
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// Executes the provided procedure with the supplied parameters.  Provide a handler delegate to determine what happens
        /// to the data return.  
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Params"></param>
        /// <param name="Handler"></param>
        /// <returns></returns>
        public Object ExecuteQueryProcedure(string Procedure, SqlParameter[] Params, Func<SqlDataReader, Object> Handler)
        {
            using (SqlConnection l_objConn = new SqlConnection(o_strConnectionString))
            {
                using (SqlCommand l_objQuery = l_objConn.CreateCommand())
                {
                    l_objQuery.CommandType = System.Data.CommandType.StoredProcedure;
                    l_objQuery.CommandText = Procedure;
                    if (Params != null)
                    {
                        l_objQuery.Parameters.AddRange(Params);
                    }

                    foreach(SqlParameter l_objParam in l_objQuery.Parameters)
                    {
                        l_objParam.Value = l_objParam.Value == null ? DBNull.Value : l_objParam.Value;
                    }

                    try
                    {
                        l_objConn.Open();
                        using (SqlDataReader l_objReader = l_objQuery.ExecuteReader())
                        {
                            return Handler(l_objReader);
                        }
                    }
                    catch (SqlException)
                    {
                        //TODO:
                        //log this here and rethrow?  
                        throw;
                    }
                }
            }
        }


        /***********************
         * DataReader helper functions
         * *********************/


        /*******
         * INT Handlers
         */
        /// <summary>
        /// Gets an int out of the data reader by ordinal column name.  
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int getInt_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetInt32(l_intCol);
            }
        }

        /// <summary>
        /// Gets an int out of the data reader by ordinal column name.  
        /// If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public int getInt_ByOrdinal(SqlDataReader reader, string column, int valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetInt32(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable int out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="p_objReader"></param>
        /// <param name="p_strColumn"></param>
        /// <returns></returns>
        public int? getNullableInt_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (int?)reader.GetInt32(l_intCol);
        }


        /*******
         * SHORT Handlers
         */
        /// <summary>
        /// Gets a short out of the data reader by ordinal column name.  
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public short getShort_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetInt16(l_intCol);
            }
        }

        /// <summary>
        /// Gets a short out of the data reader by ordinal column name.  
        /// If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public short getShort_ByOrdinal(SqlDataReader reader, string column, short valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetInt16(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable short out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public short? getNullableShort_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (short?)reader.GetInt16(l_intCol);
        }


        /*******
         * LONG Handlers
         */
        /// <summary>
        /// Gets a long out of the data reader by ordinal column name.  
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public long getLong_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetInt64(l_intCol);
            }
        }

        /// <summary>
        /// Gets a long out of the data reader by ordinal column name.  
        /// If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public long getLong_ByOrdinal(SqlDataReader reader, string column, long valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetInt64(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable long out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public long? getNullableLong_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (long?)reader.GetInt64(l_intCol);
        }


        /*******
         * DOUBLE Handlers
         */
        /// <summary>
        /// Gets a double out of the data reader by ordinal column name.  
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public double getDouble_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetDouble(l_intCol);
            }
        }

        /// <summary>
        /// Gets a double out of the data reader by ordinal column name.  
        /// If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public double getDouble_ByOrdinal(SqlDataReader reader, string column, double valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetDouble(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable double out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public double? getNullableDouble_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (double?)reader.GetDouble(l_intCol);
        }


        /*******
         * FLOAT Handlers
         */
        /// <summary>
        /// Gets a float out of the data reader by ordinal column name.  
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float getFloat_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetFloat(l_intCol);
            }
        }

        /// <summary>
        /// Gets a float out of the data reader by ordinal column name.  
        /// If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public float getFloat_ByOrdinal(SqlDataReader reader, string column, float valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetFloat(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable float out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float? getNullableFloat_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (float?)reader.GetFloat(l_intCol);
        }


        /*******
         * DECIMAL Handlers
         */
        /// <summary>
        /// Gets a decimal out of the data reader by ordinal column name.  
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public decimal getDecimal_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetDecimal(l_intCol);
            }
        }

        /// <summary>
        /// Gets a decimal out of the data reader by ordinal column name.  
        /// If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public decimal getDecimal_ByOrdinal(SqlDataReader reader, string column, decimal valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetDecimal(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable decimal out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public decimal? getNullableDecimal_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (decimal?)reader.GetDecimal(l_intCol);
        }


        /*******
         * STRING Handlers
         */
        /// <summary>
        /// Gets a string out of the data reader by ordinal column name.
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        /*
        public string getString_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetString(l_intCol);
            }
        }
        */
        /// <summary>
        /// Gets a string out of the data reader by ordinal column name.  If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="p_objReader"></param>
        /// <param name="p_strColumn"></param>
        /// <returns></returns>
        public string getString_ByOrdinal(SqlDataReader reader, string column, string valueIfNull = "")
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetString(l_intCol);
        }


        /*******
         * DATETIME Handlers
         */
        /// <summary>
        /// Gets a DateTime out of the data reader by ordinal column name.
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public DateTime getDateTime_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetDateTime(l_intCol);
            }
        }

        /// <summary>
        /// Gets a DateTime out of the data reader by ordinal column name.  If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="p_objReader"></param>
        /// <param name="p_strColumn"></param>
        /// <returns></returns>
        public DateTime getDateTime_ByOrdinal(SqlDataReader reader, string column, DateTime valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetDateTime(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable DateTime out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// NOTE: this is only really nessecary to handle the return from the old DB tables that explicitly call out nullables.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public DateTime? getNullableDateTime_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (DateTime?)reader.GetDateTime(l_intCol);
        }


        /*******
         * BOOL Handlers
         */
        /// <summary>
        /// Gets a boolean out of the data reader by ordinal column name.
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool getBool_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetBoolean(l_intCol);
            }
        }

        /// <summary>
        /// Gets a boolean out of the data reader by ordinal column name.  If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public bool getBool_ByOrdinal(SqlDataReader reader, string column, bool valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetBoolean(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable Bool out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool? getNullableBool_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (bool?)reader.GetBoolean(l_intCol);
        }


        /*******
         * BYTE Handlers
         */
        /// <summary>
        /// Gets a byte out of the data reader by ordinal column name.
        /// The target of this is assumed to not accept null values, 
        /// so an exception will be thrown in that case.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public byte getByte_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            if (reader.IsDBNull(l_intCol))
            {
                throw new Util.SqlNullValueException("Value in column '" + column + "' cannot be null.");
            }
            else
            {
                return reader.GetByte(l_intCol);
            }
        }

        /// <summary>
        /// Gets a byte out of the data reader by ordinal column name.  If the value is null, the supplied parameter will be used.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public byte getByte_ByOrdinal(SqlDataReader reader, string column, byte valueIfNull)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? valueIfNull : reader.GetByte(l_intCol);
        }

        /// <summary>
        /// Gets a Nullable Byte out of the data reader by ordinal column name.  Null coalescing is not done at this level.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public byte? getNullableByte_ByOrdinal(SqlDataReader reader, string column)
        {
            int l_intCol = reader.GetOrdinal(column);
            return reader.IsDBNull(l_intCol) ? null : (byte?)reader.GetByte(l_intCol);
        }
    }
}