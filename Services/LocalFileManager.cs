

namespace BooksApp.Services;

public class LocalFileManager : IFileManager
{
    private readonly IWebHostEnvironment env;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LocalFileManager(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        this.env = env;
        this.httpContextAccessor = httpContextAccessor;
    }

    public Task DeleteFile(string route, string container)
    {
        if (route != null)
        {
            var fileName = Path.GetFileName(route);
            string fileDirectory = Path.Combine(env.WebRootPath, container, fileName);

            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }
        }

        return Task.FromResult(0);

    }

    public async Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
    {
        await DeleteFile(route, container);
        return await SaveFile(content, extension, container, contentType);
    }

    public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
    {
        var fileName = $"{Guid.NewGuid()}{extension}";
        string folder = Path.Combine(env.WebRootPath, container);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string ruta = Path.Combine(folder, fileName);
        await File.WriteAllBytesAsync(ruta, content);

        var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
        var urlParaBD = Path.Combine(urlActual, container, fileName).Replace("\\", "/");
        return urlParaBD;
    }
}
