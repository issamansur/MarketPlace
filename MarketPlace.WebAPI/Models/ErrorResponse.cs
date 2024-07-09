namespace MarketPlace.WebAPI.Models;

public class ErrorResponse
{
    public string Title { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
}