using System.Web.Mvc;

namespace Lab08.MVC
{
    public class FilterConfig
    {
        protected FilterConfig()
        { }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrorExceptionFilterAttribute());
        }
    }
}