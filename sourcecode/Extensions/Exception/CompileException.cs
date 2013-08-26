using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;

namespace Mysoft.Map.Extensions.Exception
{
	/// <summary>
	/// 表示编译异常
	/// </summary>
	public sealed class CompileException : System.Exception
	{
		/// <summary>
		/// 初始化 CompileException 的新实例。
		/// </summary>
		public CompileException()
		{
		}

		/// <summary>
		/// 编译异常的代码
		/// </summary>
		public string Code { get; internal set; }
		/// <summary>
		/// 编译结果
		/// </summary>
		public CompilerResults CompilerResult { get; internal set; }

	}
}
