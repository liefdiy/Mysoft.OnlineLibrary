using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mysoft.Map.Extensions.DAL;

namespace Mysoft.Map.Extensions.CodeDom
{
	internal class DbMapInfo
	{
		public string DbName { get; private set; }

		public string NetName { get; private set; }

		public PropertyInfo PropertyInfo { get; private set; }

		public DataColumnAttribute Attr { get; private set; }

		public DbMapInfo(string dbName, string netName, DataColumnAttribute attr, PropertyInfo prop)
		{
			this.DbName = dbName;
			this.NetName = netName;
			this.Attr = attr;
			this.PropertyInfo = prop;
		}

		//public static DbMapInfo GetTableName(Type entityType)
		//{
		//	DataEntityAttribute attr = entityType.GetMyAttribute<DataEntityAttribute>();
		//	string fieldName = string.IsNullOrEmpty(attr.Alias) ? entityType.Name : attr.Alias;
		//	return new DbMapInfo(fieldName, entityType.Name, null, null);
		//}

		//private static IEnumerable<DbMapInfo> GetSpecificField(Type entityType, Func<DataColumnAttribute, bool> predicate)
		//{
		//	foreach( PropertyInfo property in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public) ) {
		//		DataColumnAttribute column = property.GetMyAttribute<DataColumnAttribute>();

		//		if( column != null && predicate(column) ) {
		//			string fieldName = (string.IsNullOrEmpty(column.Alias) ? property.Name : column.Alias);
		//			yield return new DbMapInfo(fieldName, property.Name, column, property);
		//		}
		//	}
		//}

		//public static List<DbMapInfo> GetPrimaryKeyFields(Type entityType)
		//{
		//	return GetSpecificField(entityType, c => c.PrimaryKey).ToList();
		//}

		//public static DbMapInfo GetTimeStampField(Type entityType)
		//{
		//	return GetSpecificField(entityType, c => c.TimeStamp).FirstOrDefault();
		//}

		//public static DbMapInfo GetIdentityField(Type entityType)
		//{
		//	return GetSpecificField(entityType, c => c.Identity).FirstOrDefault();
		//}

		//public static DbMapInfo GetSeqGuidField(Type entityType)
		//{
		//	return GetSpecificField(entityType, c => c.SeqGuid).FirstOrDefault();
		//}

		//public static List<DbMapInfo> GetAllFields(Type entityType)
		//{
		//	List<DbMapInfo> list = new List<DbMapInfo>(20);

		//	foreach( PropertyInfo property in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public) ) {
		//		string fieldName = property.Name;

		//		DataColumnAttribute column = property.GetMyAttribute<DataColumnAttribute>();				
		//		if( column != null && string.IsNullOrEmpty(column.Alias) == false )
		//			fieldName = column.Alias;

		//		list.Add(new DbMapInfo(fieldName, property.Name, column, property));
		//	}
		//	return list;
		//}
	}
}
