using System.Collections.Generic;
using System.Reflection;
using Application.Commands;
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
              .AddTransient<IRequestHandler<SaveEntity<Booking>, Booking>,
                  SaveEntityHandler<Booking>>();
            services
              .AddTransient<IRequestHandler<SaveEntity<Location>, Location>,
                  SaveEntityHandler<Location>>();
            services
                .AddTransient<IRequestHandler<SaveEntity<Space>, Space>,
                    SaveEntityHandler<Space>>();
            services
                .AddTransient<IRequestHandler<SaveEntity<Result>, Result>,
                    SaveEntityHandler<Result>>();
            services
                .AddTransient<IRequestHandler<UpdateEntity<Booking>, Booking>,
                    UpdateEntityHandler<Booking>>();
            services
              .AddTransient<IRequestHandler<UpdateEntity<Result>, Result>,
                  UpdateEntityHandler<Result>>();
            services
                .AddTransient<IRequestHandler<GetSummary<User, UserSummary>, UserSummary>,
                    GetSummaryHandler<User, UserSummary>>();
            services
                .AddTransient<IRequestHandler<GetSummary<Result, ResultSummary>, ResultSummary>,
                    GetSummaryHandler<Result,ResultSummary>>();
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
                .AddTransient<IRequestHandler<GetEntity<Location>, IEnumerable<Location>>,
                    GetEntityHandler<Location>>();
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