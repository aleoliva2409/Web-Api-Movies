namespace WebAPIMovies.Services
{
    public class StoreFilesLocal : IStoreFiles
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StoreFilesLocal(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string path, string container)
        {
            if (path != null)
            {
                var fileName = Path.GetFileName(path);
                string fileDirectory = Path.Combine(env.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                {
                    File.Delete(fileDirectory);
                }
            }
                return Task.CompletedTask;
        }

        public async Task<string> EditFile(byte[] content, string extension,
            string container, string path, string contentType)
        {
            await DeleteFile(path, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension,
            string container, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}.{extension}";
            var folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var pathFile = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(pathFile, content);

            var url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlForDB = Path.Combine(url, container, fileName).Replace("\\", "/");
            return urlForDB;
        }
    }
}
