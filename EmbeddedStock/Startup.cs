using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock.Data;
using MySql.Data.MySqlClient;

namespace EmbeddedStock
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
            Uri dbUri = new Uri(@"mysql://tsxzu20hi5xnyur4:zwmapp17fg54t9ff@vlvlnl1grfzh34vj.chr7pe7iynqr.eu-west-1.rds.amazonaws.com:3306/ai455og2qxv4iucl");

            var mySqlConnectionString = new MySqlConnectionStringBuilder
            {
                Server = dbUri.Host,
                Database = dbUri.LocalPath.Replace("/",""),
                UserID = dbUri.UserInfo.Split(":")[0],
                Password = dbUri.UserInfo.Split(":")[1],
                Port = (uint) dbUri.Port
            };
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(mySqlConnectionString.ConnectionString));
            
            services.AddMvc();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Component}/{action=Index}/{id?}");
            });
        }
    }
}