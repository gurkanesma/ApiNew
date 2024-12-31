using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration) { } 
    }
}
