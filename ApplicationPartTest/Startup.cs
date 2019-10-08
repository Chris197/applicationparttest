using McMaster.NETCore.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ApplicationPartTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddMvc()                
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Module\bin\Debug\netstandard2.0\Module.dll");
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            //var loader = PluginLoader.CreateFromAssemblyFile(path);
            var loader = PluginLoader.CreateFromAssemblyFile(path, PluginLoaderOptions.PreferSharedTypes);
            var pluginAssembly = loader.LoadDefaultAssembly();

            var partFactory = ApplicationPartFactory.GetApplicationPartFactory(pluginAssembly);
            foreach (var part in partFactory.GetApplicationParts(pluginAssembly))
            {
                Console.WriteLine($"* {part.Name}");
                mvcBuilder.PartManager.ApplicationParts.Add(part);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvcWithDefaultRoute();
            app.UseMvc();
        }
    }
}
