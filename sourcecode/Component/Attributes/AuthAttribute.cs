using System.Security.Principal;
using System.Web.Mvc;
using Mysoft.Platform.Component.Config;

namespace Mysoft.Platform.Component.Attributes
{
	public enum UserGroup
	{
		User,
		Admin
	}

	public class AuthAttribute : FilterAttribute, IAuthorizationFilter
	{
		public AuthAttribute(UserGroup allowGroup = UserGroup.User)
		{
			AllowGroup = allowGroup;
		}

		public UserGroup AllowGroup { private get; set; }

		#region IAuthorizationFilter 成员

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
			
			if(user != null && user.IsAuthenticated)
			{
				//身份已验证，判断是否admin用户
				if( !AppConfiguration.AdminGroup.Contains(user.Name.ToLowerInvariant()) && AllowGroup == UserGroup.Admin)
				{
					filterContext.Result = new RedirectResult(string.Format("{0}?returnUrl={1}", AppConfiguration.AuthorizedFailedRedirectUrl, filterContext.HttpContext.Request.Path));
				}
			}
			else
			{
				filterContext.Result = new RedirectResult(AppConfiguration.LoginRedirectUrl);				
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