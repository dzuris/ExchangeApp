using AutoMapper;
using ExchangeApp.DAL.Entities.Operations;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class DonationRepository : RepositoryBase<DonationEntity, int>, IDonationRepository
{
    private readonly IMapper _mapper;

    public DonationRepository(DbContext appDbContext, IMapper mapper) : base(appDbContext)
    {
        _mapper = mapper;
    }

    public override async Task<int> InsertAsync(DonationEntity entity)
    {
        await AppDbContext
            .Set<DonationEntity>()
            .AddAsync(entity);
        await AppDbContext.SaveChangesAsync();

        return entity.Id;
    }
}