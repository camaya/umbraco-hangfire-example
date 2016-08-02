using Owin;
using Umbraco.Web;
using Hangfire;
using UmbracoHangfireExample.Filters;

namespace UmbracoHangfireExample.App_Start
{
    public class Startup : UmbracoDefaultOwinStartup
    {

        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);

            // Configure the database where Hangfire is going to create its tables
            var cs = Umbraco.Core.ApplicationContext.Current.DatabaseContext.ConnectionString;
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(cs);

            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new UmbracoUserAuthorizationFilter()
                }
            };
            
            // Configure Hangfire's dashboard with auth configuration
            app.UseHangfireDashboard("/hangfire", dashboardOptions);

            // Create a default Hangfire server
			app.UseHangfireServer();
        }

    }
}