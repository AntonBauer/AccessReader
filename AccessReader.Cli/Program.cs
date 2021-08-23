using AccessReader.Lib;
using AccessReader.Lib.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccessReader.Cli
{
    internal static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureServices((context, services) =>
                    services
                       .AddHostedService<AccessReaderService>()
                       .AddAccessReader()
                       .Configure<AccessOptions>(context.Configuration.GetSection(AccessOptions.Access))
                );
    }
}