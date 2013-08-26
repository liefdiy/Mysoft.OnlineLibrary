using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// 表示一个存储过程的输出参数
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>由于匿名对象不支持Attribute,所以存储过程输出参数通过本类型进行标记</description></item>
	/// </list>
	/// </remarks>
	/// <example>
	/// <para>下面的代码演示了SPOut的使用方法</para>
	/// <code>
	/// //通过匿名类型,声明存储过程参数
	/// var product = new {
	/// 	Remark = "备注",
	/// 	ProductID = (SPOut)0		//存储过程输出参数
	/// };
	/// 
	/// //存储过程调用
	/// StoreProcedure sp = new StoreProcedure("InsertProduct", product);
	/// sp.ExecuteNonQuery();
	/// 
	/// //获取输出值
	/// int productID = (int)sp.Command.Parameters["@ProductID"].Value;
	/// </code>
	/// </example>
	public sealed class SPOut
	{
		/// <summary>
		/// 创建SPOut类实例
		/// </summary>
		public SPOut()
		{
			Size = -1;
		}
		/// <summary>
		/// 参数值
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// 参数长度
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// 创建一个SPOut对象
		/// </summary>
		/// <param name="o">对象值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static SPOut Create(object o)
		{
			SPOut parameter = new SPOut();
			parameter.Value = o;
			return parameter;
		}

		/// <summary>
		/// DBNull到SPOut的显式转换
		/// </summary>
		/// <param name="value">DBNull值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(DBNull value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// bool到SPOut的显式转换
		/// </summary>
		/// <param name="value">bool值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(bool value)
		{
			return SPOut.Create(value);
		}

		///// <summary>
		///// char到SPOut的显式转换
		///// </summary>
		///// <param name="value">char值</param>
		///// <returns>转换后的SPOut对象</returns>
		//public static explicit operator SPOut(char value)
		//{
		//    return SPOut.Create(value);
		//}

		///// <summary>
		///// sbyte到SPOut的显式转换
		///// </summary>
		///// <param name="value">sbyte值</param>
		///// <returns>转换后的SPOut对象</returns>
		//public static explicit operator SPOut(sbyte value)
		//{
		//    return SPOut.Create(value);
		//}

		/// <summary>
		/// byte到SPOut的显式转换
		/// </summary>
		/// <param name="value">byte值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(byte value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// int到SPOut的显式转换
		/// </summary>
		/// <param name="value">int值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(int value)
		{
			return SPOut.Create(value);
		}

		///// <summary>
		///// uint到SPOut的显式转换
		///// </summary>
		///// <param name="value">uint值</param>
		///// <returns>转换后的SPOut对象</returns>
		//public static explicit operator SPOut(uint value)
		//{
		//    return SPOut.Create(value);
		//}

		/// <summary>
		/// long到SPOut的显式转换
		/// </summary>
		/// <param name="value">long值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(long value)
		{
			return SPOut.Create(value);
		}

		///// <summary>
		///// ulong到SPOut的显式转换
		///// </summary>
		///// <param name="value">ulong值</param>
		///// <returns>转换后的SPOut对象</returns>
		//public static explicit operator SPOut(ulong value)
		//{
		//    return SPOut.Create(value);
		//}

		/// <summary>
		/// short到SPOut的显式转换
		/// </summary>
		/// <param name="value">short值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(short value)
		{
			return SPOut.Create(value);
		}

		///// <summary>
		///// ushort到SPOut的显式转换
		///// </summary>
		///// <param name="value">ushort值</param>
		///// <returns>转换后的SPOut对象</returns>
		//public static explicit operator SPOut(ushort value)
		//{
		//    return SPOut.Create(value);
		//}

		/// <summary>
		/// float到SPOut的显式转换
		/// </summary>
		/// <param name="value">float值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(float value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// double到SPOut的显式转换
		/// </summary>
		/// <param name="value">double值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(double value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// decimal到SPOut的显式转换
		/// </summary>
		/// <param name="value">decimal值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(decimal value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// Guid到SPOut的显式转换
		/// </summary>
		/// <param name="value">Guid值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(Guid value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// string到SPOut的显式转换
		/// </summary>
		/// <param name="value">string值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(string value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// DateTime到SPOut的显式转换
		/// </summary>
		/// <param name="value">DateTime值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(DateTime value)
		{
			return SPOut.Create(value);
		}

		/// <summary>
		/// byte[]到SPOut的显式转换
		/// </summary>
		/// <param name="value">byte[]值</param>
		/// <returns>转换后的SPOut对象</returns>
		public static explicit operator SPOut(byte[] value)
		{
			return SPOut.Create(value);
		}
	}
}
