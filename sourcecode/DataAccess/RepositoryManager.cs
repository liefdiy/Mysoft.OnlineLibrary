using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Mysoft.Platform.DataAccess
{
	public static class RepositoryManager
	{
		public static void Init()
		{
			string connStr = ConfigurationManager.AppSettings["Default"];
			if( string.IsNullOrEmpty(connStr) )
			{
				connStr = @"data source=.;Integrated Security=SSPI;Database=OnlineLibrary";
			}
			try
			{
				Mysoft.Map.Extensions.Initializer.UnSafeInit(connStr);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Connection string is incorrect, check 'Default' of connectionStrings section in web.config.");
			}
		}
	}
}
