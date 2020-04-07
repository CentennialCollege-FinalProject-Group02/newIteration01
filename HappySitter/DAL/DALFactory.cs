using System;
using System.Data;
using System.Data.SqlClient;

namespace HappySitter.DAL
{
    public class SqlDALFactory
    {
        String connString;

        public SqlDALFactory(String connString)
        {
            this.connString = connString;
        }

        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connString);
        }
        public IDbDataParameter CreateDataParameter()
        {
            return new SqlParameter();
        }
        public IDbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }
        public IDbDataAdapter CreateDataAdapterForTable(String table, String primaryKeyName, SqlRowUpdatedEventHandler handler)
        {
            String selectSql = "SELECT * FROM " + table + " WHERE " + primaryKeyName + " = -1";
            SqlDataAdapter adapter = new SqlDataAdapter(selectSql, connString);

            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
            cmdBuilder.QuotePrefix = "[";
            cmdBuilder.QuoteSuffix = "]";
            adapter.InsertCommand = cmdBuilder.GetInsertCommand();

            //adapter.InsertCommand.CommandText +=
            //    "; " + 
            //    "SELECT " + primaryKeyName + " FROM " + table + " " + 
            //    "WHERE " + primaryKeyName + " = SCOPE_IDENTITY();";
            //adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
            //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
            adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();

            if (handler != null)
            {
                adapter.RowUpdated += handler;
            }
            return adapter;
        }
    }
}