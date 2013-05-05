using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RichWeb.Helpers;

namespace RichWeb.Controllers
{
    public abstract class ControllerBase : Controller
    {
		protected ChatUser ChatUser;

		protected virtual void AuthorizeUser( ActionExecutingContext filterContext )
		{
			ChatUser = MvcApplication.GetUserFromSession();
			//if( ChatUser == null )
			//	throw new Exception( "Not logged in." );
		}
		
		protected override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			AuthorizeUser( filterContext );
			if( ChatUser == null )
				return;

			base.OnActionExecuting( filterContext );
		}

    }
}
