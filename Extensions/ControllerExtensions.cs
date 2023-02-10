using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace Penguin.Web.Mvc.Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class ControllerExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        #region Methods

        /// <summary>
        /// Renders the view to a string
        /// </summary>
        /// <typeparam name="TModel">The object model type</typeparam>
        /// <param name="controller">The current controller</param>
        /// <param name="viewName">The view name</param>
        /// <param name="model">The view model</param>
        /// <param name="partial">True if the view is a partial</param>
        /// <returns>A task that will contain the result of the render</returns>
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (controller is null)
            {
                throw new System.ArgumentNullException(nameof(controller));
            }

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using StringWriter writer = new();
            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

            if (viewResult.Success == false)
            {
                return $"A view with the name {viewName} could not be found";
            }

            ViewContext viewContext = new(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext).ConfigureAwait(false);

            return writer.GetStringBuilder().ToString();
        }

        #endregion Methods
    }
}