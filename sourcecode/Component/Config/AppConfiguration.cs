using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Platform.Component.Config
{
	internal class AppConfiguration
	{
		public static string AuthorizedFailedRedirectUrl
		{
			get { return "~/Error.aspx"; }
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
