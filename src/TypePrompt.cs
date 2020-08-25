using System;
using System.Collections.Generic;

namespace mdryden.tools.bigquery_exporter
{
    public class TypePrompt
    {
        public static TypePromptResult Show(BigQueryTypeDefinition definition)
        {
            Console.WriteLine($"Unable to map '{definition.Name}' ({definition.Property.PropertyType.Name}), please select an option:\r\n");

            const string skip = "Ignore Property";
            var recommendations = new Dictionary<string, string>();

            var count = 1;
            foreach (var recommendation in BigQueryTypes.Recommendations(definition.Property.PropertyType))
            {
                recommendations.Add(count.ToString(), recommendation);
                count++;
            }
            recommendations.Add(count.ToString(), skip);

            while (true)
            {
                foreach (var kvp in recommendations)
                {
                    Console.WriteLine($"[{kvp.Key}] {kvp.Value}");
                }
                var input = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();

                if (recommendations.ContainsKey(input))
                {
                    return input == skip
                        ? TypePromptResult.Remove()
                        : TypePromptResult.Select(input);
                }
                else
                {
                    Console.WriteLine("Invalid selection, please select an option:\r\n");
                }
            }
        }
    }
}
