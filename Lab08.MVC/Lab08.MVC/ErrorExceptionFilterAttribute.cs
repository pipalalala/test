using System;
using System.Web.Mvc;
using Lab08.MVC.Business;
using NLog;

namespace Lab08.MVC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ErrorExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled
                && (filterContext.Exception is ErrorException
                    || filterContext.Exception is System.Data.SqlClient.SqlException
                    || filterContext.Exception is ArgumentNullException))
            {
                Logger.Error(filterContext.Exception);

                filterContext.Result = new ViewResult { ViewName = "ErrorExceptionView" };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}