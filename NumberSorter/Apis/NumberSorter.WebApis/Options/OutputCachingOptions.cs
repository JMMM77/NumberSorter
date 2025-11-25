namespace NumberSorter.WebApis.Options;

public sealed class OutputCachingOptions
{
    public const string OutputCachingSettings = "OutputCachingSettings";

    public bool Enabled { get; set; } = true;
}
