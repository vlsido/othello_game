using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace OthelloGameConsoleApp
{
    public class Helpers
    {
        public static readonly ILoggerFactory MyLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information)
                    .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                //.AddConsole();
            });

    }
}
