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

    public override async Task<DonationEntity?> GetByIdAsync(int id)
    {
        var entity = await AppDbContext
            .Set<DonationEntity>()
            .Include(e => e.Currency)
            .SingleOrDefaultAsync(e => e.Id == id);
        return entity;
    }

    public override async Task<int> InsertAsync(DonationEntity entity)
    {
        await AppDbContext
            .Set<DonationEntity>()
            .AddAsync(entity);
        await AppDbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<DonationEntity>> GetDonations(DateTime from, DateTime until)
    {
        var list = await AppDbContext
            .Set<DonationEntity>()
            .Where(o => o.Created >= from && o.Created <= until)
            .ToListAsync();
        return list;
    }
}