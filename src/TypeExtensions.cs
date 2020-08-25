using System;
using System.Collections.Generic;
using System.Reflection;

namespace mdryden.tools.bigquery_exporter
{
    public static class TypeExtensions
    {
        public static IEnumerable<BigQueryTypeDefinition> GetDefinitions(this Type type)
        {
            foreach (var property in type.GetProperties())
            {
                yield return new BigQueryTypeDefinition(property);
            }
        }
    }
}
