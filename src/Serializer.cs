using System;
using System.Text;

namespace mdryden.tools.bigquery_exporter
{
    public class Serializer
    {
        public void Serialize(Type type)
        {
            var properties = type.GetProperties();

            var builder = new StringBuilder();
            // filter to known types
            foreach (var property in properties)
            {
                if (!TryGetBigQueryType(property.PropertyType.Name, out var bigQueryType))
                {
                    // TODO: prompt for type
                    throw new NotImplementedException();
                }

                if (builder.Length > 0)
                    builder.AppendLine(",");

                builder.AppendLine("{");
                if (property.PropertyType.Name.Contains("Nullable"))
                    builder.AppendLine("  \"mode\": \"NULLABLE\",");
                else
                    builder.AppendLine("  \"mode\": \"REQUIRED\",");
                builder.AppendLine($"  \"name\": \"{property.Name}\",");
                builder.AppendLine($"  \"type\": \"{bigQueryType}\",");
                builder.Append("}");
            }

            Console.WriteLine(builder.ToString());
        }

        public bool TryGetBigQueryType(string propertyTypeName, out string bigQueryType)
        {
            switch (propertyTypeName)
            {
                case "String":
                    bigQueryType = BigQueryTypes.String;
                    return true;

                case "Byte[]":
                    bigQueryType = BigQueryTypes.Bytes;
                    return true;

                case "Int32":
                case "Int64":
                    bigQueryType = BigQueryTypes.Integer;
                    return true;

                case "Single":
                case "Double":
                    bigQueryType = BigQueryTypes.Float;
                    return true;

                case "Decimal":
                    bigQueryType = BigQueryTypes.Numeric;
                    return true;

                case "Boolean":
                    bigQueryType = BigQueryTypes.Boolean;
                    return true;

                default:
                    bigQueryType = null;
                    return false;
            }
        }
    }
}
