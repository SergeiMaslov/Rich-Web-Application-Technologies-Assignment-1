using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RichWeb.Helpers;

namespace RichWeb.Hubs
{
	class Message
	{
		public string User;
		public string Msg;
		public DateTime Time;

		public Message( string user, string message )
		{
			User = user;
			Msg = message;
			Time = DateTime.Now;
		}
	}

    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
		private static Message[] _history = new Message[ 20 ];
		private static int _historyNdx = -1;

		public void Send( string newMessage )
		{
			var user = GetUser();

			if( user != null )
			{
				if( newMessage != null )
				{
					var msg = new Message( user.Name, newMessage );
					AddToHistory( msg );
					Clients.All.addNewMessage( msg.Time.ToString( "HH:mm:ss" ), user.Name, newMessage );
				}
			}
		}

		public void GetHistory()
		{
			var user = GetUser();

			if( user != null )
				SendHistory( Clients.Caller );
		}

		private ChatUser GetUser()
		{
			var user = MvcApplication.GetUserFromSession();

			if( user == null )
				Clients.Caller.doCommand( "login" );
			
			return user;
		}

		private void AddToHistory( Message message )
		{
			_historyNdx = (_historyNdx + 1) % _history.Length;
			_history[ _historyNdx ] = message;
		}

		private void SendHistory(dynamic caller)
		{
			int ndx = (_historyNdx + 1) % _history.Length;
			if( _history[ ndx ] == null )
				ndx = 0;

			for( int i = 0; i < _history.Length; i++ )
			{
				Message msg = _history[ ndx ];
				if( msg == null )
					break;

				caller.addNewMessage( msg.Time.ToString( "HH:mm:ss" ), msg.User, msg.Msg );
				ndx = (ndx + 1) % _history.Length;
			}
		}
    }
}