using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace mdryden.tools.bigquery_exporter
{
    public class TypeFinder
    {
        private readonly bool verbose;

        public TypeFinder(bool verbose)
        {
            this.verbose = verbose;
        }

        public Type Find(string dllPath, string classSearch)
        {
            var assembly = LoadAssembly(dllPath);
            var matches = FindClassMatches(assembly, classSearch);

            if (!matches.Any())
                throw new ExportException($"No classes matching name '{classSearch}' were found in dll.");

            if (matches.Count() > 9)
                throw new ExportException($"Too many matches for '{classSearch}' in library, please try a more specific search (namespaces may be included).");

            if (matches.Count() == 1)
                return matches.First();

            return SelectClass(matches);
        }

        public Assembly LoadAssembly(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                throw new ExportException("File not found.");
            }

            var assembly = Assembly.LoadFrom(dllPath);

            if (assembly == null)
            {
                throw new ExportException($"Unable to load .Net assembly from {dllPath}.");
            }

            return assembly;
        }

        public IEnumerable<Type> FindClassMatches(Assembly assembly, string classSearch)
        {
            var isFullyQualified = classSearch.Contains(".");

            var types = assembly.ExportedTypes;

            var exactMatches = isFullyQualified
                ? types.Where(t => t.FullName == classSearch)
                : types.Where(t => t.Name == classSearch);

            if (exactMatches.Count() == 1)
                return exactMatches;

            if (verbose && exactMatches.Any())
            {
                Console.WriteLine("Exact matches:\r\n");
                foreach (var match in exactMatches)
                    Console.WriteLine(match.FullName);

                Console.WriteLine();
            }

            var partialMatches = isFullyQualified
                ? types.Where(t => t.FullName.Contains(classSearch))
                : types.Where(t => t.Name.Contains(classSearch));

            if (verbose && partialMatches.Any())
            {
                Console.WriteLine("Partial matches:\r\n");
                foreach (var match in partialMatches)
                    Console.WriteLine(match.FullName);

                Console.WriteLine();
            }

            return partialMatches;
        }

        public Type SelectClass(IEnumerable<Type> matches)
        {
            var selections = new Dictionary<string, Type>();

            var x = 1;
            foreach (var match in matches)
            {
                selections.Add($"{x++}", match);
            }

            while (true)
            {
                Console.WriteLine("Select class to export:\r\n");
                foreach (var selection in selections)
                {
                    Console.WriteLine($"[{selection.Key}] {selection.Value.FullName}");
                }

                var input = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();

                if (selections.ContainsKey(input))
                    return selections[input];

                Console.WriteLine("Invalid selection");
                Console.WriteLine();
            }
        }

    }
}
