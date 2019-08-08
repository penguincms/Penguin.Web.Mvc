using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Penguin.Web.Mvc
{
    public class RouteInfo
    {
        #region Properties

        public string Action { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public bool IsArea => !string.IsNullOrWhiteSpace(this.Area);

        #endregion Properties

        #region Constructors

        public RouteInfo(RouteData toParse)
        {
            this.Controller = toParse.Values["controller"]?.ToString();
            this.Action = toParse.Values["action"]?.ToString();
            this.Area = toParse.Values["area"] != null ? toParse.Values["area"].ToString() : string.Empty;
        }

        #endregion Constructors
    }
}
