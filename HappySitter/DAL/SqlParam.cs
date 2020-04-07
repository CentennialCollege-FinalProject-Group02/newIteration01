using System;

namespace HappySitter.DAL
{
    public class SqlParam
    {
        public String name;
        public Object value;
        public SqlParam(String name, Object value) : this(name, value, false)
        {

        }

        public SqlParam(String name, Object value, Boolean checkInput)
        {
            Object parameter = new Object();
            parameter = ((value != null && !String.IsNullOrWhiteSpace(value.ToString())) ? value : DBNull.Value);

            if (checkInput && !ValidationUtil.IsLegalString(parameter.ToString()))
            {
                throw new Exception("Special Characters used in SQL parameter");
            }
            this.name = name;
            this.value = parameter;
        }
    }
}
