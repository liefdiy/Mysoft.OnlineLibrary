using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.DAL
{
	internal static class ConvertExt
	{
		internal static object Convert(this object value, Type targetType)
		{
			if( value == null )
				return null;

			if( targetType == typeof(string) ) 
				return value.ToString();

			Type type = Nullable.GetUnderlyingType(targetType) ?? targetType;
			if( value.GetType() == type ) {
				return value;
			}
			
			if( type == typeof(Guid) && value.GetType() == typeof(string) ) {
				return new Guid(value.ToString());
			}
			return System.Convert.ChangeType(value, type);
		}

		public static object ConvertToTimeStamp(this object value, Type targetType)
		{
			if( value == null )
				return null;

			Type type = Nullable.GetUnderlyingType(targetType) ?? targetType;
			if( value.GetType() == type ) {
				return value;
			}

			//处理时间戳声明为long的场景
			if( value.GetType() == typeof(byte[]) && type == typeof(long) ) {
				byte[] bytes = (byte[])value;
				return bytes.TimeStampToInt64();
			}

			//处理时间戳声明为long的场景
			if( value.GetType() == typeof(long) && type == typeof(byte[]) ) {
				long tmp = (long)value;
				return tmp.Int64ToTimeStamp();
			}
			return System.Convert.ChangeType(value, type);
		}
	}


}
