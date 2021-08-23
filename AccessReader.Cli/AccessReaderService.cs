using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AccessReader.Lib.AssetReader;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AccessReader.Cli
{
    internal class AccessReaderService : IHostedService
    {
        private readonly IAccessReader _reader;
        private readonly AccessOptions _accessOptions;

        public AccessReaderService(
            IAccessReader reader,
            IOptions<AccessOptions> accessOptions)
        {
            _reader = reader;
            _accessOptions = accessOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if(File.Exists(_accessOptions.FileLocation) is not true)
                return Task.CompletedTask;

            using var file = File.OpenRead(_accessOptions.FileLocation);
            var fileData = _reader.ReadFile(file);

            var data = fileData.Pages
               .Select(page => Encoding.UTF8.GetString(page.Data))
               .Select(s => s.Replace("\0", ""))
               .ToArray();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}