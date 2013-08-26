using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.Workflow
{
	/// <summary>
	/// 绑定事件参数
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description>BusinessTypeManager类的Bind(),BindGroup()方法将会触发绑定事件</description></item>
	/// <item><description>绑定事件将在每一行数据被绑定时触发,而不是在绑定绑定某一个循环域时触发</description></item>
	/// <item><description>不会为没有数据绑定的Domain触发DomainBinding事件,</description></item>
	/// <item><description>当数据库的值为null时,将不会触发DomainBinding事件,因为null值不绑定</description></item>
	/// </list>
	/// </remarks>
	/// <example>
	/// <para>下面的代码演示了BindEventArgs类的用法</para>
	/// <code>
	/// <![CDATA[
	/// 
	/// //订阅事件
	/// public void Main(){
	///		BusinessTypeManager btm = BusinessTypeManager.FromFile("Demo1_HTML.xml");
	///		btm.OnDomainBinding += new EventHandler<BindEventArgs>(btm_DomainBinding);
	///		btm.Bind();
	///	}
	///	
	/// //响应函数
	/// private void btm_DomainBinding(object sender, BindEventArgs e)
	///	{
	///		if( e.Domain.Name == "经办人" ) {
	///			e.Domain.Value = e.Value.ToString() + "-" + DateTime.Now.ToString();
	///		}
	///	}
	/// ]]>
	/// </code>
	/// </example>
	public class BindEventArgs : EventArgs
	{
		/// <summary>
		/// 绑定的数据类型
		/// </summary>
		public Type DataType { get; set; }

		/// <summary>
		/// 绑定的循环域组
		/// <list type="bullet">
		/// <item><description>在调用Bind()函数时,由于没有涉及到循环域,所以本属性将为null</description></item>
		/// </list>
		/// </summary>
		public Group Group { get; set; }

		/// <summary>
		/// 绑定的业务域
		/// </summary>
		public Domain Domain { get; set; }

		/// <summary>
		/// 被绑定的值
		/// </summary>
		public object Value { get; set; }
	}
}
