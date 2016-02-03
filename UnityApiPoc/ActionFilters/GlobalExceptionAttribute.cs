namespace UnityApiPoc.ActionFilters
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using System.Web.Http.Tracing;

    using UnityApiPoc.Helpers;

    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Error(
                context.Request,
                "Controller: " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                + Environment.NewLine + "Action: " + context.ActionContext.ActionDescriptor.ActionName,
                context.Exception);
        }
    }
}