using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IFileService
    {
        string GetFullPath(string relativePathFromConfig, string fileName);
        Task<string> ReadFileContentAsync(string relativePathFromConfig, string fileName);
    }

    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public FileService(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string GetFullPath(string relativePathFromConfig, string fileName)
        {
            var relativePath = _configuration[relativePathFromConfig];
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentException($"'{relativePathFromConfig}' ayarı bulunamadı.");

            return Path.Combine(_environment.ContentRootPath, relativePath, fileName);
        }

        public async Task<string> ReadFileContentAsync(string relativePathFromConfig, string fileName)
        {
            var fullPath = GetFullPath(relativePathFromConfig, fileName);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"{fileName} bulunamadı: {fullPath}");

            return await File.ReadAllTextAsync(fullPath);
        }
    }
}
