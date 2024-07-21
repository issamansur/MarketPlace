using Amazon.S3;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace MarketPlace.WebAPI.Middlewares.S3FileProvider;

public class S3FileProvider: IFileProvider, IDisposable
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    private bool _disposed;
    
    public S3FileProvider(IAmazonS3 s3Client, string bucketName)
    {
        _s3Client = s3Client;
        _bucketName = bucketName;
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        // Not implemented as we are focusing on file access
        return NotFoundDirectoryContents.Singleton;
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        if (string.IsNullOrEmpty(subpath) || S3PathUtils.HasInvalidPathChars(subpath))
        {
            return new NotFoundFileInfo(subpath);
        }
        
        var key = subpath.TrimStart('/');
        try
        {
            var fileInfo = new S3FileInfo(_s3Client, _bucketName, key);
            return fileInfo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.InnerException?.Message);
            return new NotFoundFileInfo(subpath);
        }
    }

    public IChangeToken Watch(string filter)
    {
        // Not implemented as we are focusing on file access
        return NullChangeToken.Singleton;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _s3Client.Dispose();
            }
            _disposed = true;
        }
    }
}