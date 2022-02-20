using System;
using System.Collections.Generic;
using System.Text;
using Core.IOC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public  static class ServicesCollectionExtension
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceDescriptors,ICoreModule[] coreModules)
        {
            foreach (var module in coreModules)
            {
                module.Load(serviceDescriptors);
            }

            return ServiceTool.Create(serviceDescriptors);
        }
    }
}
