﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace TeamCores.Common
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly MiddlewareOptions _options;
        /// <summary>
        /// Creates a default web page for new applications.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public Middleware(RequestDelegate next, MiddlewareOptions options)
        {
			_options = options ?? throw new ArgumentNullException(nameof(options));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            HttpRequest request = context.Request;
            MiddlewareConfig.Configuration = _options.Configuration;

            return _next(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCommonMiddleware(this IApplicationBuilder builder, MiddlewareOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.Use(next => new Middleware(next, options).Invoke);
        }

        public static IApplicationBuilder UseCommonMiddleware(this IApplicationBuilder builder, IConfigurationRoot config)
        {
            return UseCommonMiddleware(builder, new MiddlewareOptions { Configuration = config });
        }
    }

    public class MiddlewareOptions
    {
        public IConfigurationRoot Configuration { get; set; }
    }

    internal class MiddlewareConfig
    {
        public static IConfigurationRoot Configuration { get; set; }
    }
}
