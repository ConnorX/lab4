using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace comp2007_lesson11_mon
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
            var serverError = Server.GetLastError() as HttpException;
            Exception exc = Server.GetLastError();

            if (null != serverError)
            {
                int errorCode = serverError.GetHttpCode();

                if (404 == errorCode)
                {
                    Server.ClearError();
                    Server.Transfer("/404.aspx");
                }
            }
            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                Server.Transfer("/error.aspx?handler=Application_Error%20-%20Global.asax", true);
            }
        }
    }
}