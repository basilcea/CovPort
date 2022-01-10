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
            CreateMap<SpaceRequestBody, SaveEntity<SpaceRequestBody, Space>>();
            CreateMap<ResultPostRequestBody, SaveEntity<ResultPostRequestBody, Result>>();
            CreateMap<ResultPatchRequestBody, UpdateEntity<ResultPatchRequestBody, Result>>();
        }
    }
}