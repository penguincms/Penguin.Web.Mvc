namespace Penguin.Web.Mvc
{
    /// <summary>
    /// A class representing a set of view parameters
    /// </summary>
    public class ViewParameters
    {
        #region Properties

        /// <summary>
        /// The Master Name
        /// </summary>
        public string MasterName { get; set; }

        /// <summary>
        /// The View Model
        /// </summary>
        public object Model { get; set; }

        /// <summary>
        /// The View Name
        /// </summary>
        public string ViewName { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructs the class with the given parameters
        /// </summary>
        /// <param name="viewName">The view name</param>
        /// <param name="masterName">The master name</param>
        /// <param name="model">The view model</param>
        public ViewParameters(string viewName, string masterName, object model)
        {
            this.ViewName = viewName;
            this.MasterName = masterName;
            this.Model = model;
        }

        /// <summary>
        /// Constructs the class with the given parameters
        /// </summary>
        /// <param name="viewName">The view name</param>
        /// <param name="model">The view model</param>
        public ViewParameters(string viewName, object model)
        {
            this.ViewName = viewName;
            this.Model = model;
        }

        #endregion Constructors
    }
}