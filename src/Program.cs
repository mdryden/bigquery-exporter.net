using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdryden.tools.bigquery_exporter
{
    public class Options
    {
        [Option('d', "dll", Required = true, HelpText = ".Net DLL which contains target class.")]
        public string DllPath { get; set; }

        [Option('c', "class", Required = true, HelpText = "Class name to export from source library. May be a partial name.")]
        public string ClassSearch { get; set; }

        [Option('v', "verbose", Default = false)]
        public bool Verbose { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(ExportProcess.Export);

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
