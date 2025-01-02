using Api.Application.Interfaces.Repositories;
using Api.Persistence.Context;
using Api.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Api.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration) 
        {

            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>)); //dependency ınjection
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));


        } 
    }
}
