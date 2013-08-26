using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mysoft.Map.Extensions.DAL;


// 注意：
// 在这个代码生成过程中，直接使用了三个变量名：t1, t2, t3 ，它们的含义如下：
// t1: 调用 Insert, Delete, Update 的当前对象。
// t2: 用于生成Where条件的参考对象（比较对象）
// t3: t1对象的复本，当调用 t1.TrackChange() 后会产生。



namespace Mysoft.Map.Extensions.CodeDom
{
	internal class CodeGenerator
	{
		private StringBuilder _sb;
		private Type _entityType;
		private DbMapInfo _tableInfo;
		private List<DbMapInfo> _allFields;
		private List<DbMapInfo> _keyFields;
		private DbMapInfo _identityField;
		private DbMapInfo _timeStampField;
		private DbMapInfo _seqGuidField;


		public CodeGenerator(Type entityType)
		{
			_entityType = entityType;
			_sb = new StringBuilder(1024 * 20);

			DataEntityAttribute entityAttr = _entityType.GetMyAttribute<DataEntityAttribute>();
			string tableName;
			if( entityAttr == null ) 
				tableName = _entityType.Name;
			else 
				tableName = string.IsNullOrEmpty(entityAttr.Alias) ? _entityType.Name : entityAttr.Alias;


			_tableInfo = new DbMapInfo(tableName, _entityType.Name, null, null);


			_allFields = new List<DbMapInfo>(20);
			_keyFields = new List<DbMapInfo>(3);

			DbMapInfo info = null;

			foreach( PropertyInfo property in _entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public) ) {
				string fieldName = property.Name;

				DataColumnAttribute attr = property.GetMyAttribute<DataColumnAttribute>();
				if( attr != null ) {
					if( string.IsNullOrEmpty(attr.Alias) == false )
						fieldName = attr.Alias;

					info = new DbMapInfo(fieldName, property.Name, attr, property);
					_allFields.Add(new DbMapInfo(fieldName, property.Name, attr, property));

					if( attr.Identity )
						_identityField = info;
					if( attr.TimeStamp )
						_timeStampField = info;
					if( attr.SeqGuid )
						_seqGuidField = info;

					if( attr.PrimaryKey )
						_keyFields.Add(info);
				}
				else
					_allFields.Add(new DbMapInfo(fieldName, property.Name, null, property));
			}

		}

