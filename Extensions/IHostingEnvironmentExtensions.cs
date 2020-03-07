using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;

namespace Penguin.Web.Mvc.Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class IHostingEnvironmentExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        #region Properties

        /// <summary>
        /// True if hosted on IIS
        /// </summary>
        public static bool IsHosted => string.Compare(Process.GetCurrentProcess().ProcessName, "w3wp") == 0;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Maps the path to the content root path
        /// </summary>
        /// <param name="hostingEnvironment">The current hosting environment</param>
        /// <param name="virtualPath">The path to map</param>
        /// <returns>The absolute mapped path</returns>
        public static string MapApplication(this IHostingEnvironment hostingEnvironment, string virtualPath)
        {
            return Map(hostingEnvironment.ContentRootPath, virtualPath);
        }

        /// <summary>
        /// Maps the path to the content root path
        /// </summary>
        /// <param name="hostingEnvironment">The current hosting environment</param>
        /// <param name="virtualPath">The path to map</param>
        /// <returns>The absolute mapped path</returns>
        public static string MapPath(this IHostingEnvironment hostingEnvironment, string virtualPath)
        {
            return hostingEnvironment.MapApplication(virtualPath);
        }

        /// <summary>
        /// Maps the path to the www root path
        /// </summary>
        /// <param name="hostingEnvironment">The current hosting environment</param>
        /// <param name="virtualPath">The path to map</param>
        /// <returns>The absolute mapped path</returns>
        public static string MapPublic(this IHostingEnvironment hostingEnvironment, string virtualPath)
        {
            return Map(hostingEnvironment.WebRootPath, virtualPath);
        }

        private static string Map(string root, string virtualPath)
        {
            string toReturn = virtualPath;

            if (virtualPath[0] == '~')
            {
                toReturn = new FileInfo(Path.Combine(root, virtualPath.Substring(2))).FullName;
            }

            return toReturn;
        }

        #endregion Methods
    }
}