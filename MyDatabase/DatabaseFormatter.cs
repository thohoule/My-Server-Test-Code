using System;
using System.Text;

namespace MyDatabase
{
    public static class DatabaseFormatter
    {
        public const string CreateTableFormat = "CREATE TABLE {0} ({1})";
        public const string InsertIntoFormat = "INSERT INTO {0} ({1}) VALUES ({2})";
        public const string UpdateInfoFormat = "UPDATE {0} SET {1} {2}";

        public const string WhereStatement = "WHERE {0}";
        public const string AssignStatement = "{0} = @{1}";

        public const string SelectFromFormat = "SELECT {0} FROM {1}";

        public static string BuildFieldNames(params DatabaseField[] fields)
        {
            if (fields.Length == 0)
                return "";

            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < fields.Length; index++)
            {
                builder.Append(fields[index].Name);
                builder.Append(' ');
                builder.Append(fields[index].Type.ToString());

                if (index + 1 < fields.Length)
                    builder.Append(',');
            }

            return builder.ToString();
        }

        public static void BuildInsertFields(out string fieldsName, out string[] fieldArgs, params DatabaseField[] fields)
        {
            fieldsName = "";
            fieldArgs = new string[fields.Length];

            if (fields.Length == 0)
                return;

            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < fields.Length; index++)
            {
                builder.Append(fields[index].Name);
                builder.Append(' ');
                builder.Append(fields[index].Type.ToString());

                fieldArgs[index] = "@" + index.ToString();

                if (index + 1 < fields.Length)
                    builder.Append(',');
            }

            fieldsName = builder.ToString();
        }

        public static string BuildConditional(params DatabaseConditional[] conditionals)
        {
            if (conditionals.Length == 0)
                return "";

            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < conditionals.Length; index++)
            {
                builder.AppendFormat(AssignStatement, conditionals[index].FilterName, index.ToString());

                if (index + 1 < conditionals.Length)
                    builder.Append(" AND ");
            }

            return string.Format(WhereStatement, builder.ToString());
        }
    }
}
