using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using Mysoft.Map.Extensions.Exception;


namespace Mysoft.Map.Extensions.DAL
{
	internal class ConnectionManager : IDisposable
	{
		private SqlConnection _connection;
		private SqlTransaction _transcation;

		private string _connectionString;
		private bool _enableTranscation = false;
		private Stack<TransactionMode> _transactionModes = new Stack<TransactionMode>();

		public ConnectionManager(string connectionString)
		{
			if( string.IsNullOrEmpty(connectionString) )
				throw new ArgumentNullException("connectionString");

			_connectionString = connectionString;
		}


		public SqlConnection Connection { get { return _connection; } }

		public T ExecuteCommand<T>(SqlCommand command, Func<SqlCommand, T> func)
		{
			if( command == null )
				throw new ArgumentNullException("command");

			// 打开连接，并根据需要开启事务
			if( _connection == null ) {
				_connection = new SqlConnection(_connectionString);

				_connection.Open();

				EventManager.FireConnectionOpened(_connection);
			}

			if( _enableTranscation && _transcation == null ) {
				_transcation = _connection.BeginTransaction();
			}

			// 设置命令的连接以及事务对象
			command.Connection = _connection;

			if( _transcation != null )
				command.Transaction = _transcation;

			object userData = EventManager.FireBeforeExecute(command);
			try {
				T result = func(command);

				EventManager.FireAfterExecute(command, userData);

				return result;
			}
			catch( System.Exception ex ) {
				EventManager.FireOnException(command, ex, userData);
				throw;
			}
			finally {
				// 让命令与连接，事务断开，避免这些资源外泄。
				command.Connection = null;
				command.Transaction = null;
			}
		}

		public SqlBulkCopy CreateSqlBulkCopy(SqlBulkCopyOptions copyOptions)
		{
			if( _connection == null ) {
				_connection = new SqlConnection(_connectionString);
				_connection.Open();

				EventManager.FireConnectionOpened(_connection);
			}

			if( _enableTranscation && _transcation == null ) {
				_transcation = _connection.BeginTransaction();
			}

			return new SqlBulkCopy(_connection, copyOptions, _transcation);

		}


		internal void PushTransactionMode(TransactionMode mode)
		{
			_transactionModes.Push(mode);
			if( mode == TransactionMode.Required && _enableTranscation == false) {
				_enableTranscation = true;
			}
		}

		internal bool PopTransactionMode()
		{
			TransactionMode mode = _transactionModes.Pop();

			if( _enableTranscation && mode == TransactionMode.Required) {

				//父级是否包含需要开启事务的场景
				bool required = _transactionModes.Contains(TransactionMode.Required);

				//父级不包含开启事务的场景,也就是最外层才进行销毁
				if( required == false ) {
					_enableTranscation = false;
					if( _transcation != null ) {
						_transcation.Dispose();
						_transcation = null;
					}
				}
			}

			//是否到达栈底.到达栈底后不能继续出栈,外部需要调用本类的Dispose行为
			return _transactionModes.Count != 0;
		}

		public void Commit()
		{
			//取出栈顶元素进行判断
			TransactionMode mode = _transactionModes.Peek();
			
			//如果启用了事务,且事务段内不执行任何代码,直接Commit().这种场景应该是允许的.
			//对于内部实现,就相当于连接对象都没有创建,所以此处直接返回
			if( mode == TransactionMode.Required && _connection == null )
				return;	

			if( _transcation == null )
				throw new InvalidOperationException("当前的作用域不支持事务操作。");

			if( mode != TransactionMode.Required ) 
				throw new InvalidOperationException("未在构造函数中指定TransactionMode.Required参数,不能调用Commit方法");


			//取出当前元素才能查找父级.
			mode = _transactionModes.Pop();

			if( _enableTranscation && mode == TransactionMode.Required) {

				//父级是否包含需要开启事务的场景
				bool required = _transactionModes.Contains(TransactionMode.Required);

				if( required == false ) {
					_transcation.Commit();
				}
			}

			//处理完毕后压将当前事务模式压回栈内
			_transactionModes.Push(mode);
		}
		

		public void Rollback(string message)
		{
			if( _transcation == null )
				throw new InvalidOperationException("当前的作用域不支持事务操作。");

			throw new RollbackException(message);
		}


		public void Dispose()
		{

			_transactionModes.Clear();

			if( _connection != null ) {
				_connection.Dispose();
				_connection = null;
			}

			if( _transcation != null ) {
				_transcation.Dispose();
				_transcation = null;
			}
		}
	}
}
