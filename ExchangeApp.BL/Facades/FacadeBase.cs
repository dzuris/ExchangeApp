using AutoMapper;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.DAL.Entities;
using ExchangeApp.DAL.Repositories;
using ExchangeApp.DAL.Repositories.Interfaces;
using ExchangeApp.DAL.UnitOfWork;

namespace ExchangeApp.BL.Facades;

public abstract class 
    FacadeBase<TEntity, TListModel, TDetailModel, TId> : IFacade
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
{
    //protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    //protected readonly IUnitOfWork UnitOfWork;
    //protected readonly IRepository<TEntity, TId> Repository;
    //protected readonly IMapper Mapper;

    //protected FacadeBase(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    //{
    //    UnitOfWorkFactory = unitOfWorkFactory;
    //    UnitOfWork = UnitOfWorkFactory.Create();
    //    Repository = UnitOfWork.GetRepository<TEntity, TId>();
    //    Mapper = mapper;
    //}

    //public async Task<IEnumerable<TListModel>> GetAllAsync()
    //{
    //    var entities = await Repository.GetAllAsync();
    //    return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TListModel>>(entities);
    //}

    //public async Task<TDetailModel?> GetByIdAsync(TId id)
    //{
    //    var entity = await Repository.GetByIdAsync(id);

    //    return entity is null ? null : Mapper.Map<TEntity, TDetailModel>(entity);
    //}

    //public async Task InsertAsync(TDetailModel model)
    //{
    //    var entity = Mapper.Map<TDetailModel, TEntity>(model);
    //    await Repository.InsertAsync(entity);
    //    await UnitOfWork.CommitAsync();
    //}

    //public async Task UpdateAsync(TDetailModel model)
    //{
    //    var entity = Mapper.Map<TDetailModel, TEntity>(model);
    //    await Repository.UpdateAsync(entity);
    //    await UnitOfWork.CommitAsync();
    //}

    //public async Task DeleteAsync(TId id)
    //{
    //    await Repository.DeleteAsync(id);
    //    await UnitOfWork.CommitAsync();
    //}
}