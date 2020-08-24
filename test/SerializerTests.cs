using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdryden.tools.bigquery_exporter.tests
{
    [TestFixture]
    public class SerializerTests
    {

        [TestCase(typeof(string), ExpectedResult = "STRING")]
        [TestCase(typeof(byte[]), ExpectedResult = "BYTES")]
        [TestCase(typeof(int), ExpectedResult = "INTEGER")]
        [TestCase(typeof(long), ExpectedResult = "INTEGER")]
        [TestCase(typeof(float), ExpectedResult = "FLOAT")]
        [TestCase(typeof(double), ExpectedResult = "FLOAT")]
        [TestCase(typeof(decimal), ExpectedResult = "NUMERIC")]
        [TestCase(typeof(bool), ExpectedResult = "BOOLEAN")]
        public string TryGetBigQueryTypePrimitiveTests(Type type)
        {
            var target = new Serializer();

            target.TryGetBigQueryType(type.Name, out var actual);
            return actual;
        }

    }
}
