namespace UnityApiPoc
{
    using System.Web.Mvc;

    using UnityApiPoc.ActionFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
