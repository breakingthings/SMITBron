using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paramore.Brighter.Extensions.DependencyInjection;
using NSwag;
using Microsoft.Extensions.Options;
using LinqToDB.AspNet;
using System.Data.Common;
using System.Data;
using SMITBron.Infrastructure;
using SMITBron.DBMigrations;
using Microsoft.Extensions.Logging;
using Paramore.Darker.AspNetCore;
using Paramore.Darker.QueryLogging;
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidation;
using LinqToDB.Data;
using SMITBron.Web.Helpers;
using SMITBron.Web.Middlewares;

namespace SMITBron
{
    public class Startup
    {
        private const string DEV_CORS = "DevCORS";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddBrighter(options => {
                options.MapperLifetime = ServiceLifetime.Singleton;
                options.HandlerLifetime = ServiceLifetime.Scoped;
                options.CommandProcessorLifetime = ServiceLifetime.Scoped;
                
            }).AutoFromAssemblies(typeof(BookingService.Handlers.NewBookingHandler).Assembly, 
            typeof(HotelService.Handlers.GetAllBookingsHandler).Assembly);

            services.AddDarker(options =>
            {
                options.HandlerLifetime = ServiceLifetime.Scoped;
                options.QueryProcessorLifetime = ServiceLifetime.Scoped;
            })
             .AddHandlersFromAssemblies(typeof(SMITBron.HotelService.Responses.HotelApartmentResult).Assembly,
                typeof(SMITBron.BookingService.Handlers.CancelBookingHandler).Assembly
             )
             .AddJsonQueryLogging();

            services.AddValidatorsFromAssemblies(new Assembly[]
                    { typeof(SMITBron.HotelService.Responses.HotelApartmentResult).Assembly,
                    typeof(SMITBron.BookingService.Handlers.NewBookingHandler).Assembly});
            

            services.AddOpenApiDocument(conf =>
            {
                conf.Title = "SMIT Bron api";
            });

            services.AddCors(o => o.AddPolicy(DEV_CORS, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true)
                .WithExposedHeaders(new string[]
                {
                    APIBaseController.TimeTakenHeaderKey
                });
                
            }));

            services.AddLinqToDBContext<RWDbConnection>((provider, options) =>
            {
                options.UseConnectionString(LinqToDB.ProviderName.PostgreSQL, Configuration.GetConnectionString("Default"));
            });

            services.AddSingleton<RequestInfoHeadersMiddleware>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(DEV_CORS);
                DataConnection.TurnTraceSwitchOn(System.Diagnostics.TraceLevel.Verbose);
                DataConnection.WriteTraceLine = (message, displayname, tracelevel) =>
                { System.Diagnostics.Debug.WriteLine(message); };
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMiddleware<SMITBron.Web.Middlewares.RequestInfoHeadersMiddleware>();

            app.UseRouting();
            app.UseOpenApi();
            app.UseAuthorization();
            app.UseSwaggerUi3(c =>
            {

            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });




            ///Run dbup on startup
            DBMigrations.DBMigrations.Run(Configuration.GetConnectionString("Default"), 
                app.ApplicationServices.GetService<ILogger<DBMigrations.DBMigrations>>());
        }
    }
}
