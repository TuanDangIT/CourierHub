namespace CourierHub.Dpd.Configurations;

/// <summary>
/// Configuration options for Dpd API integration.
/// </summary>
public sealed class DpdOptions
{
    /// <summary>
    /// The base URL for the Dpd API.
    /// </summary>
    public required string BaseUrl { get; set; }

    /// <summary>
    /// Login for Basic schema authentication. Required for authentication.
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Password for Basic schema authentication. Required for authentication.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// The master financial ID assigned by Dpd. Required for authentication.
    /// </summary>
    public required string MasterFID { get; set; }
}
