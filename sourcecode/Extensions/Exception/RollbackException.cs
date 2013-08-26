using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.Exception
{
	/// <summary>
	/// 表示一个回滚事务的异常
	/// </summary>
	public sealed class RollbackException : System.Exception
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常信息</param>
		public RollbackException(string message) : base(message)
		{
		}
	}
}
