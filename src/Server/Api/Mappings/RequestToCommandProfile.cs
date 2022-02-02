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
            CreateMap<BookingPostRequestBody, SaveEntity<Booking>>()
                .ForPath(d => d.Body.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(d => d.Body.SpaceId, opt => opt.MapFrom(s => s.SpaceId))
                .ForPath(d => d.Body.TestType, opt => opt.MapFrom(s => s.TestType.Trim().ToUpper()));

            CreateMap<BookingPatchRequestBody, UpdateEntity<Booking>>()
                .ForPath(d => d.Body.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(d => d.Body.Id, opt => opt.MapFrom(s => s.Id))
                .ForPath(d => d.Body.Status, opt => opt.MapFrom(s => s.Status.Trim().ToUpper()));

            CreateMap<SpaceRequestBody, SaveEntity<Space>>()
                .ForPath(d => d.Body.LocationName, opt => opt.MapFrom(s => s.LocationName))
                .ForPath(d => d.Body.SpacesCreated, opt => opt.MapFrom(s => s.SpacesCreated))
                .ForPath(d => d.Body.Date, opt => opt.MapFrom(s => DateTime.Parse(s.Date)))
                .ForPath(d => d.RequesterId, opt => opt.MapFrom(s => s.RequesterId));

            CreateMap<ResultPostRequestBody, SaveEntity<Result>>()
                .ForMember(d => d.RequesterId, opt => opt.MapFrom(s => s.RequesterId))
                .ForPath(d => d.Body.BookingId, opt => opt.MapFrom(s => s.BookingId))
                .ForPath(d => d.Body.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(d => d.Body.TestType, opt => opt.MapFrom(s => s.TestType.ToUpper()))
                .ForPath(d => d.Body.Status, opt => opt.MapFrom(s => s.Status.Trim().ToUpper()))
                .ForPath(d => d.Body.TestLocation, opt => opt.MapFrom(s => s.TestLocation.Trim().ToUpper()));

            CreateMap<ResultPatchRequestBody, UpdateEntity<Result>>()
                .ForMember(d => d.RequesterId, opt => opt.MapFrom(s => s.RequesterId))
                .ForPath(d => d.Body.Id, opt => opt.MapFrom(s => s.Id))
                .ForPath(d => d.Body.Status, opt => opt.MapFrom(s => s.Status.Trim().ToUpper()))
                .ForPath(d => d.Body.Positive, opt => opt.MapFrom(s => Boolean.Parse(s.Positive)));
        }
    }
}