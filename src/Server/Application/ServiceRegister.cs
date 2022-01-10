using System.Collections.Generic;
using System.Reflection;
using Application.Commands;
using Application.DTO;
using Application.Queries;
using Domain.Aggregates;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services
              .AddTransient<IRequestHandler<SaveEntity<BookingPostRequestBody,Booking>, Booking>,
                  SaveEntityHandler<BookingPostRequestBody,Booking>>();
            services
                .AddTransient<IRequestHandler<SaveEntity<SpaceRequestBody,Space>, Space>,
                    SaveEntityHandler<SpaceRequestBody,Space>>();
            services
                .AddTransient<IRequestHandler<SaveEntity<ResultPostRequestBody,Result>, Result>,
                    SaveEntityHandler<ResultPostRequestBody,Result>>();
            services
                .AddTransient<IRequestHandler<UpdateEntity<BookingPatchRequestBody,Booking>, Booking>,
                    UpdateEntityHandler<BookingPatchRequestBody,Booking>>();
            services
              .AddTransient<IRequestHandler<UpdateEntity<ResultPatchRequestBody,Result>, Result>,
                  UpdateEntityHandler<ResultPatchRequestBody,Result>>();
            services
                .AddTransient<IRequestHandler<GetSummary<UserSummary,User>, IEnumerable<UserSummary>>,
                    GetSummaryHandler<UserSummary,User>>();
            services
                .AddTransient<IRequestHandler<GetSummary<ResultSummary,Result>, IEnumerable<ResultSummary>>,
                    GetSummaryHandler<ResultSummary,Result>>();
            services
                .AddTransient<IRequestHandler<GetEntity<User>, IEnumerable<User>>,
                    GetEntityHandler<User>>();
            services
                .AddTransient<IRequestHandler<GetEntity<Result>, IEnumerable<Result>>,
                    GetEntityHandler<Result>>();
            services
                .AddTransient<IRequestHandler<GetEntity<Booking>, IEnumerable<Booking>>,
                    GetEntityHandler<Booking>>();
            services
                .AddTransient<IRequestHandler<GetEntity<Space>, IEnumerable<Space>>,
                    GetEntityHandler<Space>>();
            services
                .AddTransient<IRequestHandler<GetEntityById<Booking>, Booking>,
                    GetEntityByIdHandler<Booking>>();
            services
              .AddTransient<IRequestHandler<GetEntityById<Result>, Result>,
                  GetEntityByIdHandler<Result>>();
        
            return services;
        }
    }
}