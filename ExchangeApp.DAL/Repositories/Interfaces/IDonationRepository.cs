using ExchangeApp.DAL.Entities;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface IDonationRepository : IRepository<DonationEntity, int>
{
    new Task<int> InsertAsync(DonationEntity donation);
}