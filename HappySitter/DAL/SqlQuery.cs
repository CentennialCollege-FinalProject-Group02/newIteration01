using System;
using System.Collections;

public class SqlQuery
{
    public String sqlText;
    public ArrayList sqlParams;
    public SqlQuery(String sqlText, ArrayList sqlParams)
    {
        this.sqlText = sqlText;
        this.sqlParams = sqlParams;
    }
}
