using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RichWeb.Helpers;
using RichWeb.Models;

namespace RichWeb.Controllers
{
    public class ChatController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View((object)(ChatUser != null? ChatUser.Name : null));
        }

		/*
        [HttpPost]
        public JsonResult GetChatHistory(string username)
        {
            List<Record> chatHistory = HistoryReader.GetInstance().GetHistory(username);

            return Json(chatHistory);
        }*/

		[HttpPost]
		public JsonResult Login( string username, string password )
		{
			try
			{
				ChatUser user = MvcApplication.UsersDatabase.GetUser( username, password );
				
				if( user != null )
				{
					MvcApplication.AddUserToSession( user );
					return Json( "ok" );
				}
				else
				{
					return Json( "Wrong login or password." );
				}
			}
			catch( Exception e )
			{
				return Json( e.Message );
			}
		}

		[HttpPost]
		public JsonResult Signup( string username, string password )
		{
			try
			{
				if( username.IndexOf( ':' ) >= 0 )
					return Json( "Login can not contain a colon." );

				if( MvcApplication.UsersDatabase.IsUserExists( username ) )
					return Json( "This login name already exists." );

				var user = MvcApplication.UsersDatabase.AddUser( username, password );
				MvcApplication.AddUserToSession( user );
					return Json( "ok" );
			}
			catch (Exception e)
			{
				return Json( e.Message );
			}
		}

		[HttpPost]
		public void Logout()
		{
			Session.Clear();
		}
    }
}
