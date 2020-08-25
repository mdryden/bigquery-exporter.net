using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdryden.tools.bigquery_exporter.tests
{
    [TestFixture]
    public class BigQueryTypeDefinitionTests
    {

        [TestCase(typeof(string), ExpectedResult = "STRING")]
        [TestCase(typeof(byte[]), ExpectedResult = "BYTES")]
        [TestCase(typeof(int), ExpectedResult = "INTEGER")]
        [TestCase(typeof(long), ExpectedResult = "INTEGER")]
        [TestCase(typeof(float), ExpectedResult = "FLOAT")]
        [TestCase(typeof(double), ExpectedResult = "FLOAT")]
        [TestCase(typeof(decimal), ExpectedResult = "NUMERIC")]
        [TestCase(typeof(bool), ExpectedResult = "BOOLEAN")]
        [TestCase(typeof(DateTime), ExpectedResult = "")]
        [TestCase(typeof(object), ExpectedResult = "")]
        public string GetBigQueryTypePrimitiveTests(Type type)
        {
            var actual = BigQueryTypeDefinition.GetBigQueryType(type);
            return actual;
        }

        [TestCase(typeof(int?), ExpectedResult = "INTEGER")]
        [TestCase(typeof(long?), ExpectedResult = "INTEGER")]
        [TestCase(typeof(float?), ExpectedResult = "FLOAT")]
        [TestCase(typeof(double?), ExpectedResult = "FLOAT")]
        [TestCase(typeof(decimal?), ExpectedResult = "NUMERIC")]
        [TestCase(typeof(bool?), ExpectedResult = "BOOLEAN")]
        [TestCase(typeof(DateTime?), ExpectedResult = "")]
        public string GetBigQueryTypeNullableTests(Type type)
        {
            var actual = BigQueryTypeDefinition.GetBigQueryType(type);
            return actual;
        }


        [TestCase(typeof(int?), ExpectedResult = true)]
        [TestCase(typeof(long), ExpectedResult = false)]
        [TestCase(typeof(DateTime?), ExpectedResult = true)]
        public bool GetIsNullableTests(Type type)
        {
            var actual = BigQueryTypeDefinition.IsNullable(type);
            return actual;
        }
    }
}
