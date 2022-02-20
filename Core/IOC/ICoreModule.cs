using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IOC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceDescriptors); 
    }
}
