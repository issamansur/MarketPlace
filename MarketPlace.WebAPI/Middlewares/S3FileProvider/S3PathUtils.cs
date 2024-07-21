namespace MarketPlace.WebAPI.Middlewares.S3FileProvider;

public static class S3PathUtils
{
    private static char[] GetInvalidFileNameChars() => Path.GetInvalidFileNameChars()
        .Where(c => c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar).ToArray();

    private static readonly char[] _invalidFileNameChars = GetInvalidFileNameChars();
    
    public static bool HasInvalidPathChars(string path) =>
        path.IndexOfAny(_invalidFileNameChars) >= 0;
}