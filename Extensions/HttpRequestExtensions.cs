using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Penguin.Web.Mvc.Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class HttpRequestExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        #region Methods

        /// <summary>
        /// True if the request is coming from localhost
        /// </summary>
        /// <param name="req">The request to check</param>
        /// <returns>True if the request is coming from localhost</returns>
        public static bool IsLocal(this HttpRequest req)
        {
            if (req is null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            ConnectionInfo connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                return connection.LocalIpAddress != null
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            // for in memory TestServer or when dealing with default connection info
            return connection.RemoteIpAddress == null && connection.LocalIpAddress == null;
        }

        /// <summary>
        /// Returns the request referer
        /// </summary>
        /// <param name="request">The request to get the referer for</param>
        /// <returns>The referer</returns>
        public static Uri Referer(this HttpRequest request)
        {
            string referer = request?.Headers["Referer"];

            return string.IsNullOrWhiteSpace(referer) ? null : new Uri(referer);
        }

        /// <summary>
        /// Gets the full request Url
        /// </summary>
        /// <param name="request">The request to check</param>
        /// <returns>The request Url</returns>
        public static Uri Url(this HttpRequest request)
        {
            return request is null
                ? throw new ArgumentNullException(nameof(request))
                : new Uri($"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}");
        }

        #endregion Methods
    }
}