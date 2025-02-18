﻿using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.App;

internal interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class SqLiteDbMigrator : IDbMigrator
{
    private readonly IDbContextFactory<ExchangeAppDbContext> _dbContextFactory;

    public SqLiteDbMigrator(IDbContextFactory<ExchangeAppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using ExchangeAppDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
