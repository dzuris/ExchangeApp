using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.DAL.Repositories.Interfaces;

public interface IDonationRepository : IRepository<DonationEntity, int>
{
    new Task<int> InsertAsync(DonationEntity entity);
}