using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions
{
	/// <summary>
	/// 表示数据库时间戳类型转换的扩展方法类
	/// </summary>
	public static class TimeStampExtensions
	{
		private static byte[] ReverseArray(byte[] oldArray)
		{
			byte[] newArray = new byte[oldArray.Length];
			int index = oldArray.Length - 1;
			for( int i = 0; i < oldArray.Length; i++ ) {
				newArray[index] = oldArray[i];
				index--;
			}
			return newArray;
		}

		/// <summary>
		/// 将用字节表示的时间戳数据转换为用长整型表示.
		/// </summary>
		/// <param name="array">字节数组</param>
		/// <returns>长整型时间戳</returns>
		public static long TimeStampToInt64(this byte[] array){

			if (array == null)
				throw new ArgumentException("array");

			if( array.Length != 8 )
				throw new ArgumentOutOfRangeException("array", "byte[]数组表示的时间戳，长度应该为8。");

			return BitConverter.ToInt64(ReverseArray(array), 0);
		}

		/// <summary>
		/// 将用长整型表示的时间戳数据转换为用字节数组表示
		/// </summary>
		/// <param name="value">长整型数组</param>
		/// <returns>字节数组</returns>
		public static byte[] Int64ToTimeStamp(this long value) {

			if( value <= 0 )
				throw new ArgumentOutOfRangeException("array", "long类型表示的时间戳应该大于0。");

			return ReverseArray(BitConverter.GetBytes(value));
		}
	}
}
