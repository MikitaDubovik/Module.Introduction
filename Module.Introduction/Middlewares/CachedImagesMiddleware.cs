using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;
using Module.Introduction.Services;

namespace Module.Introduction.Middlewares
{
    public class CachedImagesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _applicationSettings;

        private string _previousValue;

        private DateTime _previousTime;
        private readonly int _maximumPeriodBetweenRequest;

        private const string FolderName = @"D:\Work\CachedImages";

        public CachedImagesMiddleware(RequestDelegate next, IOptions<ApplicationSettings> settingsOptions)
        {
            _next = next;
            _applicationSettings = settingsOptions.Value;
            _maximumPeriodBetweenRequest = _applicationSettings.MaximumPeriodBetweenRequest;
        }

        public async Task InvokeAsync(HttpContext context, IFilesService filesService)
        {
            if (context.Request.Path.Value.Contains("Categories/GetImage") || context.Request.Path.Value.Contains("Categories/image") || context.Request.Path.Value.Contains("Categories/GetFile"))
            {
                var currentTime = DateTime.Now;
                if ((currentTime - _previousTime).Minutes > _maximumPeriodBetweenRequest)
                {
                    if (Directory.Exists(FolderName))
                    {
                        Directory.Delete(FolderName, true);
                    }

                    _previousTime = currentTime;
                    await _next(context);
                }

                var id = int.Parse(context.Request.Path.Value.Split('/').Last());
                var ms = filesService.Read(id);
                if (ms != null)
                {
                    await context.Response.Body.WriteAsync(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                }

                await _next(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
