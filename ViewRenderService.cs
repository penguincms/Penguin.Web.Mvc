using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Penguin.DependencyInjection.Abstractions.Attributes;
using Penguin.DependencyInjection.Abstractions.Enums;
using Penguin.Web.Mvc.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Penguin.Web.Mvc
{
    /// <summary>
    /// Contains a method to render a view to an HTML string representation
    /// </summary>
    [Register(ServiceLifetime.Scoped, typeof(IViewRenderService))]
    public class ViewRenderService : IViewRenderService
    {
        #region Constructors

        /// <summary>
        /// Constructs a new instance of the view render service
        /// </summary>
        /// <param name="razorViewEngine">A view engine implementation</param>
        /// <param name="tempDataProvider">A temp data provider</param>
        /// <param name="serviceProvider">A service provider for injection</param>
        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            this._razorViewEngine = razorViewEngine;
            this._tempDataProvider = tempDataProvider;
            this._serviceProvider = serviceProvider;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Renders a view by path, to an HTML string
        /// </summary>
        /// <param name="viewName">The name/path of the view</param>
        /// <param name="ExecutingPath">The executing path of the context in which its being requested</param>
        /// <param name="model">The model to pass into the view</param>
        /// <param name="Get">Used to determine whether or not the underlying FindView uses FindView or GetView</param>
        /// <returns>A task containing the result of an async rendering of an MVC view</returns>
        public async Task<string> RenderToStringAsync(string viewName, string ExecutingPath, object model, bool Get = false)
        {
            if (ExecutingPath is null)
            {
                throw new ArgumentNullException(nameof(ExecutingPath));
            }

            DefaultHttpContext httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            ActionContext actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (StringWriter sw = new StringWriter())
            {
                Microsoft.AspNetCore.Mvc.ViewEngines.ViewEngineResult viewResult;

                if (!Get)
                {
                    viewResult = this._razorViewEngine.FindView(actionContext, viewName, false);
                }
                else
                {
                    viewResult = this._razorViewEngine.GetView("", viewName, false);

                    //We dont know 100% where we are even executing from so this gives us a chance to let the caller tell us
                    if (viewResult.View == null && !string.IsNullOrWhiteSpace(ExecutingPath))
                    {
                        viewResult = this._razorViewEngine.GetView(ExecutingPath, viewName, false);
                    }
                }

                if (viewResult.View == null)
                {
                    if (ExecutingPath.Contains("\\"))
                    {
                        throw new ArgumentNullException($"{viewName} does not match any available view. Additionally your path contains the characters \"\\\". Are you sure you didn't mean \"/\"?");
                    }

                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                ViewDataDictionary viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                ViewContext viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, this._tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        #endregion Methods

        #region Fields

        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        #endregion Fields
    }
}