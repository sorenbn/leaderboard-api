using Application.Features.ScoreEntries.Commands.Post;
using Application.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblyContaining<PostScoreEntryValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationMiddleware<,>));

            return services;
        }
    }
}
