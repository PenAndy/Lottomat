using System.Web.Mvc;

namespace Lottomat.Application.Admin
{
    /// <summary>
    /// FilterConfig
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// RegisterGlobalFilters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandlerErrorAttribute());
        }
    }
}