using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Mysoft.Map.Extensions.DAL;
using System.Web;
using System.Data.Common;
using System.Threading;

namespace Mysoft.Map.Extensions
{
	internal static class TableTrace
	{
		private static string[] s_traceTables;
		private static int s_AttachCount = 0;

		public static void InitTrace()
		{
			int attachCount = Interlocked.CompareExchange(ref s_AttachCount, 1, 0);

			//事件只订阅一次,不订阅多次
			if( attachCount == 0 ) {
				string traceTable = ConfigurationManager.AppSettings["TraceTable"];
				if( string.IsNullOrEmpty(traceTable) == false ) {
					using( ConnectionScope scope = new ConnectionScope() ) {
						object o = CPQuery.From("SELECT OBJECT_ID(N'mySQLLog')", null).ExecuteScalar<object>();
						if( o != null ) {
							s_traceTables = traceTable.Split(',').ToArray<string>();
							AttachEvent();
						}
					}
				}

			}
		}

		private static void AttachEvent()
		{
			EventManager.AfterExecute += new EventHandler<CommandEventArgs>(EventManager_AfterExecute);
			EventManager.OnException += new EventHandler<ExceptionEventArgs>(EventManager_OnException);
		}

		private static void EventManager_OnException(object sender, ExceptionEventArgs e)
		{
			try {
				for( int i = 0; i < s_traceTables.Length; i++ ) {
					string tableName = s_traceTables[i];
					if( e.Command.CommandText.IndexOf(tableName, StringComparison.OrdinalIgnoreCase) != -1 ) {
						WriteSqlLog(e.Command, e.Exception);
						return;
					}
				}
			}
			catch { 
				//内部写日志产生异常不对外抛出,以免影响外层业务逻辑
			}
			
		}

		private static void EventManager_AfterExecute(object sender, CommandEventArgs e)
		{
			try {
				for( int i = 0; i < s_traceTables.Length; i++ ) {
					string tableName = s_traceTables[i];
					if( e.Command.CommandText.IndexOf(tableName, StringComparison.OrdinalIgnoreCase) != -1 ) {
						WriteSqlLog(e.Command);
						return;
					}
				}
			}
			catch {
				//内部写日志产生异常不对外抛出,以免影响外层业务逻辑
			}
		}

		private static void WriteSqlLog(DbCommand cmd)
		{
			WriteSqlLog(cmd, null);
		}

		private static void WriteSqlLog(DbCommand cmd, System.Exception ex)
		{
			StringBuilder sb = new StringBuilder(1024);
			sb.Append("SQL:");
			sb.Append(cmd.CommandText);
			sb.Append(",参数:");
			foreach( DbParameter p in cmd.Parameters ) {
				string value = "null";
				if( p.Value != DBNull.Value && p.Value != null ) {
					value = p.Value.ToString();
				}
				sb.AppendFormat("{0}:{1},", p.ParameterName, value);
			}

			string message = ex == null ? "" : ex.ToString();
			string userName = "", clientIp = "", pageUrl = "";

			HttpContext context = HttpContext.Current;
			if( context != null && context.Session != null) {
				userName = context.Session["UserName"].ToString();
				clientIp = context.Request.UserHostAddress;
				pageUrl = context.Request.Path;
			}

			using( ConnectionScope scope = new ConnectionScope() ) {
				SqlParameter[] parameters = new SqlParameter[]{
						new SqlParameter("@LogGUID", Guid.NewGuid()),
						new SqlParameter("@ExeUser", userName),
						new SqlParameter("@ExeIP", clientIp),
						new SqlParameter("@ExePage", pageUrl),
						new SqlParameter("@ExeSQL", sb.ToString()),
						new SqlParameter("@ErrorMessage", message)
					};

				parameters[0].SqlDbType = SqlDbType.UniqueIdentifier;
				parameters[1].SqlDbType = SqlDbType.VarChar;
				parameters[2].SqlDbType = SqlDbType.VarChar;
				parameters[3].SqlDbType = SqlDbType.VarChar;
				parameters[4].SqlDbType = SqlDbType.Text;
				parameters[5].SqlDbType = SqlDbType.Text;

				parameters[1].Size = 50;
				parameters[2].Size = 40;
				parameters[3].Size = 100;


				CPQuery query = CPQuery.From(@"INSERT INTO mySQLLog (LogGUID, ExeDateTime, ExeUser, ExeIP, ExePage, ExeSQL, ErrorMessage) 
					VALUES(@LogGUID, GETDATE(), @ExeUser, @ExeIP, @ExePage, @ExeSQL, @ErrorMessage)", parameters);
				query.ExecuteNonQuery();
			}
		}
	}
}
