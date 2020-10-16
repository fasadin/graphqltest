using System;
using graphqltest.Common.Data;
using graphqltest.Common.Extensions;
using graphqltest.Data;
using graphqltest.Errors;
using graphqltest.Mutations;
using graphqltest.Providers;
using graphqltest.Queries;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace graphqltest
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: remove before production
            IdentityModelEventSource.ShowPII = true;
            
            RegistergraphqltestServices(services);
            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            services.Configure<Settings>(_configuration.GetSection("Settings"));

            services.AddJwt(settings.AuthOptions);
            
            services.AddCors();
            
            services.AddDbContext<graphqltestContext>(options =>
                options.UseSqlite("Data Source=Database.db"));


            services.AddHttpClient<ILanguageProvider, LanguageProvider>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5020");
            });
            
            // Add in-memory event provider for subscriptions
            //services.AddInMemorySubscriptionProvider();

            AddGraphqlServices(services);
        }

        private static void RegistergraphqltestServices(IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IPostProvider, PostProvider>();
        }

        private void AddGraphqlServices(IServiceCollection services)
        {
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddServices(sp)
                .AddQueryType(d => d.Name("Query"))
                .AddMutationType(d => d.Name("Mutation"))
                //.AddSubscriptionType(d => d.Name("Subscription"))
                //.AddType<UserQuery>()
                //.AddType<UserMutation>()
                .AddType<PostQuery>()
                .AddType<PostMutation>()
                .AddType<LanguageQuery>()
                .AddAuthorizeDirectiveType()
                //.Create(),new QueryExecutionOptions { ForceSerialExecution = true });
                .Create());

            services.AddErrorFilter<graphqltestErrorFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ConfigureHandlingExceptions();
                app.UsePlayground();
            }

            app
                .UseRouting()
                // .UseHsts()
                .UseHttpsRedirection()
                // .UseCors(policy => {
                //     policy.AllowAnyHeader();
                //     policy.AllowAnyMethod();
                //     policy.SetIsOriginAllowed(origin => true); // allow any origin
                //     policy.AllowCredentials();
                // })
                .UseMiddleware<JwtMiddleware>()
                .UseAuthentication()
                .UseAuthorization()
                .UseWebSockets()
                .UseGraphQL("/api2");
        }

    }

}