using System;
using System.Collections;
using System.Collections.Generic;

namespace mdryden.tools.bigquery_exporter
{
    public static class BigQueryTypes
    {
        public const string String = "STRING";
        public const string Bytes = "BYTES";
        public const string Integer = "INTEGER";
        public const string Float = "FLOAT";
        public const string Numeric = "NUMERIC";
        public const string Boolean = "BOOLEAN";
        public const string Timestamp = "TIMESTAMP";
        public const string Date = "DATE";
        public const string Time = "TIME";
        public const string DateTime = "DATETIME";
        public const string Geography = "GEOGRAPHY";
        public const string Record = "RECORD";

        public static IEnumerable<string> Recommendations(Type propertyType)
        {
            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                yield return Date;
                yield return DateTime;
                yield return Time;
                yield return Timestamp;
            }
            else
            {
                yield return String;
                yield return Bytes;
                yield return Integer;
                yield return Float;
                yield return Numeric;
                yield return Boolean;
                yield return Timestamp;
                yield return Date;
                yield return Time;
                yield return DateTime;
                yield return Geography;
                yield return Record;
            }
        }
    }
}
