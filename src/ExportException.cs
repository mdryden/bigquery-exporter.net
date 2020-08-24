using System;

namespace mdryden.tools.bigquery_exporter
{
    class ExportException : Exception
    {
        public ExportException(string message)
            : base(message)
        {

        }
    }
}
