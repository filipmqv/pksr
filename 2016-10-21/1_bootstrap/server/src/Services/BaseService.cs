using System;
using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace Server.Services
{
	public class BaseService : Service
	{
		protected int GetcurrentAuthUserId()
		{
			var session = this.GetSession ();
			if (session.IsAuthenticated == false)
				throw new HttpError (HttpStatusCode.Unauthorized, "not authorized session");

			int id;
			if (!int.TryParse (session.UserAuthId, out id))
				throw new Exception ("Unexpected UserAuthId, cannot Parse as int");

			return id;

			
		}
			
	}
}

