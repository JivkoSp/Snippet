using SnippetApi.Data.Repository.Generic;
using SnippetApi.Data.Repository.Interfaces;
using SnippetApi.Data.Repository.Repositories;
using SnippetApi.Data.Repository.UnitOfWork;

namespace SnippetApi.Extensions
{
    public static class ServiseExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGroupRepo, GroupRepo>();
            services.AddScoped<ICommandRepo, CommandRepo>();
        }
    }
}
