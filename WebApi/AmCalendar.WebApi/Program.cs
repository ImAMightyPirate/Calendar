// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.WebApi
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main program.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Entry point for the main program.
        /// </summary>
        /// <param name="args">The arguments supplied to the program.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates an ASP.NET Core host.
        /// </summary>
        /// <param name="args">The arguments supplied to the program.</param>
        /// <returns>The host builder instance.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
