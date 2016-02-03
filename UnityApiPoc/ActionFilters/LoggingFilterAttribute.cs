namespace UnityApiPoc.ActionFilters
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Http.Tracing;

    using UnityApiPoc.Helpers;

    using ITraceWriter = Newtonsoft.Json.Serialization.ITraceWriter;

    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(
                context.Request,
                "Controller: " +
                context.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine
                + "Action: " + context.ActionDescriptor.ActionName,
                "JSON",
                context.ActionArguments);
        }
    }
}