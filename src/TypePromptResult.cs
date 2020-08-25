namespace mdryden.tools.bigquery_exporter
{
    public class TypePromptResult
    {
        public string SelectedType { get; set; }

        public bool RemoveProperty { get; set; }

        public static TypePromptResult Remove() => new TypePromptResult { RemoveProperty = true };

        public static TypePromptResult Select(string selection) => new TypePromptResult { SelectedType = selection };
    }
}
