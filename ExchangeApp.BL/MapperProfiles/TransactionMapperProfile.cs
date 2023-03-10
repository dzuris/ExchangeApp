using AutoMapper;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.DAL.Entities.Operations;

namespace ExchangeApp.BL.MapperProfiles;

public class TransactionMapperProfile : Profile
{
    public TransactionMapperProfile()
    {
        CreateMap<TransactionEntity, TransactionDetailModel>()
            .ForMember(
                dst => dst.Currency,
                opt => opt.MapFrom(src => src.Currency))
            .ForMember(
                dst => dst.Customer,
                opt => opt.MapFrom(src => src.Customer));

        CreateMap<TransactionDetailModel, TransactionEntity>()
            .ForMember(
                dst => dst.Currency,
                opt => opt.Ignore())
            .ForMember(
                dst => dst.Customer,
                opt => opt.Ignore());

        CreateMap<TransactionEntity, TransactionListModel>();
    }
}