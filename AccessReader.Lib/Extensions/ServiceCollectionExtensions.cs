using AccessReader.Lib.AssetReader;
using Microsoft.Extensions.DependencyInjection;

namespace AccessReader.Lib.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccessReader(this IServiceCollection services) =>
            services.AddTransient<IAccessReader, AccessFileReader>();
    }
}