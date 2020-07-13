using Microsoft.AspNetCore.Mvc.Filters;
using Penguin.Web.Mvc.Extensions;
using System.Security;

namespace Penguin.Web.Mvc.Attributes
{
    /// <summary>
    /// Throws a security exception if the connection calling the action is not local
    /// </summary>
    public sealed class IsLocalAttribute : ActionFilterAttribute
    {
        #region Methods

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        public override void OnActionExecuting(ActionExecutingContext filterContext)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            if (filterContext is null)
            {
                throw new System.ArgumentNullException(nameof(filterContext));
            }

            bool isLocal = filterContext.HttpContext.Request.IsLocal();

            if (!isLocal)
            {
                throw new SecurityException();
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

        #endregion Methods
    }
}