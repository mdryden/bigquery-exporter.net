using Newtonsoft.Json;
using System;
using System.Linq;
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
                Console.WriteLine();

                var definitions = type.GetDefinitions().ToList();

                foreach (var definition in definitions.Where(p => string.IsNullOrEmpty(p.Type)))
                {
                    var result = TypePrompt.Show(definition);

                    if (!result.RemoveProperty)
                        definition.Type = result.SelectedType;
                }

                definitions.RemoveAll(d => string.IsNullOrEmpty(d.Type));

                var json = JsonConvert.SerializeObject(definitions, Formatting.Indented);


                Console.WriteLine(json);
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
