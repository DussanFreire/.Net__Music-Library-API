using MusicLibraryAPI.Services;
using MusicLibraryAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using MusicLibraryAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MusicLibraryAPI
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
            services.AddControllers().AddNewtonsoftJson(); 
            services.AddTransient<IArtistsService, ArtistService>();
            services.AddTransient<IAlbumsService, AlbumService>();
            services.AddTransient<ISongsService, SongService>();
            services.AddTransient<IAlbumsWithReproductionsService, AlbumWithReproductionsService>();
            services.AddTransient<IMusicLibraryRepository, MusicLibraryRepository>();

            
            //automapper configuration
            services.AddAutoMapper(typeof(Startup));

            //entity framework configuration  MusicLibraryConnection
            services.AddDbContext<MusicLibraryDbContext>( options => {
                options.UseSqlServer(Configuration.GetConnectionString("MusicLibraryConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