		public static string GetCodeHeader()
		{
			return @"using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.DAL;
namespace _Tool.AutoGenerateCode {";
		}

		public string GetCode(string className)
		{
			_sb.AppendLine("public static class " + className + " {");
			_sb.AppendFormat("private static readonly {0} s_Empty = new {0}();\r\n\r\n", _entityType.FullName);

			this.Switch();

			this.UpdateSetFields();
			this.WhereByPK();
			this.WhereByTimeStamp();
			this.WhereByOriginalValue();

			//this.Insert_AllField();
			this.Insert();

			this.Delete();
			this.ConcurrencyDelete(ConcurrencyMode.TimeStamp);
			this.ConcurrencyDelete(ConcurrencyMode.OriginalValue);

			this.Update();
			this.ConcurrencyUpdate(ConcurrencyMode.TimeStamp);
			this.ConcurrencyUpdate(ConcurrencyMode.OriginalValue);

			SetEntityProperty();
			DataReaderToList();
			DataReaderToSingle();
			DataTableToList();
			XmlToEntity();
			CloneMe();

			_sb.AppendLine("}");

			return _sb.ToString();
		}

		

		private void Switch()
		{
			_sb.AppendLine("public static object Execute(int flag, object[] parameters) {");
			_sb.AppendLine("switch( flag ) {");
			_sb.AppendLine("case 1: return DataReaderToList((SqlDataReader)parameters[0]);");
			_sb.AppendLine("case 2: return DataReaderToSingle((SqlDataReader)parameters[0]);");
			_sb.AppendFormat("case 3: return Insert(({0})parameters[0]);\r\n", _entityType.FullName);
			_sb.AppendFormat("case 4: return Delete(({0})parameters[0]);\r\n", _entityType.FullName);
			_sb.AppendFormat("case 5: return ConcurrencyDelete_TimeStamp(({0})parameters[0]);\r\n", _entityType.FullName);
			_sb.AppendFormat("case 6: return ConcurrencyDelete_OriginalValue(({0})parameters[0]);\r\n", _entityType.FullName);
			_sb.AppendFormat("case 7: return Update(({0})parameters[0], ({0})parameters[1]);\r\n", _entityType.FullName);
			_sb.AppendFormat("case 8: return ConcurrencyUpdate_TimeStamp(({0})parameters[0], ({0})parameters[1], ({0})parameters[2]);\r\n", _entityType.FullName);
			_sb.AppendFormat("case 9: return ConcurrencyUpdate_OriginalValue(({0})parameters[0], ({0})parameters[1], ({0})parameters[2]);\r\n", _entityType.FullName);
			_sb.AppendLine("case 10: return DataTableToList((DataTable)parameters[0]);");
			_sb.AppendLine("case 11: return XmlToSingle(parameters[0].ToString());");
			_sb.AppendLine("case 12: return XmlToList(parameters[0].ToString());");
			_sb.AppendLine("case 13: return CloneMe(parameters[0]);");
			_sb.AppendLine("default: throw new NotImplementedException();");
			_sb.AppendLine("}}");
		}

		private void Insert()
		{
			_sb.AppendFormat("public static CPQuery Insert({0} t1) {{\r\n", _entityType.FullName);

			_sb.AppendFormat("{0} t2 = s_Empty;\r\n", _entityType.FullName);
			_sb.AppendLine("bool changed = false;");
			_sb.AppendLine("string[] zeroProperties = t1.GetZeroProperties();");
			_sb.AppendLine("List<SqlParameter> parameters = new List<SqlParameter>();");

			_sb.AppendLine("StringBuilder sqlBuilder = new StringBuilder();");
			_sb.AppendLine("StringBuilder sqlParams = new StringBuilder();");
			_sb.AppendFormat("sqlBuilder.Append(\"insert into [{0}] (\");\r\n", _tableInfo.DbName);

			foreach( DbMapInfo info in _allFields ) {
				if( info.Attr != null && (info.Attr.Identity || info.Attr.TimeStamp || info.Attr.SeqGuid) )
					continue;

				if( info.PropertyInfo.PropertyType.IsClass && info.PropertyInfo.PropertyType != typeof(string) )
					_sb.AppendFormat("if( t1.{0} != null ){{\r\n", info.NetName);		// 类似 byte[] 这些类型

				else if( info.PropertyInfo.PropertyType.IsValueType && info.PropertyInfo.PropertyType.IsNullableType() == false )
					_sb.AppendFormat("if( t1.{0} != t2.{0} || Array.IndexOf(zeroProperties, \"{0}\") >= 0){{\r\n", info.NetName);
				else
					_sb.AppendFormat("if( t1.{0} != t2.{0} ){{\r\n", info.NetName);		// 引用类型或者可空类型不可能是零值
				
				_sb.AppendLine("if( changed ) {sqlBuilder.Append(\",\"); sqlParams.Append(\",\"); }");
				_sb.AppendLine("else changed = true;");

				_sb.AppendFormat("sqlBuilder.Append(\"[{0}]\");\r\n", info.DbName);
				_sb.AppendFormat("sqlParams.Append(\"@{0}\");\r\n", info.NetName);

				_sb.AppendFormat("parameters.Add(new SqlParameter(\"@{0}\", {1}));\r\n", info.NetName, OutPropertyGetValue(info, "t1"));

				_sb.AppendLine("}");
			}

			// 没有为任何字段赋值。
			_sb.AppendLine("if( changed == false ) return null;");

			_sb.AppendLine("sqlBuilder.Append(\") values (\").Append(sqlParams.ToString()).Append(\")\");");
			_sb.AppendLine("CPQuery query = CPQuery.From(sqlBuilder.ToString(), parameters.ToArray());");
			_sb.AppendLine("return query;");
			_sb.AppendLine("}");
		}

		private void Insert_AllField()
		{
			_sb.AppendFormat("public static CPQuery Insert_AllField({0} t1) {{\r\n", _entityType.FullName);
			_sb.AppendFormat("CPQuery query = CPQuery.Create() + \"insert into [{0}] (", _tableInfo.DbName);

			int index = 1;
			foreach( DbMapInfo info in _allFields ) {
				if( info.Attr != null && (info.Attr.Identity || info.Attr.TimeStamp || info.Attr.SeqGuid) )
					continue;

				if( index++ > 1 )
					_sb.AppendFormat(",[{0}]", info.DbName);
				else
					_sb.AppendFormat("[{0}]", info.DbName);
			}

			_sb.Append(") values (\" \r\n");

			index = 1;
			foreach( DbMapInfo info in _allFields ) {
				if( info.Attr != null && (info.Attr.Identity || info.Attr.TimeStamp || info.Attr.SeqGuid) )
					continue;

				if( index++ > 1 )
					_sb.AppendFormat("+ \",\" + (new SqlParameter(\"@{0}\", {1}))\r\n",
						info.DbName, OutPropertyGetValue(info, "t1"));
				else
					_sb.AppendFormat("+ (new SqlParameter(\"@{0}\", {1}))\r\n",
							info.DbName, OutPropertyGetValue(info, "t1"));
			}

			_sb.Append("+ \")\";\r\n");
			_sb.AppendLine("return query;");
			_sb.AppendLine("}");
		}

		private string OutPropertyGetValue(DbMapInfo info, string varName)
		{
			if( info.PropertyInfo.PropertyType.IsClass )
				return string.Format("({0}.{1} ?? (object)DBNull.Value)", varName, info.NetName);
			else if( info.PropertyInfo.PropertyType.IsNullableType() )
				return string.Format("({0}.{1}.HasValue ? {0}.{1}.Value : (object)DBNull.Value)", varName, info.NetName);
			else
				return string.Format("{0}.{1}", varName, info.NetName);
		}

		private bool EnsureExistPrimaryField(string actionName)
		{
			if( _keyFields.Count == 0 ) {
				_sb.AppendFormat("throw new InvalidOperationException(" +
					"\"类型 {0} 没有定义主键字段，不能执行{1}操作。\");", _entityType.FullName, actionName);

				_sb.AppendLine("\r\n}");
				return true;
			}
			return false;
		}

		private void CheckPrimaryKeyValue(string varName)
		{
			foreach( DbMapInfo info in _keyFields )
				_sb.AppendFormat("if( {0}.{1} == s_Empty.{1} ) throw new InvalidOperationException(\"没有为主键字段赋值：{0}.{1}\");\r\n", varName, info.NetName);
		}

		private void Delete()
		{
			_sb.AppendFormat("public static CPQuery Delete({0} t1) {{\r\n", _entityType.FullName);

			if( EnsureExistPrimaryField("删除") )
				return;

			CheckPrimaryKeyValue("t1");

			_sb.AppendFormat("CPQuery query = CPQuery.Create() + \"delete from [{0}] \"; \r\n", _tableInfo.DbName);

			_sb.AppendLine("WhereByPK(query, t1);");

			_sb.AppendLine("return query;");
			_sb.AppendLine("}");
		}

		private void WhereByPK()
		{
			_sb.AppendFormat("private static void WhereByPK(CPQuery query, {0} t1) {{\r\n", _entityType.FullName);
			GenerateSimpleWhere("t1");
			_sb.AppendLine("}");
		}

		private void GenerateSimpleWhere(string varName)
		{
			_sb.AppendLine("query=query+ \" where \";");

			int index = 1;
			foreach( DbMapInfo info in _keyFields ) {
				if( index++ > 1 )
					_sb.AppendLine("query=query+ \" and \";");

				SetField(info, varName, null);
			}
		}

		private void SetField(DbMapInfo info, string varName, string paraNamePrefix)
		{
			_sb.AppendFormat("query=query+ \" [{0}] = \" + (new SqlParameter(\"@{1}{3}\", {2}));\r\n",
				info.DbName, paraNamePrefix, OutPropertyGetValue(info, varName), info.NetName);
		}

		private void UpdateSetFields()
		{
			_sb.AppendFormat("private static CPQuery UpdateSetFields({0} t1, {0} t2) {{\r\n", _entityType.FullName);
			_sb.AppendLine("bool changed = false;");
			_sb.AppendLine("string[] zeroProperties = t1.GetZeroProperties();");
			_sb.AppendFormat("CPQuery query = CPQuery.Create() + \"update [{0}] set \"; \r\n\r\n", _tableInfo.DbName);

			foreach( DbMapInfo info in _allFields ) {
				if( info.Attr != null && (info.Attr.Identity || info.Attr.TimeStamp || info.Attr.PrimaryKey) )
					continue;

				if( info.PropertyInfo.PropertyType.IsClass && info.PropertyInfo.PropertyType != typeof(string) )
					_sb.AppendFormat("if( t1.{0} != null ){{\r\n", info.NetName);		// 类似 byte[] 这些类型

				else if( info.PropertyInfo.PropertyType.IsValueType && info.PropertyInfo.PropertyType.IsNullableType() == false )
					_sb.AppendFormat("if( t1.{0} != t2.{0} || Array.IndexOf(zeroProperties, \"{0}\") >= 0){{\r\n", info.NetName);
				else
					_sb.AppendFormat("if( t1.{0} != t2.{0} ){{\r\n", info.NetName);		// 引用类型或者可空类型不可能是零值
								
				_sb.AppendLine("if( changed ) query = query + \" , \";");
				_sb.AppendLine("else changed = true;");

				SetField(info, "t1", null);

				_sb.AppendLine("}");
			}

			_sb.AppendLine("if( changed == false ) return null;\r\n");
			_sb.AppendLine("else return query;");
			_sb.AppendLine("}");
		}

		private void Update()
		{
			_sb.AppendFormat("public static CPQuery Update({0} t1, {0} t3){{\r\n", _entityType.FullName);

			if( EnsureExistPrimaryField("更新") )
				return;

			CheckPrimaryKeyValue("t1");

			_sb.AppendFormat("{0} t2 = t3 ?? s_Empty;\r\n", _entityType.FullName);

			_sb.AppendLine("CPQuery query = UpdateSetFields(t1, t2);");
			_sb.AppendLine("if( query == null ) return null;");

			_sb.AppendLine("WhereByPK(query, t1);");

			_sb.AppendLine("return query;");
			_sb.AppendLine("}");
		}

		private void ConcurrencyUpdate(ConcurrencyMode concurrencyMode)
		{
			// 注意：为了方便设计代码生成， t1 为当前对象，t2 则为参考对象。
			_sb.AppendFormat("public static CPQuery ConcurrencyUpdate_{1}({0} t1, {0} t2, {0} t3){{\r\n",
				_entityType.FullName, concurrencyMode.ToString());

			if( concurrencyMode == ConcurrencyMode.TimeStamp && IsTimeStampFieldNotExist() )
				return;

			if( EnsureExistPrimaryField("更新") )
				return;

			//CheckPrimaryKeyValue("t1");
			CheckPrimaryKeyValue("t2");

			_sb.AppendLine("CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);");
			_sb.AppendLine("if( query == null ) return null;");

			if( concurrencyMode == ConcurrencyMode.TimeStamp )
				_sb.AppendLine("WhereByTimeStamp(query, t2);");
			else
				_sb.AppendLine("WhereByOriginalValue(query, s_Empty, t2);");


			_sb.AppendLine("return query;");
			_sb.AppendLine("}");
		}


		private void ConcurrencyDelete(ConcurrencyMode concurrencyMode)
		{
			// 注意：为了方便设计代码生成， t1 为当前对象，t2 则为参考对象。
			_sb.AppendFormat("public static CPQuery ConcurrencyDelete_{1}({0} t2) {{\r\n",
				_entityType.FullName, concurrencyMode.ToString());

			if( concurrencyMode == ConcurrencyMode.TimeStamp && IsTimeStampFieldNotExist() )
				return;

			if( EnsureExistPrimaryField("删除") )
				return;

			//CheckPrimaryKeyValue("t1");
			CheckPrimaryKeyValue("t2");

			if( concurrencyMode == ConcurrencyMode.OriginalValue)
				_sb.AppendFormat("{0} t1 = s_Empty;\r\n", _entityType.FullName);

			_sb.AppendFormat("CPQuery query = CPQuery.Create() + \"delete from [{0}] \"; \r\n", _tableInfo.DbName);

			if( concurrencyMode == ConcurrencyMode.TimeStamp )
				_sb.AppendLine("WhereByTimeStamp(query, t2);");
			else
				_sb.AppendLine("WhereByOriginalValue(query, t1, t2);");

			_sb.AppendLine("return query;");
			_sb.AppendLine("}");
		}


		private bool IsTimeStampFieldNotExist()
		{
			if( _timeStampField == null ) {
				_sb.AppendFormat("throw new InvalidOperationException(" +
					"\"数据实体类型 {0} 对应的数据表没有TimeStamp字段。\");", _entityType.FullName);

				_sb.AppendLine("\r\n}");
				return true;
			}
			return false;
		}

		private void WhereByTimeStamp()
		{
			if( _timeStampField != null ) {
				_sb.AppendFormat("private static void WhereByTimeStamp(CPQuery query, {0} t2) {{\r\n", _entityType.FullName);
				GenerateSimpleWhere("t2");

				_sb.AppendLine("query=query+ \" and \";");
				if( _timeStampField.PropertyInfo.PropertyType == typeof(long) ) {
					_sb.AppendFormat("query=query+ \" [{0}] = \" + (new SqlParameter(\"@{1}{3}\", CodeUtil.LongToByte({2}.{3}) ));\r\n",
					_timeStampField.DbName, "original_", "t2", _timeStampField.NetName);
				}
				else {
					SetField(_timeStampField, "t2", "original_");
				}
				_sb.AppendLine("}");
				return;
			}
			//else
			//    throw new InvalidProgramException("不应该执行到这里。");
		}


		private void WhereByOriginalValue()
		{
			_sb.AppendFormat("private static void WhereByOriginalValue(CPQuery query, {0} t1, {0} t2) {{\r\n", _entityType.FullName);
			_sb.AppendLine("string[] zeroProperties = t2.GetZeroProperties();");
			_sb.AppendLine("query=query+ \" where \";");

			int index = 1;
			foreach( DbMapInfo info in _keyFields ) {
				if( index++ > 1 )
					_sb.AppendLine("query=query+ \" and \";");

				SetField(info, "t2", "original_");
			}

			foreach( DbMapInfo info in _allFields ) {
				if( info.Attr != null && (info.Attr.PrimaryKey || info.Attr.TimeStamp) )
					continue;

				if( info.PropertyInfo.PropertyType.IsClass && info.PropertyInfo.PropertyType != typeof(string) )
					continue;			// 类似 byte[] 这些类型

				if( info.PropertyInfo.PropertyType.IsValueType && info.PropertyInfo.PropertyType.IsNullableType() == false )
					_sb.AppendFormat("if( t1.{0} != t2.{0} || Array.IndexOf(zeroProperties, \"{0}\") >= 0){{\r\n", info.NetName);
				else
					_sb.AppendFormat("if( t1.{0} != t2.{0} ){{\r\n", info.NetName);		// 引用类型不可能是零值
				
				if( index++ > 1 )
					_sb.AppendLine("query=query+ \" and \";");

				if( info.Attr != null && info.Attr.IsNullable )
					_sb.AppendFormat("query=query+ \" ([{0}] = \" + (new SqlParameter(\"@{1}{3}\", {2})) + \" or @{1}{3} is null and [{0}] is null)\";\r\n",
						info.DbName, "original_", OutPropertyGetValue(info, "t2"), info.NetName);
				else
					_sb.AppendFormat("query=query+ \" ([{0}] = \" + (new SqlParameter(\"@{1}{3}\", {2})) + \")\";\r\n",
						info.DbName, "original_", OutPropertyGetValue(info, "t2"), info.NetName);

				_sb.AppendLine("}");
			}
			_sb.AppendLine("}");
		}

		public static string GetCodeUtil()
		{
			return @"
public class CodeUtil {
public static string[] GetColumnNames(SqlDataReader reader) {
	int count = reader.FieldCount;
	string[] names = new string[count];
	for( int i = 0; i < count; i++ ) {
		names[i] = reader.GetName(i).ToLower();
	}
	return names;
}
public static string[] GetColumnNames(DataTable table){
	int count = table.Columns.Count;
	string[] names = new string[count];
	for( int i = 0; i < count; i++ ) {
		names[i] = table.Columns[i].ColumnName.ToLower();
	}
	return names;
}
public static byte[] ReverseArray(byte[] oldArray){
	byte[] newArray = new byte[oldArray.Length];
	int index = oldArray.Length - 1;
	for( int i = 0; i < oldArray.Length; i++) {
		newArray[index] = oldArray[i];
		index--;
	}
	return newArray;
}
public static long ByteToLong(byte[] value){
	return BitConverter.ToInt64(ReverseArray(value), 0);
}
public static byte[] LongToByte(long value){
	return ReverseArray(BitConverter.GetBytes(value));
}}";
		}

		private void SetEntityProperty(){

			_sb.AppendFormat("private static void SetEntityProperty({0} obj, string columnName, object value){{", _entityType.FullName);
			_sb.Append("if (value == DBNull.Value || value == null){\r\n return; \r\n}\r\n");
			_sb.Append("switch(columnName){\r\n");
			foreach( DbMapInfo info in _allFields ) {
				_sb.AppendFormat("case \"{0}\":\r\n", info.DbName.ToLower());
				_sb.Append(GetDataEntitySetCode(info));
				_sb.Append(" break;\r\n");
			}
			_sb.Append("}}");

		}

		private void DataReaderToList()
		{
			_sb.Append("public static object DataReaderToList(SqlDataReader dr){\r\n");
			_sb.AppendFormat("List<{0}> list = new List<{0}>();\r\n", _entityType.FullName);
			_sb.Append("string[] columnNames = CodeUtil.GetColumnNames(dr);\r\n");
			_sb.Append("while(dr.Read()){\r\n");
			_sb.AppendFormat("{0} obj = new {0}();\r\n", _entityType.FullName);
			_sb.Append("for(int i = 0; i < columnNames.Length; i++){\r\n");
			_sb.Append("SetEntityProperty(obj, columnNames[i], dr.GetValue(i));");
			_sb.Append("}\r\n");
			_sb.Append("list.Add(obj);\r\n");
			_sb.Append("}\r\n");
			_sb.Append("return list;\r\n");
			_sb.Append("}\r\n");
		}

		private void DataTableToList()
		{
			_sb.Append("public static object DataTableToList(DataTable table){\r\n");
			_sb.AppendFormat("List<{0}> list = new List<{0}>(table.Rows.Count);\r\n", _entityType.FullName);
			_sb.Append("string[] columnNames = CodeUtil.GetColumnNames(table);\r\n");
			_sb.Append("foreach(DataRow row in table.Rows){");
			_sb.AppendFormat("{0} obj = new {0}();\r\n", _entityType.FullName);
			_sb.Append("for(int i = 0; i < columnNames.Length; i++){\r\n");
			_sb.Append("string columnName = columnNames[i];");
			_sb.Append("SetEntityProperty(obj, columnName, row[columnName]);");
			_sb.Append("}\r\n");
			_sb.Append("list.Add(obj);\r\n");
			_sb.Append("}\r\n");
			_sb.Append("return list;\r\n");
			_sb.Append("}\r\n");
		}

		private void DataReaderToSingle()
		{
			_sb.Append("public static object DataReaderToSingle(SqlDataReader dr){\r\n");
			_sb.AppendFormat("{0} obj = null;\r\n", _entityType.FullName);
			_sb.Append("string[] columnNames = CodeUtil.GetColumnNames(dr);\r\n");
			_sb.Append("if(dr.Read()){\r\n");
			_sb.AppendFormat("obj = new {0}();\r\n", _entityType.FullName);
			_sb.Append("for(int i = 0; i < columnNames.Length; i++){\r\n");
			_sb.Append("SetEntityProperty(obj, columnNames[i], dr.GetValue(i));");
			_sb.Append("}\r\n");
			_sb.Append("}\r\n");
			_sb.Append("return obj;\r\n");
			_sb.Append("}\r\n");
		}

		private void XmlToEntity()
		{
			if( _keyFields.Count == 0) {

				_sb.Append("public static object XmlToSingle(string xml){\r\n");
				EnsureExistPrimaryField("Xml转换为实体");

				_sb.Append("public static object XmlToList(string xml){\r\n");
				EnsureExistPrimaryField("Xml转换为实体集合");
				
				return;
			}

			_sb.AppendFormat("private static void XmlToProperty({0} obj, string columnName, string value){{\r\n", _entityType.FullName);
			_sb.Append("if (string.IsNullOrEmpty(value)){return;}");
			_sb.Append("switch( columnName) {\r\n");
			foreach( DbMapInfo info in _allFields ) {
				_sb.AppendFormat("case \"{0}\":", info.DbName.ToLower());
				_sb.Append("{");
				_sb.Append(GetXmlEntitySetCode(info));
				_sb.Append("}");
				_sb.Append("break;\r\n");
			}
			_sb.Append("}}\r\n");

			_sb.AppendFormat("private static void XmlToPrimaryKey({0} obj, XmlReader reader){{\r\n", _entityType.FullName);
			_sb.Append("if( reader.MoveToAttribute(\"keyname\") ) {\r\n");
			_sb.Append("string keyname=reader.ReadContentAsString();\r\n");
			_sb.Append("if( reader.MoveToAttribute(\"keyvalue\") ) {\r\n");
			_sb.Append("string keyval=reader.ReadContentAsString();\r\n");
			_sb.Append("XmlToProperty(obj, keyname.ToLower(), keyval);\r\n");
			_sb.Append("}\r\n");
			_sb.Append("else {\r\n");
			_sb.Append("throw new InvalidOperationException(\"xml中不存在keyvalue属性\");");
			_sb.Append("}}\r\n");
			_sb.Append("else {\r\n");
			_sb.Append("throw new InvalidOperationException(\"xml中不存在keyname属性\");");
			_sb.Append("}\r\n");
			_sb.Append("reader.MoveToElement();}\r\n");

			_sb.AppendFormat("private static void XmlToSingle({0} obj, XmlReader reader){{\r\n", _entityType.FullName);
			_sb.Append("int depth = reader.Depth;");
			_sb.Append("while(reader.Read()){\r\n");
			_sb.Append("if( reader.Depth == depth ) {break;}");
			_sb.Append("if( reader.NodeType == XmlNodeType.Element ) {\r\n");
			_sb.Append("string name = reader.Name;\r\n");
			_sb.Append("string val = reader.ReadString();\r\n");
			_sb.Append("XmlToProperty(obj, name.ToLower(), val);\r\n");
			_sb.Append("}}}\r\n");

			_sb.Append("public static object XmlToSingle(string xml){\r\n");
			_sb.AppendFormat("{0} obj = null;\r\n", _entityType.FullName);
			_sb.Append("using( StringReader sr = new StringReader(xml) ) {\r\n");
			_sb.Append("using( XmlReader reader = XmlTextReader.Create(sr) ) {\r\n");
			_sb.AppendFormat("if( reader.ReadToFollowing(\"{0}\") ) {{\r\n", _tableInfo.DbName);
			_sb.AppendFormat("obj = new {0}();\r\n", _entityType.FullName);
			_sb.Append("XmlToPrimaryKey(obj, reader);\r\n");
			_sb.Append("XmlToSingle(obj, reader);\r\n");
			_sb.Append("}\r\n");
			_sb.Append("else {\r\n");
			_sb.AppendFormat("throw new InvalidOperationException(\"xml中不存{0}节点\");", _tableInfo.DbName);
			_sb.Append("}}}\r\n");
			_sb.Append("return obj;}\r\n");

			_sb.Append("public static object XmlToList(string xml){\r\n");
			_sb.AppendFormat("List<{0}> list = new List<{0}>();\r\n", _entityType.FullName);
			_sb.Append("using( StringReader sr = new StringReader(xml) ) {\r\n");
			_sb.Append("using( XmlReader reader = XmlTextReader.Create(sr) ) {\r\n");
			_sb.AppendFormat("if( reader.ReadToFollowing(\"{0}\") ) {{\r\n", _tableInfo.DbName);
			_sb.AppendFormat("{0} obj = new {0}();\r\n", _entityType.FullName);
			_sb.Append("XmlToPrimaryKey(obj, reader);\r\n");
			_sb.Append("XmlToSingle(obj, reader);\r\n");
			_sb.Append("list.Add(obj);\r\n");
			_sb.AppendFormat("while( reader.ReadToNextSibling(\"{0}\") ) {{\r\n", _tableInfo.DbName);
			_sb.AppendFormat("obj = new {0}();\r\n", _entityType.FullName);
			_sb.Append("XmlToPrimaryKey(obj, reader);\r\n");
			_sb.Append("XmlToSingle(obj, reader);\r\n");
			_sb.Append("list.Add(obj);\r\n");
			_sb.Append("}}\r\n");
			_sb.Append("else {\r\n");
			_sb.AppendFormat("throw new InvalidOperationException(\"xml中不存{0}节点\");", _tableInfo.DbName);
			_sb.Append("}}}\r\n");
			_sb.Append("return list;}\r\n");
		}

		private static Dictionary<string, string> s_dictXmlSetCode = new Dictionary<string, string>(13){
			{"System.Int64", "long tmp;if(long.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Byte[]","obj.{0} = Convert.FromBase64String(value);\r\n"},
			{"TimeStamp","long tmp;if(long.TryParse(value, out tmp)){{obj.{0}=CodeUtil.LongToByte(tmp);}}\r\n"},
			{"System.Boolean","bool tmp;if (bool.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.String","obj.{0} = value;\r\n"},
			{"System.DateTime","DateTime tmp;if(DateTime.TryParse(value, out tmp)){{obj.{0}=tmp;}};\r\n"},
			{"System.Decimal","decimal tmp;if(decimal.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Double","double tmp;if(double.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Int32","int tmp;if(int.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Single","float tmp;if(float.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Int16","short tmp;if(short.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Byte","byte tmp;if(byte.TryParse(value, out tmp)){{obj.{0}=tmp;}}\r\n"},
			{"System.Guid","obj.{0} = new Guid(value);\r\n"},
			{"System.DateTimeOffset","DateTimeOffset tmp;if(DateTimeOffset.TryParse(value, out tmp)){{obj.{0}=tmp;}};\r\n"},
			{"System.TimeSpan","TimeSpan tmp;if(TimeSpan.TryParse(value, out tmp)){{obj.{0} = tmp;}};\r\n"},
		};

		private static Dictionary<string, string> s_dictEntiySetCode = new Dictionary<string, string>(13){
			{"System.Int64", "obj.{0} = (long)value;\r\n"},
			{"TimeStampToLong","obj.{0} = value.GetType() == typeof(long) ? (long)value : CodeUtil.ByteToLong((byte[])value);\r\n"},
			{"TimeStampToByte","obj.{0} = value.GetType() == typeof(long) ? CodeUtil.LongToByte((long)value) : (byte[])value;\r\n"},
			{"System.Byte[]","obj.{0} = (byte[])value;\r\n"},
			{"System.Boolean","obj.{0} = (bool)value;\r\n"},
			{"System.String","obj.{0} = value.ToString();\r\n"},
			{"System.DateTime","obj.{0} = (DateTime)value;\r\n"},
			{"System.Decimal","obj.{0} = (decimal)value;\r\n"},
			{"System.Double","obj.{0} = (double)value;\r\n"},
			{"System.Int32","obj.{0} = (int)value;\r\n"},
			{"System.Single","obj.{0} = (float)value;\r\n"},
			{"System.Int16","obj.{0} = (short)value;\r\n"},
			{"System.Byte","obj.{0} = (byte)value;\r\n"},
			{"System.Guid","obj.{0} = (Guid)value;\r\n"},
			{"System.DateTimeOffset","obj.{0} = (DateTimeOffset)value;\r\n"},
			{"System.TimeSpan","obj.{0} = (TimeSpan)value;\r\n"},
		};

		private string GetDataEntitySetCode(DbMapInfo info)
		{
			Type targetType = Nullable.GetUnderlyingType(info.PropertyInfo.PropertyType) ?? info.PropertyInfo.PropertyType;
			string code, typeName;
			typeName = targetType.ToString();

			if( info.Attr != null && info.Attr.TimeStamp ) {
				if( typeName == "System.Int64" ) {
					typeName = "TimeStampToLong";
				}
				if( typeName == "System.Byte[]" ) {
					typeName = "TimeStampToByte";
				}
			}

			if( s_dictEntiySetCode.TryGetValue(typeName, out code) ) {
				return string.Format(code, info.NetName);
			}
			else {
				throw new NotSupportedException("不支持的实体数据类型");
			}
		}

		private string GetXmlEntitySetCode(DbMapInfo info)
		{
			Type targetType = Nullable.GetUnderlyingType(info.PropertyInfo.PropertyType) ?? info.PropertyInfo.PropertyType;

			string code, typeName;

			typeName = targetType.ToString();

			if( typeName == "System.Byte[]" 
				&& info.Attr != null
				&& info.Attr.TimeStamp) {

				typeName = "TimeStamp";
			}

			if( s_dictXmlSetCode.TryGetValue(typeName, out code) ) {
				return string.Format(code, info.NetName);
			}
			else {
				throw new NotSupportedException("不支持的实体数据类型");
			}
		}

		private void CloneMe()
		{
			_sb.AppendFormat("public static object CloneMe(object src){{\r\n", _entityType.FullName);
			_sb.AppendFormat("{0} entity = ({0})src;\r\n", _entityType.FullName);
			_sb.AppendFormat("{0} obj = new {0}();\r\n", _entityType.FullName);
			foreach(DbMapInfo info in _allFields){
				_sb.AppendFormat("obj.{0} = entity.{0};\r\n", info.NetName);
			}
			_sb.Append("return obj;\r\n");
			_sb.Append("}");
		}
	}
}
