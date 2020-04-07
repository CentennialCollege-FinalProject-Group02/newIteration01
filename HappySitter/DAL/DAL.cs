using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace HappySitter.DAL
{
    public class DAL
    {
        SqlDALFactory factory;

        public DAL(String connString)
        {
            factory = new SqlDALFactory(connString);
        }

        public DataSet ExecuteSelectCommand(String sql, ArrayList sqlParams)
        {
            IDbCommand cmd = factory.CreateCommand();
            IDbConnection connection = factory.CreateConnection();
            IDbDataAdapter adapter = factory.CreateDataAdapter();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (sqlParams != null)
            {
                foreach (SqlParam p in sqlParams)
                {
                    cmd.Parameters.Add(GetDataParameter(p));
                }
            }
            cmd.CommandTimeout = 180; //seconds

            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }

        public DataSet ExecuteSelectStoredProc(String sql, ArrayList sqlParams)
        {
            IDbCommand cmd = factory.CreateCommand();
            IDbConnection connection = factory.CreateConnection();
            IDbDataAdapter adapter = factory.CreateDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (sqlParams != null)
            {
                foreach (SqlParam p in sqlParams)
                {
                    cmd.Parameters.Add(GetDataParameter(p));
                }
            }
            cmd.CommandTimeout = 180; //seconds

            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }

        public Int64 ExecuteScalarCommand(String sql, ArrayList sqlParams)
        {
            Int64 result;
            IDbCommand cmd = factory.CreateCommand();
            IDbConnection connection = factory.CreateConnection();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (sqlParams != null)
            {
                foreach (SqlParam p in sqlParams)
                {
                    cmd.Parameters.Add(GetDataParameter(p));
                }
            }
            cmd.CommandTimeout = 180; //seconds

            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                result = Convert.ToInt64(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e; // Let the exception bubbles up, but do close the conn
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public void ExecuteNonQueryCommand(String sql, ArrayList sqlParams)
        {
            IDbCommand cmd = factory.CreateCommand();
            IDbConnection connection = factory.CreateConnection();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (sqlParams != null)
            {
                foreach (SqlParam p in sqlParams)
                {
                    cmd.Parameters.Add(GetDataParameter(p));
                }
            }
            cmd.CommandTimeout = 180; //seconds

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e; // Let the exception bubbles up, but do close the conn
            }
            finally
            {
                connection.Close();
            }
        }

        public Int32 ExecuteInsertRecordCommand(String sql, ArrayList sqlParams)
        {
            IDbCommand cmd = factory.CreateCommand();
            IDbConnection connection = factory.CreateConnection();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            if (sqlParams != null)
            {
                foreach (SqlParam p in sqlParams)
                {
                    cmd.Parameters.Add(GetDataParameter(p));
                }
            }

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmd2 = factory.CreateCommand();
                cmd2.Connection = connection;
                cmd2.CommandText = "SELECT @@IDENTITY";
                cmd2.Parameters.Clear();
                return Convert.ToInt32(cmd2.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw e; // Let the exception bubbles up, but do close the conn
            }
            finally
            {
                connection.Close();
            }
        }

        public Boolean ExecuteCommandsInTransaction(params SqlQuery[] queries)
        {
            IDbConnection connection = factory.CreateConnection();
            connection.Open();
            IDbTransaction trans = connection.BeginTransaction();

            try
            {
            
                foreach (SqlQuery query in queries)
                {
                    IDbCommand cmd = factory.CreateCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = query.sqlText;
                    cmd.Transaction = trans;
                    if (query.sqlParams != null)
                    {
                        foreach (SqlParam p in query.sqlParams)
                        {
                            cmd.Parameters.Add(GetDataParameter(p));
                        }
                    }    
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
                return true;
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw e; // Let the exception bubbles up, but do close the conn
            }
            finally
            {
                connection.Close();
            }
        }

        public IDbDataAdapter GetDataAdapterForTable(String table, String primaryKeyName, SqlRowUpdatedEventHandler handler)
        {
            return factory.CreateDataAdapterForTable(table, primaryKeyName, handler);
        }

        public IDbDataParameter GetDataParameter(String name, DbType type, Int16 size, String value)
        {
            IDbDataParameter param = factory.CreateDataParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Size = size;
            param.Value = value;

            return param;
        }
        public IDbDataParameter GetDataParameter(SqlParam p)
        {
            IDbDataParameter param = factory.CreateDataParameter();
            param.ParameterName = p.name;
            param.Value = p.value;

            return param;
        }
   
    
    
    }
}
