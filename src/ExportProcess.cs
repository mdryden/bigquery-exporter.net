using System;
using System.Reflection;

namespace mdryden.tools.bigquery_exporter
{
    public class ExportProcess
    {
        public static void Export(Options options)
        {
            try
            {
                var typeFinder = new TypeFinder(options.Verbose);
                var type = typeFinder.Find(options.DllPath, options.ClassSearch);

                Console.WriteLine($"Exporting {type.FullName}");

                var serializer = new Serializer();
                serializer.Serialize(type);
            }
            catch (ExportException ex)
            {
                Console.WriteLine($"Export failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured during export: {ex.Message}");
            }

        }
    }
}
