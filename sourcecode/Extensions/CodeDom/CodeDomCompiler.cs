using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using Mysoft.Map.Extensions.Exception;

namespace Mysoft.Map.Extensions.CodeDom
{
	internal sealed class CodeDomCompiler
	{
		public void DoWork(object obj)
		{
			TypesAndReferences threadParam = (TypesAndReferences)obj;

			try {
				// 1. 生成代码
				string code = GeneratorCode(threadParam);

				// 2. 编译生成的代码
				Assembly assembly = CompilerCode(code, threadParam.ReferencedAssemblies);

				// 3. 创建委托，供后续代码调用
				CreateDelegate(threadParam.EntityTypes, assembly);
			}
			catch( System.Exception ex ) {
				BuildManager.AddException(ex);
			}
		}

		private string GeneratorCode(TypesAndReferences threadParam)
		{
			//测试ERP258系统,1253个实体类,5线程编译,每线程的StringBuilder长度数据如下:
			//3826393,4451978,3653788,4433206,4545317
			//根据以上数字,初始值设置为1024*1024*5
			StringBuilder sb = new StringBuilder(1024 * 1024 * 5);

			// 生成命名空间和命名空间的引用
			sb.AppendLine(CodeGenerator.GetCodeHeader());

			// 生成一个工具类
			sb.AppendLine(CodeGenerator.GetCodeUtil());

			// 为每个数据实体类型生成数据访问代码
			foreach( Type type in threadParam.EntityTypes ) {
				CodeGenerator generator = new CodeGenerator(type);

				// 数据访问代码的类型名称根据数据实体名称得到，具体生成方法请参考下面这行代码。
				string className = "CodeDom_" + type.FullName.Replace(".", "_");

				// 生成数据访问代码。
				string code = generator.GetCode(className);
				sb.AppendLine(code);
			}

			sb.Append("}");

			return sb.ToString();
		}


		private Assembly CompilerCode(string code, string[] referenceAssemblies)
		{
			CompilerParameters cp = new CompilerParameters();
			cp.GenerateExecutable = false;
			cp.GenerateInMemory = true;

			for( int i = 0; i < referenceAssemblies.Length; i++ )
				cp.ReferencedAssemblies.Add(referenceAssemblies[i]);


			CSharpCodeProvider csProvider = new CSharpCodeProvider();

			// 编译代码
			CompilerResults cr = csProvider.CompileAssemblyFromSource(cp, code);

			// 检查是否发生编译错误。
			if( cr.Errors != null && cr.Errors.HasErrors ) {
				throw new CompileException { Code = code, CompilerResult = cr };
			}

			return cr.CompiledAssembly;
		}

		private void CreateDelegate(List<Type> types, Assembly assembly)
		{
			foreach( Type type in types ) {
				// 从编译结果中查找类型名称。
				string className = "_Tool.AutoGenerateCode.CodeDom_" + type.FullName.Replace(".", "_");
				// 从编译结果中查找类型
				Type targetType = assembly.GetType(className);

				// 从找到的类型中查找供外面调用的接口方法
				MethodInfo methodExecute = targetType.GetMethod("Execute", BindingFlags.Static | BindingFlags.Public);
				// 生成调用委托
				Func<int, object[], object> executeFunc = Delegate.CreateDelegate(typeof(Func<int, object[], object>), methodExecute) as Func<int, object[], object>;

				// 保存委托。
				TypeDescription description = new TypeDescription() { ExecuteFunc = executeFunc };
				TypeDescriptionCache.SaveComplieResult(type, description);
			}
		}

	}
}
