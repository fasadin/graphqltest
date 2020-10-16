using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace graphqltest.Common.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureHandlingExceptions(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
    
                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            
            return applicationBuilder;
        }
    }
}