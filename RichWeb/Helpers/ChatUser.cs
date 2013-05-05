using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;

namespace RichWeb.Helpers
{
	[Serializable]
	public class ChatUser
	{
		public string Name;
		public string PasswordHash;

		public ChatUser()
		{
		}

		public ChatUser( string name, string passwordHash )
		{
			Name = name;
			PasswordHash = passwordHash;
		}
	}
}