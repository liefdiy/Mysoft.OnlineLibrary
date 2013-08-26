using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mysoft.Map.Extensions.Exception
{
	/// <summary>
	/// 开放式并发冲突发生时引发的异常。
	/// </summary>
	public sealed class OptimisticConcurrencyException : System.Exception
	{
		/// <summary>
		/// 初始化 OptimisticConcurrencyException 的新实例。
		/// </summary>
		public OptimisticConcurrencyException()
		{
		}

		/// <summary>
		/// 使用指定的错误消息初始化 OptimisticConcurrencyException 的新实例。
		/// </summary>
		/// <param name="message">错误消息</param>
		public OptimisticConcurrencyException(string message)
			: base(message)
		{
		}

		private OptimisticConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}




	}
}
