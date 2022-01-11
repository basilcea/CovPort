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
            CreateMap<BookingPostRequestBody, SaveEntity<BookingPostRequestBody, Booking>>();
            CreateMap<BookingPatchRequestBody, UpdateEntity<BookingPatchRequestBody, Booking>>();
            CreateMap<SpaceRequestBody, SaveEntity<SpaceRequestBody, Space>>()
            .ForPath(d => d.Body.Date , opt => opt.MapFrom(s => DateTime.Parse(s.Date)))
            .ForMember(d => d.RequesterId , opt => opt.MapFrom(s => s.RequesterId));

            CreateMap<ResultPostRequestBody, SaveEntity<ResultPostRequestBody, Result>>()
            .ForMember(d => d.RequesterId , opt => opt.MapFrom(s => s.RequesterId));

            CreateMap<ResultPatchRequestBody, UpdateEntity<ResultPatchRequestBody, Result>>();
        }
    }
}