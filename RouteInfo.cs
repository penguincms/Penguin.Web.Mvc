using Microsoft.AspNetCore.Routing;

namespace Penguin.Web.Mvc
{
    /// <summary>
    /// A class containing route information
    /// </summary>
    public class RouteInfo
    {
        #region Properties

        /// <summary>
        /// The route action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The route Area
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// The route controller
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Returns true if Area is set
        /// </summary>
        public bool IsArea => !string.IsNullOrWhiteSpace(Area);

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructs a new instance of this class using the provided route data
        /// </summary>
        /// <param name="toParse">the route data to use to populate the instance</param>
        public RouteInfo(RouteData toParse)
        {
            if (toParse is null)
            {
                throw new System.ArgumentNullException(nameof(toParse));
            }

            Controller = toParse.Values["controller"]?.ToString();
            Action = toParse.Values["action"]?.ToString();
            Area = toParse.Values["area"] != null ? toParse.Values["area"].ToString() : string.Empty;
        }

        #endregion Constructors
    }
}