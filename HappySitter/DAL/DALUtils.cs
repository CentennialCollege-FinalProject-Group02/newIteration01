using System;
using System.Collections;
using HappySitter.Utils;

namespace HappySitter.DAL
{
    public class DALUtils
    {
        public static ArrayList BuildParamArray(String[] cols, System.Web.HttpRequestBase data)
        {
            ArrayList list = new ArrayList();
            foreach (string k in cols)
            {
                string value = data[k];
                list.Add(new SqlParam("@" + k, data[k]));
            }
            return list;
        }

        public static String BuildInsertColumnList(String[] cols)
        {
            string str = "";
            foreach (string c in cols)
            {
                str = StringUtils.CombineStrings(str, c, ", ");
            }
            return str;
        }
        public static String BuildInsertValueList(String[] cols)
        {
            string str = "";
            foreach (string c in cols)
            {
                str = StringUtils.CombineStrings(str, "@" + c, ", ");
            }
            return str;
        }
        public static String BuildUpdateClause(String[] cols)
        {
            string str = "";
            foreach (string c in cols)
            {
                str = StringUtils.CombineStrings(str, c + " = @" + c, ", ");
            }
            return str;
        }
    }
}
