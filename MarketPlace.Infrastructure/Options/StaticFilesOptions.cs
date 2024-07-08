namespace MarketPlace.Infrastructure.Options;

public class StaticFilesOptions
{
    public string RealPath { get; init; }
    public string RequestPath { get; init; }
    public int CacheExpireInMinutes { get; init; }
}