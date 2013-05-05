using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace RichWeb.Helpers
{
	public class UsersDatabase
	{
		private readonly string _dataFile;
		private List<ChatUser> _users = new List<ChatUser>();
		private JavaScriptSerializer _serializer = new JavaScriptSerializer();

		public UsersDatabase(string dbPath)
		{
			_dataFile = dbPath;

			if( File.Exists( _dataFile ) )
			{
				using( StreamReader stream = new StreamReader( _dataFile ) )
				{
					_users = _serializer.Deserialize( stream.ReadToEnd(), _users.GetType() ) as List<ChatUser>;
				}
			}
		}

		public ChatUser GetUser( string name, string password )
		{
			return _users.Where( it => it.Name.Equals( name, StringComparison.OrdinalIgnoreCase ) && it.PasswordHash == GetShaHash( password ) ).FirstOrDefault();
		}

		public bool IsUserExists( string name )
		{
			return _users.Where( it => it.Name.Equals( name, StringComparison.OrdinalIgnoreCase )).FirstOrDefault() != null;
		}

		public ChatUser AddUser( string name, string password )
		{
			//	adduser to list
			ChatUser user = new ChatUser( name, GetShaHash( password ) );
			_users.Add( user );
			
			//	save list into file
			using( StreamWriter stream = new StreamWriter( _dataFile ) )
			{
				stream.Write( _serializer.Serialize( _users ) );
			}

			return user;
		}

		private string GetShaHash( string text )
		{
			using( SHA256 sha = SHA256.Create() )
			{
				byte[] bytes = Encoding.UTF8.GetBytes( text );
				return BitConverter.ToString( sha.ComputeHash( bytes ) ).Replace( "-", string.Empty );
			}
		}

	}
}