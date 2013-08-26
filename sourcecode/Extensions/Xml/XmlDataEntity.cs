using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.CodeDom;

namespace Mysoft.Map.Extensions.Xml
{
	/// <summary>
	/// AppForm生成的单表xml及多表xml保存到数据库的包装
	/// </summary>
	public static class XmlDataEntity
	{
		//internal static Func<string, string, List<KeyValuePair<string, SqlCommand>>> SaveXmlFunc { get; set; }

		///// <summary>
		///// 保存主表信息
		///// </summary>
		///// <param name="xml">xml数据</param>
		///// <returns>主键</returns>
		//internal static string SaveMaster(string xml)
		//{
		//    if( string.IsNullOrEmpty(xml) )
		//        throw new ArgumentNullException("xml");

		//    string data = string.Format("<data>{0}</data>", xml);

		//    string connectionString = ConnectionScope.GetDefaultConnectionString();

		//    List<KeyValuePair<string, SqlCommand>> cmds = SaveXmlFunc(connectionString, data);

		//    if( cmds != null && cmds.Count > 0 ) {
		//        KeyValuePair<string, SqlCommand> kvp = cmds[0];

		//        using( ConnectionScope scope = new ConnectionScope() ) {
		//            scope.Current.ExecuteCommand<int>(kvp.Value, cmd => {
		//                return cmd.ExecuteNonQuery();
		//            });
		//            return kvp.Key;
		//        }
		//    }

		//    return null;
		//}


		///// <summary>
		///// 保存从表信息
		///// </summary>
		///// <param name="xml">xml数据</param>
		///// <param name="fkName">外键名称</param>
		///// <param name="fkValue">外键值</param>
		///// <returns>主键集合</returns>
		//internal static List<string> SaveDetail(string xml, string fkName, string fkValue)
		//{
		//    if( string.IsNullOrEmpty(xml) )
		//        throw new ArgumentNullException("xml");

		//    if( string.IsNullOrEmpty(fkName) )
		//        throw new ArgumentNullException("fkName");

		//    if( string.IsNullOrEmpty(fkValue) )
		//        throw new ArgumentNullException("fkValue");

		//    XmlDocument doc = new XmlDocument();
		//    doc.LoadXml(xml);
		//    foreach( XmlNode node in doc.SelectNodes(string.Format("//{0}", fkName)) ) {
		//        node.InnerXml = fkValue;
		//    }
		//    return SaveDetail(doc.InnerXml);
		//}

		///// <summary>
		///// 保存多条记录
		///// </summary>
		///// <param name="xml">xml数据</param>
		///// <returns>主键集合</returns>
		//internal static List<string> SaveDetail(string xml)
		//{
		//    if( string.IsNullOrEmpty(xml) )
		//        throw new ArgumentNullException("xml");

		//    string connectionString = ConnectionScope.GetDefaultConnectionString();

		//    List<KeyValuePair<string, SqlCommand>> cmds = SaveXmlFunc(connectionString, xml);

		//    if( cmds != null && cmds.Count > 0 ) {
		//        List<string> list = new List<string>();
		//        using( ConnectionScope scope = new ConnectionScope() ) {
		//            foreach( KeyValuePair<string, SqlCommand> kvp in cmds ) {

		//                scope.Current.ExecuteCommand<int>(kvp.Value, cmd => {
		//                    return cmd.ExecuteNonQuery();
		//                });

		//                list.Add(kvp.Key);

		//            }
		//        }
		//        return list;
		//    }

		//    return null;
		//}

