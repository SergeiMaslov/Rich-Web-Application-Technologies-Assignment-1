using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using RichWeb.Helpers;

namespace RichWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
		private const string SESSION_VAR_USER = "SESSION_VAR_USER";
		private static RouteBase _hubRoute;

		public static UsersDatabase UsersDatabase
		{
			get;
			private set;
		}

		public static string Root
		{
			get;
			private set;
		}

        protected void Application_Start()
        {
			_hubRoute = RouteTable.Routes.MapHubs();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterAuth();

			Root = Server.MapPath( "~" );
			UsersDatabase = new UsersDatabase( Path.Combine( Root, "App_Data\\users.json" ) );
        }

		protected void Application_BeginRequest( object sender, EventArgs e )
		{
			if( IsSignalRRequest( Context ) )
			{
				// Turn readonly sessions on for SignalR
				Context.SetSessionStateBehavior( SessionStateBehavior.ReadOnly );
			}
		}

		private bool IsSignalRRequest( HttpContext context )
		{
			// If the routeData isn't null then it's a SignalR request
			RouteData routeData = _hubRoute.GetRouteData( new HttpContextWrapper( context ) );
			return routeData != null;
		}

		public static ChatUser GetUserFromSession()
		{
			ChatUser user = HttpContext.Current.Session[ SESSION_VAR_USER ] as ChatUser;
			return user;
		}

		public static void AddUserToSession( ChatUser user )
		{
			HttpContext.Current.Session[ SESSION_VAR_USER ] = user;
		}
    }
}