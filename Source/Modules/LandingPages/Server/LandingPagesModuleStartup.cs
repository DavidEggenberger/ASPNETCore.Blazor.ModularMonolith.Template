﻿using Blazored.Modal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.Misc.Modules;
using System.Reflection;

namespace Modules.LandingPages.Web
{
    public class LandingPagesModuleStartup : IModuleStartup
    {
        public Assembly FeaturesAssembly => null;

        public void ConfigureServices(IServiceCollection services, IConfiguration config = null)
        {
            services.AddBlazoredModal();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
