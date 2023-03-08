using ContactManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ContactManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                name: "AllowDevOrigin",
                builder =>
                {
                    builder.AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:4200");
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactManager", Version = "v1" });
            });
            services.AddDbContext<ContactDbContext>(opt => opt.UseNpgsql(Configuration["DefaultConnectionString"]));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeAPI v1"));
            }

            app.UseCors("AllowDevOrigin");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
