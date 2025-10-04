namespace Sportik.Backend.Application.DTOs.Auth;

public sealed record AuthResultDto(string AccessToken, string RefreshToken, string TokenType, long ExpiresIn);