using System;
using System.Data.Common;
using Application.Commands;
using Application.DTO;
using AutoMapper;
using Domain.Aggregates;
using Domain.Entities;

namespace Infrastructure.Mappings
{
    public class ReaderToClassProfile : Profile
    {
        public ReaderToClassProfile()
        {
            CreateMap<DbDataReader, User>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s[0]))
                   .ForMember(d => d.Email, opt => opt.MapFrom(s => s[1].ToString()))
                   .ForMember(d => d.Name, opt => opt.MapFrom(s => s[2].ToString()))
                   .ForMember(d => d.UserRole, opt => opt.MapFrom(s => s[3].ToString()))
                   .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => (DateTime)s[4]))
                   .ForMember(d => d.DateUpdated, opt => opt.MapFrom(s => (DateTime)s[5]));
            CreateMap<DbDataReader, Booking>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s[6]))
                   .ForMember(d => d.UserId, opt => opt.MapFrom(s => (int)s[0]))
                   .ForMember(d => d.SpaceId, opt => opt.MapFrom(s => (int)s[7]))
                   .ForMember(d => d.Status, opt => opt.MapFrom(s => s[8].ToString()))
                   .ForMember(d => d.TestType, opt => opt.MapFrom(s => s[9].ToString()))
                   .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s[10].ToString()))
                   .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => (DateTime)s[11]))
                   .ForMember(d => d.DateUpdated, opt => opt.MapFrom(s => (DateTime)s[12]));
            CreateMap<DbDataReader, Result>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s[13]))
                   .ForMember(d => d.BookingId, opt => opt.MapFrom(s => (int)s[6]))
                   .ForMember(d => d.UserId, opt => opt.MapFrom(s => (int)s[0]))
                   .ForMember(d => d.Status, opt => opt.MapFrom(s => s[14].ToString()))
                   .ForMember(d => d.TestType, opt => opt.MapFrom(s => s[9].ToString()))
                   .ForMember(d => d.TestLocation, opt => opt.MapFrom(s => s[10].ToString()))
                   .ForMember(d => d.Positive, opt => opt.MapFrom(s => Convert.ToBoolean((int)s[15])))
                   .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => (DateTime)s[16]))
                   .ForMember(d => d.DateUpdated, opt => opt.MapFrom(s => (DateTime)s[17]));
            CreateMap<DbDataReader, ResultSummary>()
                   .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s[0].ToString()))
                   .ForMember(d => d.BookingCapacity, opt => opt.MapFrom(s => (int) s[1]))
                   .ForMember(d => d.Bookings, opt => opt.MapFrom(s => (int) s[2]))
                   .ForMember(d => d.Tests, opt => opt.MapFrom(s => (int) s[3]))
                   .ForMember(d => d.PositiveCases, opt => opt.MapFrom(s => (int)s[4]))
                   .ForMember(d => d.NegativeCases, opt => opt.MapFrom(s => (int)s[5]))
                   .ForMember(d => d.AwaitingResult, opt => opt.MapFrom(s => (int)s[6]));  
        }
    }
}

