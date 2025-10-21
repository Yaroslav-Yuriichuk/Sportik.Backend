using Microsoft.EntityFrameworkCore;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Models;
using Sportik.Backend.Infrastructure.Persistence;
using Sportik.Backend.Infrastructure.Persistence.Entities;
using Sportik.Backend.Infrastructure.Persistence.Mappers;

namespace Sportik.Backend.Infrastructure.Repositories.Implementations;

internal sealed class RefreshTokensRepository : IRefreshTokensRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ITokenService _tokenService;

    public RefreshTokensRepository(AppDbContext dbContext, ITokenService tokenService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        string hash = _tokenService.HashToken(token);

        UserRefreshToken? entity = await _dbContext.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Hash == hash, cancellationToken);

        return entity is null ? null : RefreshTokenMapper.ToDomain(entity, token);
    }

    public async Task<RefreshToken> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        string token = refreshToken.Token;
        string hash = _tokenService.HashToken(token);

        UserRefreshToken entity = RefreshTokenMapper.ToEntity(refreshToken, hash);

        await _dbContext.RefreshTokens.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return RefreshTokenMapper.ToDomain(entity, token);
    }

    public async Task<RefreshToken?> RevokeAsync(string token, CancellationToken cancellationToken = default)
    {
        string hash = _tokenService.HashToken(token);

        UserRefreshToken? entity = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.Hash == hash, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        entity.RevokedAt = DateTimeOffset.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return RefreshTokenMapper.ToDomain(entity, token);
    }

    public async Task<RefreshToken> ReplaceAsync(string oldToken, RefreshToken newRefreshToken, CancellationToken cancellationToken = default)
    {
        string oldHash = _tokenService.HashToken(oldToken);

        UserRefreshToken? oldEntity = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.Hash == oldHash, cancellationToken);

        if (oldEntity is not null)
        {
            oldEntity.RevokedAt = DateTimeOffset.UtcNow;
        }

        string newToken = newRefreshToken.Token;
        string newHash = _tokenService.HashToken(newToken);

        UserRefreshToken newEntity = RefreshTokenMapper.ToEntity(newRefreshToken, newHash);

        await _dbContext.RefreshTokens.AddAsync(newEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return RefreshTokenMapper.ToDomain(newEntity, newToken);
    }
}