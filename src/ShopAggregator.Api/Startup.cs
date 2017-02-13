using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopAggregator.Api.Configuration;
using ShopAggregator.Api.Db;
using Swashbuckle.AspNetCore.Swagger;

namespace ShopAggregator.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.AddMvc();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "ShopAggregator API", Version = "v1"}); });

            var connectionString = Configuration.GetConnectionString("ShopAggregator");
            services.AddDbContext<ShopAggregatorContext>(options => options.UseMySql(connectionString));
            services.AddSingleton<ShopAggregatorUnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopAggregator API V1"); });
        }
    }
}