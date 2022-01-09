using System;
using Application.Commands;
using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Api.Mappings
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<BookingPostRequestBody, SaveEntity<Booking>>();
            CreateMap<BookingPatchRequestBody, UpdateEntity<Booking>>();
            CreateMap<LocationRequestBody, SaveEntity<Location>>();
            CreateMap<SpaceRequestBody, SaveEntity<Space>>();
            CreateMap<ResultPatchRequestBody, SaveEntity<Result>>()
            .ForMember(
                d => d.Entity.Positive, opt => opt.MapFrom(s => Boolean.Parse(s.Positive))
            );
        }
    }
}