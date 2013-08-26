using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Data.SqlClient;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;

using Mysoft.Map.Extensions.Exception;
using Mysoft.Map.Extensions.DAL;

namespace Mysoft.Map.Extensions.CodeDom
{
	internal sealed class TypesAndReferences
	{
		public List<Type> EntityTypes;
		public string[] ReferencedAssemblies;
	}


	internal sealed class BuildManager
	{
		private static readonly bool IsAspnetApp = string.IsNullOrEmpty(System.Web.HttpRuntime.AppDomainAppId) == false;

		public static string BinDirectory
		{
			get
			{
				if( IsAspnetApp )
					return System.Web.HttpRuntime.BinDirectory;

				else
					return AppDomain.CurrentDomain.BaseDirectory;
			}
		}
	
		public static void StartAutoCompile()
		{
			// 1. 查找所有的数据实体类型
			TypesAndReferences findResult = FindEntityTypes();

			// 2. 批量编译数据实体
			BatchCompile(findResult);
		}

		

		private static TypesAndReferences FindEntityTypes()
		{
			string runtimeFolder = BinDirectory;

			List<Assembly> assemblies = new List<Assembly>();

			// 只搜索所有以【.Entity.dll】结尾的程序集
			string searchPattern = System.Configuration.ConfigurationManager.AppSettings["DAL:EntryProjectNameSearchPattern"];
			if( string.IsNullOrEmpty(searchPattern) )
				searchPattern = "*.Entity.dll";

			foreach( string file in Directory.GetFiles(runtimeFolder, searchPattern, SearchOption.TopDirectoryOnly) ) 
				assemblies.Add(Assembly.LoadFile(file));


			List<string> assemblyList = new List<string>();
			List<Type> types = new List<Type>();

			foreach( Assembly assembly in assemblies ) {
				// 保存程序集的引用
				assemblyList.Add(assembly.Location);

				foreach( Type type in assembly.GetExportedTypes() ) {
					// 查找所有 BaseEntity 的继承类，并且【排除嵌套类型】。
					if( type.IsSubclassOf(typeof(BaseEntity)) && type.IsNested == false ) {
						// 数据实体类型要求提供【无参构造函数】。
						if( type.GetConstructor(Type.EmptyTypes) == null )
							throw new InvalidProgramException(string.Format("类型 {0} 没有定义无参的构造函数 。", type));
						
						// 保留符合所有条件的数据实体类型，它将被编译
						types.Add(type);
					}
				}
			}

			// 添加必要的其它引用程序集，供编译使用
			assemblyList.Add("System.dll");
			assemblyList.Add("System.Xml.dll");
			assemblyList.Add("System.Web.dll");
			assemblyList.Add("System.Data.dll");
			assemblyList.Add(typeof(Mysoft.Map.Extensions.CodeDom.BuildManager).Assembly.Location);

			// 构造返回结果
			TypesAndReferences result = new TypesAndReferences();
			result.EntityTypes = types;
			result.ReferencedAssemblies = assemblyList.ToArray();
			return result;
		}

		private static BuildException s_buildException 
			= new BuildException() { BuildExceptions = new List<System.Exception>() };

		private static readonly object s_mutex = new object();

		public static void AddException(System.Exception ex)
		{
			lock( s_mutex ) {
				s_buildException.BuildExceptions.Add(ex);
			}
		}

		private static readonly int Number200 = 200;
		private static readonly int MaxThreadCount = 5;


		private static void BatchCompile(TypesAndReferences findResult)
		{
			//findResult.EntityTypes = findResult.EntityTypes.Take(200).ToList();

			int typeCount = findResult.EntityTypes.Count;

			//如果小于或等于容器大小.则无需开启线程编译
			if( typeCount <= Number200 ) {
				CodeDomCompiler compiler = new CodeDomCompiler();
				compiler.DoWork(findResult);
			}
			else {
				// 计算编译线程数量
				int threadCount = typeCount % Number200 == 0 ? typeCount / Number200 : typeCount / Number200 + 1;
				if( threadCount > MaxThreadCount )
					threadCount = MaxThreadCount;


				int threadPqgeSize = (typeCount / threadCount) + 1;
				int typeSum = 0;

				// 为每个线程准备调用参数
				TypesAndReferences[] parameters = new TypesAndReferences[threadCount];
				for( int i = 0; i < threadCount; i++ ) {
					parameters[i] = new TypesAndReferences();
					parameters[i].ReferencedAssemblies = (from s in findResult.ReferencedAssemblies select s).ToArray();
					parameters[i].EntityTypes = findResult.EntityTypes.Skip(typeSum).Take(threadPqgeSize).ToList();
					typeSum += parameters[i].EntityTypes.Count;
				}

				// 创建编译线程
				List<Thread> threads = new List<Thread>(threadCount);
				for( int i = 1; i < threadCount; i++ ) {
					CodeDomCompiler compiler = new CodeDomCompiler();
					Thread thread = new Thread(compiler.DoWork);
					thread.IsBackground = true;
					thread.Name = "CompilerThread #" + i.ToString();
					threads.Add(thread);
					thread.Start(parameters[i]);
				}

				// 重用当前线程：为当前线程指派编译任务。
				CodeDomCompiler compiler2 = new CodeDomCompiler();
				compiler2.DoWork(parameters[0]);


				// 等待所有的编译线程执行线束。
				foreach( Thread thread in threads ) 
					thread.Join();
				

				// 如果在编译期间发生了异常，则抛出所有收集到的异常。
				if( s_buildException.BuildExceptions.Count > 0 ) 
					throw s_buildException;

			}
		}

		

	}
}
