using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Penguin.Web.Mvc.Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ActionResultExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Captures the result and returns a string
        /// </summary>
        /// <param name="result">The result to capture</param>
        /// <param name="controllerContext">The current controller context</param>
        /// <returns>The result in string form</returns>
        public static string Capture(this ActionResult result, ControllerContext controllerContext)
        {
            using (ResponseCapture it = new ResponseCapture(controllerContext.HttpContext.Response))
            {
                result.ExecuteResult(controllerContext);
                return it.ToString();
            }
        }
    }
}
