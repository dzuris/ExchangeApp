using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangeApp.DAL.Repositories;

public class DonationRepository : RepositoryBase<DonationEntity, int>, IDonationRepository
{
    public DonationRepository(DbContext appDbContext) : base(appDbContext)
    {
    }
}