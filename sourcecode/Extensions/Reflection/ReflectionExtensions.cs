using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mysoft.Map.Extensions
{
	internal static class ReflectionExtensions
	{
		internal static T GetMyAttribute<T>(this MemberInfo m, bool inherit) where T : Attribute
		{
			T[] array = m.GetCustomAttributes(typeof(T), inherit) as T[];

			if( array.Length == 1 )
				return array[0];

			if( array.Length > 1 )
				throw new InvalidProgramException(string.Format("方法 {0} 不能同时指定多次 [{1}]。", m.Name, typeof(T)));

			return default(T);
		}

		internal static T GetMyAttribute<T>(this MemberInfo m) where T : Attribute
		{
			return GetMyAttribute<T>(m, false);
		}


		internal static T[] GetMyAttributes<T>(this MemberInfo m, bool inherit) where T : Attribute
		{
			return m.GetCustomAttributes(typeof(T), inherit) as T[];
		}

		internal static T[] GetMyAttributes<T>(this MemberInfo m) where T : Attribute
		{
			return m.GetCustomAttributes(typeof(T), false) as T[];
		}


		internal static bool IsNullableType(this Type nullableType)
		{
			return ((nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition)
				&& (nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)));
		}

		internal static bool IsSupportBinSerializable(this Type testType)
		{
			return testType.GetMyAttribute<SerializableAttribute>() != null;
		}
	}
}
