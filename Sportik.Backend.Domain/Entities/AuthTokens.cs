namespace Sportik.Backend.Domain.Entities;

public sealed class AuthTokens
{
    public string AccessToken { get; init; } = string.Empty;

    public string RefreshToken { get; init; } = string.Empty;

    public string TokenType { get; init; } = "Bearer";

    public long ExpiresIn { get; init; }
}