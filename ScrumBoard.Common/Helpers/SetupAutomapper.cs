using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ScrumBoard.Common.Automapper;

namespace ScrumBoard.Common.Helpers
{
    public static class SetupAutomapperExtension
    {
        public static IServiceCollection SetupAutomapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(MapperProfile));
            return serviceCollection;
        }
    }
}
