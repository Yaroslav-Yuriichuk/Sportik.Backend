using Sportik.Backend.Api.DTOs.Auth;
using Sportik.Backend.Domain.Models;

namespace Sportik.Backend.Api.Mappers;

internal static class AuthTokensMapper
{
    public static AuthTokensDto ToDto(AuthTokens tokens)
    {
        return new AuthTokensDto(
            AccessToken: tokens.AccessToken,
            RefreshToken: tokens.RefreshToken,
            TokenType: tokens.TokenType,
            ExpiresIn: tokens.ExpiresIn);
    }

    public static AuthTokens ToDomain(AuthTokensDto tokens)
    {
        return new AuthTokens
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            TokenType = tokens.TokenType,
            ExpiresIn = tokens.ExpiresIn
        };
    }
}