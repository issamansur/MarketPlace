using System.Xml;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.FileProviders;

namespace MarketPlace.WebAPI.Middlewares.S3FileProvider;

public class S3FileInfo: IFileInfo
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _key;

    private readonly GetObjectMetadataResponse _metadata;
    
    private bool _disposed;
    
    public S3FileInfo(IAmazonS3 s3Client, string bucketName, string key)
    {
        _s3Client = s3Client;
        _bucketName = bucketName;
        _key = key;
        
        _metadata = GetObjectMetadataAsync().Result;
    }

    public bool Exists => _metadata.HttpStatusCode == System.Net.HttpStatusCode.OK;

    public long Length => _metadata.ContentLength;

    public string? PhysicalPath => null;

    public string Name => Path.GetFileName(_key);

    public DateTimeOffset LastModified => _metadata.LastModified;

    public bool IsDirectory => false;

    public Stream CreateReadStream()
    {
        var request = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = _key
        };

        try
        {
            var file = _s3Client.GetObjectAsync(request).Result;
            
            return file.ResponseStream;
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new FileNotFoundException("The file does not exist.");
            }

            throw;
        }
    }

    private async Task<GetObjectMetadataResponse> GetObjectMetadataAsync()
    {
        var request = new GetObjectMetadataRequest
        {
            BucketName = _bucketName,
            Key = _key
        };

        try
        {
            var metadata = await _s3Client.GetObjectMetadataAsync(request);

            return metadata;
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new GetObjectMetadataResponse
                {
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            throw;
        }
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