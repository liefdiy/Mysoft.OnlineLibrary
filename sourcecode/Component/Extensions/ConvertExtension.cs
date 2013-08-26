using System;
using System.Globalization;

namespace Mysoft.Platform.Component.Extensions
{
	public static class ConvertExtension
	{
		public static string GetString(this object obj, string defaultVal = "")
		{
			return obj != null ? obj.ToString() : defaultVal;
		}

		public static int GetInt(this object obj, int defaultVal = 0)
		{
			if( obj == null )
			{
				return defaultVal;
			}
			else
			{
				Int32.TryParse(obj.ToString(), out defaultVal);
			}
			return defaultVal;
		}

		public static DateTime GetDateTime(this object obj, string defaultVal = "1900-01-01")
		{
			DateTime val = new DateTime(1900, 1, 1);
			DateTime.TryParseExact(defaultVal, "yyyy-MM-dd", null, DateTimeStyles.None, out val);

			if(obj == null)
			{
				return val;
			}
			else
			{
				try
				{
					return Convert.ToDateTime(obj);
				}
				catch
				{
					return val;
				}
			}
		}

		public static decimal GetDecimal(this object obj, decimal defaultVal = 0)
		{
			if( obj == null )
			{
				return defaultVal;
			}
			else
			{
				decimal.TryParse(obj.ToString(), out defaultVal);
			}
			return defaultVal;
		}
	}
}