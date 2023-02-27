using System.Linq.Expressions;
using AutoMapper;

namespace ExchangeApp.BL.Extensions;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> MapMember<TSource, TDestination, TSourceMember>(
        this IMappingExpression<TSource, TDestination> map,
        Expression<Func<TDestination, object>> dstSelector,
        Expression<Func<TSource, TSourceMember>> srcSelector)
    {
        map.ForMember(dstSelector, config => config.MapFrom(srcSelector));
        return map;
    }

    public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map,
        Expression<Func<TDestination, object?>> selector)
    {
        if (selector is not null)
        {
            map.ForMember(selector, opt => opt.Ignore());
        }

        return map;
    }
}