using System.Security.Principal;
using System.Web.Mvc;
using Mysoft.Platform.Component.Config;

namespace Mysoft.Platform.Component.Attributes
{
	public class AuthAttribute : FilterAttribute, IAuthorizationFilter
	{
		private bool _onlyAdmin;
		public AuthAttribute(bool onlyAdmin)
		{
			_onlyAdmin = onlyAdmin;
		}

		#region IAuthorizationFilter 成员

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
			
			if(!(user != null && user.IsAuthenticated))
			{
				filterContext.Result = new RedirectResult(AppConfiguration.LoginRedirectUrl);
			}
			else
			{
				if(!AppConfiguration.AdminGroup.Contains(user.Name.ToLowerInvariant()))
				{
					filterContext.Result = new RedirectResult(AppConfiguration.AuthorizedFailedRedirectUrl);
				}
			}

			//if (filterContext.HttpContext.Session != null)
			//{
			//    if (filterContext.HttpContext.Session["LoginTicket"] != null)
			//    {
			//        return;
			//    }
			//}
			//filterContext.Result = new RedirectResult(AppConfiguration.AuthorizedFailedRedirectUrl);
		}

		#endregion
	}
}