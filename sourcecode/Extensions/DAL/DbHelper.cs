using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Mysoft.Map.Extensions.Reflection;
using Mysoft.Map.Extensions.CodeDom;
using System.Xml;
using System.IO;

namespace Mysoft.Map.Extensions.DAL
{
	internal static class DbHelper
	{
		internal static int ExecuteNonQuery(SqlCommand command)
		{
			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<int>(
					command,
					cmd => cmd.ExecuteNonQuery()
					);
			}
		}


		internal static DataTable FillDataTable(SqlCommand command)
		{
			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<DataTable>(
					command,
					cmd => {
						using( SqlDataReader reader = cmd.ExecuteReader() ) {
							DataTable table = new DataTable("_tb");
							table.Load(reader);
							return table;
						}
					}
					);
			}
		}

		internal static DataSet FillDataSet(SqlCommand command)
		{
			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<DataSet>(
					command,
					cmd => {
						DataSet ds = new DataSet();
						SqlDataAdapter adapter = new SqlDataAdapter(cmd);
						adapter.Fill(ds);
						for( int i = 0; i < ds.Tables.Count; i++ ) {
							ds.Tables[i].TableName = "_tb" + i.ToString();
						}
						return ds;
					}
					);
			}
		}

		internal static T ExecuteScalar<T>(SqlCommand command)
		{
			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<T>(
					command,
					cmd => ConvertScalar<T>(cmd.ExecuteScalar())
					);
			}
		}

		internal static T ConvertScalar<T>(object obj)
		{
			if( obj == null || DBNull.Value.Equals(obj) )
				return default(T);

			if( obj.GetType() == typeof(T) )
				return (T)obj;


			return (T)Convert.ChangeType(obj, typeof(T));
		}




		internal static List<T> FillScalarList<T>(SqlCommand command)
		{
			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<List<T>>(
					command,
					cmd => {
						List<T> list = new List<T>();
						using( SqlDataReader reader = cmd.ExecuteReader() ) {
							while( reader.Read() ) {
								list.Add(ConvertScalar<T>(reader[0]));
							}
							return list;
						}
					}
					);
			}
		}

		internal static List<T> ToList<T>(SqlCommand cmd) where T : class, new()
		{
			Type type = typeof(T);

			TypeDescription description = TypeDescriptionCache.GetTypeDiscription(type);

			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<List<T>>(cmd, p => {
					using( SqlDataReader reader = p.ExecuteReader() ) {
						if( description.ExecuteFunc != null ) {
							return description.ExecuteFunc(1, new object[] { reader }) as List<T>;
						}
						else if( type.IsSubclassOf(typeof(BaseEntity)) )
							throw BaseEntity.GetNonStandardExecption(type);
						else
							return ToList<T>(reader, description);
					}
				});
			}
		}

		private static List<T> ToList<T>(SqlDataReader reader, TypeDescription description) where T : class, new()
		{
			Type type = typeof(T);

			Dictionary<string, DbMapInfo> dict = description.MemberDict;

			List<T> list = new List<T>();
			string[] names = GetColumnNames(reader);
			while( reader.Read() ) {
				T obj = Activator.CreateInstance(type) as T;
				for( int i = 0; i < names.Length; i++ ) {
					string name = names[i];

					DbMapInfo info;
					if( dict.TryGetValue(name, out info) ) {
						object val = reader.GetValue(i);

						if( val != null && DBNull.Value.Equals(val) == false ) {
							if( info.Attr != null && info.Attr.TimeStamp ) {
								info.PropertyInfo.FastSetValue(obj, val.ConvertToTimeStamp(info.PropertyInfo.PropertyType));
							}
							else {
								info.PropertyInfo.FastSetValue(obj, val.Convert(info.PropertyInfo.PropertyType));
							}
						}
					}
				}
				list.Add(obj);
			}
			return list;
		}

		internal static List<T> ToList<T>(DataTable table, TypeDescription description) where T : class, new()
		{
			Type type = typeof(T);

			Dictionary<string, DbMapInfo> dict = description.MemberDict;

			List<T> list = new List<T>();
			foreach(DataRow row in table.Rows) {
				T obj = Activator.CreateInstance(type) as T;
				for( int i = 0; i < table.Columns.Count; i++ ) {
					string name = table.Columns[i].ColumnName;
					DbMapInfo info;
					if( dict.TryGetValue(name, out info) ) {
						object val = row[i];

						if( val != null && DBNull.Value.Equals(val) == false ) {
							if( info.Attr != null && info.Attr.TimeStamp ) {
								info.PropertyInfo.FastSetValue(obj, val.ConvertToTimeStamp(info.PropertyInfo.PropertyType));
							}
							else {
								info.PropertyInfo.FastSetValue(obj, val.Convert(info.PropertyInfo.PropertyType));
							}
						}
					}
				}
				list.Add(obj);
			}
			return list;
		}


		internal static T ToSingle<T>(SqlCommand cmd) where T : class, new()
		{

			Type type = typeof(T);

			TypeDescription description = TypeDescriptionCache.GetTypeDiscription(type);

			using( ConnectionScope scope = new ConnectionScope() ) {
				return scope.Current.ExecuteCommand<T>(cmd, p => {
					using( SqlDataReader reader = p.ExecuteReader() ) {
                        if( description.ExecuteFunc != null ) {
							return description.ExecuteFunc(2, new object[] { reader }) as T;
                        }
						else if( type.IsSubclassOf(typeof(BaseEntity)) )
							throw BaseEntity.GetNonStandardExecption(type);
						else
							return ToSingle<T>(reader, description);
					}
				});
			}
		}

		private static T ToSingle<T>(SqlDataReader reader, TypeDescription description) where T : class, new()
        {
            Type type = typeof(T);

			Dictionary<string, DbMapInfo> dict = description.MemberDict;

            if( reader.Read() ) {
                string[] names = GetColumnNames(reader);

                T obj = Activator.CreateInstance(type) as T;
                for( int i = 0; i < names.Length; i++ ) {
                    string name = names[i];

					DbMapInfo info;
					if( dict.TryGetValue(name, out info) ) {
                        object val = reader.GetValue(i);

						if( val != null && DBNull.Value.Equals(val) == false ) {
							if( info.Attr != null && info.Attr.TimeStamp ) {
								info.PropertyInfo.FastSetValue(obj, val.ConvertToTimeStamp(info.PropertyInfo.PropertyType));
							}
							else {
								info.PropertyInfo.FastSetValue(obj, val.Convert(info.PropertyInfo.PropertyType));
							}
						}
                    }
                }
                return obj;
            }
            else {
                return default(T);
            }
        }

		internal static string[] GetColumnNames(SqlDataReader reader)
		{
			int count = reader.FieldCount;
			string[] names = new string[count];
			for( int i = 0; i < count; i++ ) {
				names[i] = reader.GetName(i);
			}
			return names;
		}
	}
}
