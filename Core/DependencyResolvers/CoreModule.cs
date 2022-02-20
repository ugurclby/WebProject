using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Core.CrossCuttinConcerns.Caching;
using Core.CrossCuttinConcerns.Caching.Microsoft;
using Core.IOC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection; 
namespace Core.DependencyResolvers
{
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMemoryCache();//IMemoryCache
            serviceDescriptors.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceDescriptors.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceDescriptors.AddSingleton<Stopwatch>(); 
        }
    }
}
