namespace Sportik.Backend.Application.DTOs.Auth;

public sealed record LoginResultDto(string AccessToken, string RefreshToken, string TokenType, long ExpiresIn);