using Microsoft.AspNetCore.Mvc.Filters;
using Penguin.Web.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

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
