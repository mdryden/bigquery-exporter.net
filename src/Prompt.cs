using System;

namespace mdryden.tools.bigquery_exporter
{
    public static class Prompt
    {
        public static string ReadLine(string prompt = "> ")
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
