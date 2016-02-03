namespace UnityApiPoc.Extension
{
    using System;
    using System.Diagnostics;
    using System.Web;

    public class MyHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose()
        { 
            // TODO Any required disposal
        }

        protected void BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var correlationId = app.Request.Headers["Correlation-Id"];
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                app.Request.Headers.Add("Correlation-Id", correlationId);
            }

            BeginRequest(app);
        }

        protected void BeginRequest(HttpApplication application)
        {
            const string Mask = "{0} last accessed the site at {1}. Correlation ID = {2}";
            try
            {
                Debug.WriteLine(string.Format(Mask, application.Request.UserHostAddress, DateTime.Now, application.Request.Headers["Correlation-Id"]));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected void EndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            app.Response.Headers.Add("Correlation-Id", app.Request.Headers["Correlation-Id"]);

            EndRequest((HttpApplication)sender);
        }

        protected void EndRequest(HttpApplication application)
        {
            const string Mask = "{0} completed web request at {1}. Correlation ID = {2}";
            try
            {
                Debug.WriteLine(string.Format(Mask, application.Request.UserHostAddress, DateTime.Now, application.Request.Headers["Correlation-Id"]));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}