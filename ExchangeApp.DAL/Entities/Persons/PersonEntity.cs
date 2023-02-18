﻿using AutoMapper;

namespace ExchangeApp.DAL.Entities.Persons;

public record PersonEntity : IEntity
{
    public required Guid Id { get; set; }
    public required DateTime Created { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class PersonEntityMapperProfile : Profile
{
    public PersonEntityMapperProfile()
    {
        CreateMap<PersonEntity, PersonEntity>();
    }
}
