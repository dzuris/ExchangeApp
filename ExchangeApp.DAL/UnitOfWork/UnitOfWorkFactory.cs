using AutoMapper;
using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ExchangeAppDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public UnitOfWorkFactory(IDbContextFactory<ExchangeAppDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext(), _mapper);
}