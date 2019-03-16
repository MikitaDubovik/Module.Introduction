using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;

namespace Module.Introduction.Middlewares
{
    public class CachedImagesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _applicationSettings;

        private string _previousValue;
        private readonly int _maximumNumberOfImage;
        private int _currentNumberOfImage;

        private DateTime _previousTime;
        private readonly int _maximumPeriodBetweenRequest;

        private readonly string _folderName = Directory.GetCurrentDirectory() + "CachedImages";

        public CachedImagesMiddleware(RequestDelegate next, IOptions<ApplicationSettings> settingsOptions)
        {
            _next = next;
            _applicationSettings = settingsOptions.Value;
            _maximumNumberOfImage = _applicationSettings.MaximumNumberOfImage;
            _maximumPeriodBetweenRequest = _applicationSettings.MaximumPeriodBetweenRequest;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("Categories/GetImage") || context.Request.Path.Value.Contains("Categories/image"))
            {
                if (string.IsNullOrEmpty(_previousValue))
                {
                    _previousValue = context.Request.Path.Value.Split('/').Last();
                }
                else
                {
                    var currentTime = DateTime.Now;
                    if ((currentTime - _previousTime).Minutes > _maximumPeriodBetweenRequest)
                    {
                        if (Directory.Exists(_folderName))
                        {
                            Directory.Delete(_folderName, true);
                        }

                        _previousTime = currentTime;
                        await _next(context);
                    }

                    if (_previousValue.Equals(context.Request.Path.Value.Split('/').Last()))
                    {
                        if (Directory.Exists(_folderName))
                        {
                            //Get cached image bu _curr
                        }
                    }
                    else
                    {
                        //Most likely not here, but in CategoriesController
                        if (_currentNumberOfImage == 0)
                        {
                            //Create cached image
                            _currentNumberOfImage++;
                        }
                        if (_currentNumberOfImage == _maximumNumberOfImage)
                        {
                            _currentNumberOfImage = 1;
                            //Create cached image
                            _currentNumberOfImage++;
                        }
                    }

                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
