using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WitnessReport.Configuration;
using WitnessReport.Services;
using WitnessReport.Services.Interfaces;

namespace WitnessReport
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IIpAddressService, IpAddressService>();
            services.AddScoped<IGeolocationService, GeolocationService>();
            services.AddScoped<IPhoneNumberValidityService, PhoneNumberValidityService>();
            services.AddScoped<IBaseHttpClient, BaseHttpClient>();
            services.AddScoped<IFbiDataService, FbiDataService>();

            services.Configure<IpGeolocationConfiguration>(Configuration.GetSection("IpGeolocationConfiguration"));
            services.Configure<FbiConfiguration>(Configuration.GetSection("FBIConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
