using System.Collections.Generic;

namespace Mysoft.Platform.Component.Config
{
	internal class AppConfiguration
	{
		public static string AuthorizedFailedRedirectUrl
		{
			get { return "~/Error/Index"; }
		}

		public static string LoginRedirectUrl
		{
			get { return "~/Login.aspx"; }
		}

		public static List<string> AdminGroup
		{
			get { return new List<string>(); }
		}
	}
}