		/// <summary>
		/// 将Map平台AppForm生成的xml字符串转换为实体对象
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>xml结构为2层,第1层为数据库表名,第2层为字段名</description></item>
		/// <item><description>xml结构第1层节点必须包含keyname,keyvalue属性,keyvalue属性可以为空</description></item>
		/// <item><description>如果类,属性存在Alias别名标记,则按照别名赋值,否则按照类名,属性名赋值</description></item>
		/// </list>
		/// </remarks>
		/// <example>
		/// <para>下面的代码演示了从ConvertXmlToSingle()方法的用法</para>
		/// <code>
		/// <![CDATA[
		/// using System;
		/// using System.Collections.Generic;
		/// using System.Linq;
		/// using System.Text;
		/// 
		/// //引入命名空间
		/// using Mysoft.Map.Extensions;
		/// using Mysoft.Map.Extensions.Xml;
		/// using Mysoft.Map.Extensions.DAL;
		/// namespace Demo
		/// {
		///     public class DemoBusiness
		///     {
		///			public void Demo(){
		///				string xml = @"<cb_Contract keyname="ContractGUID" keyvalue="">
		///								  <ContractName>测试合同一</ContractName>
		///								  <ContractCode>HT-001</ContractCode>
		///								   ...其他值
		///							   </cb_Contract>";
		///				CbContract contract = XmlDataEntity.ConvertXmlToSingle<CbContract>(xml);
		///				
		///				//主键为空,表示是新增
		///				if (contract.ContractGUID == Guid.Empty){
		///					contract.ContractGUID = Guid.NewGuid();
		///					//插入到数据库
		///					contract.Insert();
		///				}
		///				else{
		///					//更新到数据库
		///					contract.Update();
		///				}
		///			}
		///     }
		/// }
		/// ]]>
		/// </code>
		/// </example>
		/// <typeparam name="T">实体类型</typeparam>
		/// <param name="xml">xml字符串</param>
		/// <returns>实体对象</returns>
		public static T ConvertXmlToSingle<T>(string xml) where T : BaseEntity, new()
		{
			if( string.IsNullOrEmpty(xml) )
				throw new ArgumentNullException("xml");

			Type type = typeof(T);
			TypeDescription description = TypeDescriptionCache.GetTypeDiscription(type);
			if( description.ExecuteFunc == null )
				throw BaseEntity.GetNonStandardExecption(type);

			try {
				return description.ExecuteFunc(11, new object[] { xml }) as T;
			}
			catch( System.Exception ex ) {
				//这里不希望调用者看到代码生成器产生的代码结构,于是在这里抛出捕获到的异常
				throw ex;
			}
		}

		/// <summary>
		/// 将Map平台AppForm格式xml字符串转换为实体对象集合
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>xml结构为3层,第1层被忽略,第2层为数据库表名,第3层为字段名</description></item>
		/// <item><description>xml结构第2层节点必须包含keyname,keyvalue属性,keyvalue属性可以为空</description></item>
		/// <item><description>如果类,属性存在Alias别名标记,则按照别名赋值,否则按照类名,属性名赋值</description></item>
		/// </list>
		/// </remarks>
		/// <example>
		/// <para>下面的代码演示了从ConvertXmlToList()方法的用法</para>
		/// <code>
		/// <![CDATA[
		/// using System;
		/// using System.Collections.Generic;
		/// using System.Linq;
		/// using System.Text;
		/// 
		/// //引入命名空间
		/// using Mysoft.Map.Extensions;
		/// using Mysoft.Map.Extensions.Xml;
		/// using Mysoft.Map.Extensions.DAL;
		/// namespace Demo
		/// {
		///     public class DemoBusiness
		///     {
		///			public void Demo(){
		///				string xml = @"<UserData>
		///							     <cb_Contract keyname="ContractGUID" keyvalue="">
		///								   <ContractName>测试合同一</ContractName>
		///								   <ContractCode>HT-001</ContractCode>
		///								   ...其他值
		///							     </cb_Contract>
		///							     <cb_Contract keyname="ContractGUID" keyvalue="">
		///								   <ContractName>测试合同二</ContractName>
		///								   <ContractCode>HT-002</ContractCode>
		///								   ...其他值
		///							     </cb_Contract>
		///							   </UserData>";
		///							   
		///				List<CbContract> contracts = XmlDataEntity.ConvertXmlToList<CbContract>(xml);
		///				
		///				foreach(CbContract contract in contracts){
		///					//主键为空,表示是新增
		///					if (contract.ContractGUID == Guid.Empty){
		///						contract.ContractGUID = Guid.NewGuid();
		///						//插入到数据库
		///						contract.Insert();
		///					}
		///					else{
		///						//更新到数据库
		///						contract.Update();
		///					}
		///				}
		///			}
		///     }
		/// }
		/// ]]>
		/// </code>
		/// </example>
		/// <typeparam name="T">实体类型</typeparam>
		/// <param name="xml">xml字符串</param>
		/// <returns>实体对象集合</returns>
		public static List<T> ConvertXmlToList<T>(string xml) where T : BaseEntity, new()
		{
			if( string.IsNullOrEmpty(xml) )
				throw new ArgumentNullException("xml");

			Type type = typeof(T);
			TypeDescription description = TypeDescriptionCache.GetTypeDiscription(type);
			if( description.ExecuteFunc == null )
				throw BaseEntity.GetNonStandardExecption(type);

			try {
				return description.ExecuteFunc(12, new object[] { xml }) as List<T>;
			}
			catch( System.Exception ex ) {
				//这里不希望调用者看到代码生成器产生的代码结构,于是在这里抛出捕获到的异常
				throw ex;
			}
		}
	}
}
