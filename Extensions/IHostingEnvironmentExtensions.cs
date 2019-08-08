using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Penguin.Web.Mvc.Extensions
{
    public static class IHostingEnvironmentExtensions
    {
        #region Properties

        public static bool IsHosted => string.Compare(Process.GetCurrentProcess().ProcessName, "w3wp") == 0;

        #endregion Properties

        #region Methods

        public static string MapApplication(this IHostingEnvironment hostingEnvironment, string virtualPath) => Map(hostingEnvironment.ContentRootPath, virtualPath);

        public static string MapPath(this IHostingEnvironment hostingEnvironment, string virtualPath) => hostingEnvironment.MapApplication(virtualPath);

        public static string MapPublic(this IHostingEnvironment hostingEnvironment, string virtualPath) => Map(hostingEnvironment.WebRootPath, virtualPath);

        private static string Map(string root, string virtualPath)
        {
            string toReturn = virtualPath;

            if (virtualPath.StartsWith("~"))
            {
                toReturn = new FileInfo(Path.Combine(root, virtualPath.Substring(2))).FullName;
            }

            return toReturn;
        }

        #endregion Methods
    }
}
