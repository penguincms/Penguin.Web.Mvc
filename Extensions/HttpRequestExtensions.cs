using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Penguin.Web.Mvc.Extensions
{
    public static class HttpRequestExtensions
    {
        #region Methods

        public static bool IsLocal(this HttpRequest req)
        {
            ConnectionInfo connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                if (connection.LocalIpAddress != null)
                {
                    return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
                }
                else
                {
                    return IPAddress.IsLoopback(connection.RemoteIpAddress);
                }
            }

            // for in memory TestServer or when dealing with default connection info
            if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
            {
                return true;
            }

            return false;
        }

        public static Uri Referer(this HttpRequest request) => new Uri(request.Headers["Referer"].ToString());

        public static Uri Url(this HttpRequest request) => new Uri($"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}");

        #endregion Methods
    }
}
