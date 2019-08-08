using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Penguin.Web.Mvc.Extensions
{
    public static class ActionResultExtensions
    {
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
