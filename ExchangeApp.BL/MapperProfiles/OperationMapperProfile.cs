using AutoMapper;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.BL.MapperProfiles;

public class OperationMapperProfile : Profile
{
    public OperationMapperProfile()
    {
        CreateMap<OperationEntityBase, OperationEntityBase>()
            .Include<TransactionEntity, TransactionEntity>()
            .Include<DonationEntity, DonationEntity>();

        CreateMap<OperationEntityBase, OperationListModelBase>()
            .Include<TransactionEntity, TransactionListModel>()
            .Include<DonationEntity, DonationListModel>();
    }
}