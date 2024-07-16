namespace MarketPlace.Infrastructure.Options;

public class ImageServiceOptions: ICloneable
{
    public string Directory { get; init; }
    public List<string> AllowedExtensions { get; init; }
    public int MaxSizeMb { get; init; }
    
    public object Clone()
    {
        return new ImageServiceOptions
        {
            Directory = Directory,
            AllowedExtensions = AllowedExtensions,
            MaxSizeMb = MaxSizeMb,
        };
    }
}