using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface IFacade<TListModel, TDetailModel, TId>
    where TListModel : IModel
    where TDetailModel : class, IModel
{
    Task<IEnumerable<TListModel>> GetAllAsync();
    Task<TDetailModel?> GetByIdAsync(TId id);
    Task InsertAsync(TDetailModel model);
    Task UpdateAsync(TDetailModel model);
    Task DeleteAsync(TId id);
}