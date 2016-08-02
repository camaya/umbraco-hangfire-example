using Hangfire.Dashboard;
using System;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Security;
using Hangfire.Annotations;

namespace UmbracoHangfireExample.Filters
{
    public class UmbracoUserAuthorizationFilter : IDashboardAuthorizationFilter
    {

        public bool Authorize([NotNull] DashboardContext context)
        {
            var auth = new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket();
            if (auth == null)
            {
                return false;
            }

            var backofficeUser = ApplicationContext.Current.Services.UserService.GetByUsername(auth.Name);
            var isAllowed = backofficeUser != null &&
                backofficeUser.AllowedSections.Any(x => x.Equals("developer", StringComparison.InvariantCultureIgnoreCase));
            return isAllowed;
        }

    }
}