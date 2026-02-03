namespace Project1.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }  // ? -> RequestedId can be null

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
