using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.Exception
{
	/// <summary>
	/// 表示代码生成异常
	/// </summary>
	public sealed class BuildException : System.Exception
	{
		/// <summary>
		/// 初始化 BuildException 的新实例。
		/// </summary>
		public BuildException()
		{

		}

		/// <summary>
		/// 包含多个编译异常或其他异常实例
		/// </summary>
		public List<System.Exception> BuildExceptions { get; internal set; }
	}
}
