using System;
using System.Collections.Generic;
using System.Text;

namespace Penguin.Web.Mvc
{
    public class ViewParameters
    {
        #region Properties

        public string MasterName { get; set; }

        public object Model { get; set; }

        public string ViewName { get; set; }

        #endregion Properties

        #region Constructors

        public ViewParameters(string viewName, string masterName, object model)
        {
            this.ViewName = viewName;
            this.MasterName = masterName;
            this.Model = model;
        }

        public ViewParameters(string viewName, object model)
        {
            this.ViewName = viewName;
            this.Model = model;
        }

        #endregion Constructors
    }
}
