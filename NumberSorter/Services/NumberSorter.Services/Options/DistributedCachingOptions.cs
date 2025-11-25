namespace NumberSorter.Services.Options;

public sealed class DistributedCachingOptions
{
    public const string DistributedCachingSettings = "DistributedCachingSettings";

    public bool Enabled { get; set; } = true;
    public TimeSpan Expiration { get; init; } = TimeSpan.FromMinutes(1);
}
