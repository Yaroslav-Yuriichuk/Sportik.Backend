namespace Sportik.Backend.Api.DTOs.Auth;

internal sealed record AuthTokensDto(string AccessToken, string RefreshToken, string TokenType, long ExpiresIn);