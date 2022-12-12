using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace ScrumBoard.Common.Extensions.WebBuilderExtensions
{
    public static class AddAppSettingsFile
    {
        public static WebApplicationBuilder AddLocalAppSettings(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("appsettings.Local.json", true, true);
            return builder;
        }
    }
}
