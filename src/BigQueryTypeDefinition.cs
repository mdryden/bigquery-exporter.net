using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Text;

namespace mdryden.tools.bigquery_exporter
{
    public class BigQueryTypeDefinition
    {
        public BigQueryTypeDefinition(PropertyInfo property)
        {
            Type = GetBigQueryType(property.PropertyType);
            Property = property;
        }

        [JsonProperty("name")]
        public string Name => Property.Name;

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mode")]
        public string Mode => IsNullable(Property.PropertyType) ? "NULLABLE" : "REQUIRED";

        [JsonIgnore]
        public PropertyInfo Property { get; }

        public static bool IsNullable(Type propertyType) => propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static string GetBigQueryType(Type propertyType)
        {
            if (propertyType.IsGenericType && propertyType.GetGenericArguments().Length > 1)
                return string.Empty;

            var propertyTypeName = propertyType.IsGenericType 
                ? propertyType.GetGenericArguments()[0].Name
                : propertyType.Name;

            switch (propertyTypeName)
            {
                case "String":
                    return BigQueryTypes.String;

                case "Byte[]":
                    return BigQueryTypes.Bytes;

                case "Int32":
                case "Int64":
                    return BigQueryTypes.Integer;

                case "Single":
                case "Double":
                    return BigQueryTypes.Float;

                case "Decimal":
                    return BigQueryTypes.Numeric;

                case "Boolean":
                    return BigQueryTypes.Boolean;

                default:
                    return string.Empty;
            }
        }
    }
}
