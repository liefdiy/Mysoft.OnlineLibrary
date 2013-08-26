using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// 事件管理类,提供事件的订阅
	/// </summary>
	/// <remarks>
	///	<list type="bullet">
	/// <item><description>注意:由于本类提供的事件都是静态事件,事件触发后,事件响应函数将直接在宿主线程中执行</description></item>
	/// <item><description>请确保事件响应函数中的代码不要抛出异常,否则将直接影响宿主SQL命令的执行行为</description></item>
	/// <item><description>请不要在事件响应函数中使用或调用ConnectionScope包装,因为在事件响应函数中通过ConnectionScope执行的SQL出错时,异常会被再次抛出,再次捕获.导致堆栈溢出</description></item>
	/// <item><description>使用事务需要单独开启事务,否则将直接参与到宿主代码的事务中</description></item>
	/// </list>
	/// </remarks>
	/// <example>
	///	<para>下面的代码演示了事件订阅的用法</para>
	///	<code>
	///	public void Event()
	///	{
	///		//订阅事件
	///		EventManager.ConnectionOpened += new EventHandler&lt;ConnectionEventArg&gt;(EventManager_ConnectionOpened);
	///		EventManager.BeforeExecute += new EventHandler&lt;CommandEventArg&gt;(EventManager_BeforeExecute);
	///		EventManager.AfterExecute += new EventHandler&lt;CommandEventArg&gt;(EventManager_AfterExecute);
	///		EventManager.OnException += new EventHandler&lt;ExceptionEventArg&gt;(EventManager_OnException);
	///	
	///		//数据库操作
	///		using( ConnectionScope scope = new ConnectionScope() ) {
	///			Guid guid = "SELECT newid()".AsCPQuery().ExecuteScalar&lt;Guid&gt;();
	///			try {
	///				"SELECT x".AsCPQuery().ExecuteScalar&lt;string&gt;();
	///			}
	///			catch( Exception ex ) {
	///				//仅仅为了验证是否能触发OnException函数
	///			}
	///		}
	///	}
	///	public void EventManager_OnException(object sender, ExceptionEventArg e)
	///	{
	///		//当发生异常时,将会触发这个事件
	///		//e.Exception属性包含了具体的内部错误
	///	}
	///	public void EventManager_AfterExecute(object sender, CommandEventArg e)
	///	{
	///		//当SQL命令执行完毕时,将会触发这个事件
	///	}
	///	public void EventManager_BeforeExecute(object sender, CommandEventArg e)
	///	{
	///		e.UserData = "Test";
	///		//当SQL命令准备执行时,将会触发这个事件
	///		//UserData在命令执行前可以传入,在命令执行完毕,发生异常时可以收到传入的这个对象.
	///	}
	///	public void EventManager_ConnectionOpened(object sender, ConnectionEventArg e)
	///	{
	///		//当连接打开后.将会触发这个事件
	///	}
	/// </code>
	/// </example>

	public static class EventManager
	{
		/// <summary>
		/// 连接打开事件
		/// </summary>
		public static event EventHandler<ConnectionEventArgs> ConnectionOpened;

		/// <summary>
		/// 命令执行之前事件
		/// </summary>
		public static event EventHandler<CommandEventArgs> BeforeExecute;

		/// <summary>
		/// 命令执行之后事件
		/// </summary>
		public static event EventHandler<CommandEventArgs> AfterExecute;

		/// <summary>
		/// 异常事件
		/// </summary>
		public static event EventHandler<ExceptionEventArgs> OnException;

		internal static void FireConnectionOpened(DbConnection conn)
		{
			EventHandler<ConnectionEventArgs> handler = ConnectionOpened;
			if( handler != null ) {
				ConnectionEventArgs arg = new ConnectionEventArgs();
				arg.Connection = conn;
				handler(null, arg);
			}
		}

		internal static object FireBeforeExecute(DbCommand cmd)
		{
			object userData = null;
			EventHandler<CommandEventArgs> handler = BeforeExecute;
			if( handler != null ) {
				CommandEventArgs arg = new CommandEventArgs();
				arg.Command = cmd;
				//arg.UserData = null;
				handler(null, arg);

				userData = arg.UserData;
			}
			return userData;
		}

		internal static void FireAfterExecute(DbCommand cmd, object data)
		{
			EventHandler<CommandEventArgs> handler = AfterExecute;
			if( handler != null ) {
				CommandEventArgs arg = new CommandEventArgs();
				arg.Command = cmd;
				arg.UserData = data;
				handler(null, arg);
			}
		}

		internal static void FireOnException(DbCommand cmd, System.Exception ex, object data)
		{
			EventHandler<ExceptionEventArgs> handler = OnException;
			if( handler != null ) {
				ExceptionEventArgs arg = new ExceptionEventArgs();
				arg.Command = cmd;
				arg.Exception = ex;
				arg.UserData = data;
				handler(null, arg);
			}
		}
	}

	/// <summary>
	/// 数据库连接事件参数
	/// 用法参见<see cref="EventManager"/>类.
	/// </summary>
	public class ConnectionEventArgs : EventArgs
	{
		/// <summary>
		/// 当前打开的数据库连接
		/// </summary>
		public DbConnection Connection { get; internal set; }
	}

	/// <summary>
	/// 执行命令事件参数
	/// 用法参见<see cref="EventManager"/>类.
	/// </summary>
	public class CommandEventArgs : EventArgs
	{
		/// <summary>
		/// 执行的命令
		/// </summary>
		public DbCommand Command { get; internal set; }

		/// <summary>
		/// 用户自定义数据
		/// </summary>
		public object UserData { get; set; }
	}

	/// <summary>
	/// 异常事件参数
	/// 用法参见<see cref="EventManager"/>类.
	/// </summary>
	public class ExceptionEventArgs : EventArgs
	{
		/// <summary>
		/// 异常时所执行的命令
		/// </summary>
		public DbCommand Command { get; internal set; }
		/// <summary>
		/// 异常事件中包含的内部异常
		/// </summary>
		public System.Exception Exception { get; internal set; }
		/// <summary>
		/// 用户自定义数据
		/// </summary>
		public object UserData { get; internal set; }
	}
}
