using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Api.Application.Exceptions
{
    public static class ConfigureExceptionMiddleware //Extension method'lar yalnızca static sınıflar içinde tanımlanabilir.
    {
        public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>(); //static sınıflar generic argüman olarak kullanılamaz.
        }
    }
}
