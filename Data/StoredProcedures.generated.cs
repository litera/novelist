using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using NPoco;

namespace Novelist.Data
{
	#region Generated code 

	/// <summary>Defines all strongly typed methods for calling stored procedures.</summary>
	[GeneratedCode("TextTemplatingFileGenerator", "10")] // this attribute makes code analysis to skip this class
	internal static class StoredProcedures
	{
		#region Utility method 

		private static IEnumerable<SqlDataRecord> CreateDataRecords<T>(IEnumerable<T> items, SqlMetaData metaData)
		{
			SqlDataRecord record = new SqlDataRecord(metaData);
			foreach (T item in items)
			{
				record.SetValue(0, item);
				yield return record;    
			}
		} 
		  
		#endregion 

		#region Post class

		/// <summary>Defines all Post related stored procedure calls.</summary>
		internal static partial class Post
		{

			public static Sql Create(int? authorId, string title, string content)
			{
				Sql result = Sql.Builder.Append(";exec dbo.[Post_Create] @AuthorId, @Title, @Content", new {
					AuthorId = authorId,
					Title = title,
					Content = content
				});

				return result;
			}

			public static Sql Featurize()
			{
				Sql result = Sql.Builder.Append(";exec dbo.[Post_Featurize] ", new {
					
				});

				return result;
			}

			public static Sql Get(int? postId)
			{
				Sql result = Sql.Builder.Append(";exec dbo.[Post_Get] @PostId", new {
					PostId = postId
				});

				return result;
			}

			public static Sql GetAll()
			{
				Sql result = Sql.Builder.Append(";exec dbo.[Post_GetAll] ", new {
					
				});

				return result;
			}
		}

		#endregion

		#region User class

		/// <summary>Defines all User related stored procedure calls.</summary>
		internal static partial class User
		{

			public static Sql Authenticate(string email, int? hash)
			{
				Sql result = Sql.Builder.Append(";exec dbo.[User_Authenticate] @Email, @Hash", new {
					Email = email,
					Hash = hash
				});

				return result;
			}

			public static Sql Create(string name, string email, int? hash)
			{
				Sql result = Sql.Builder.Append(";exec dbo.[User_Create] @Name, @Email, @Hash", new {
					Name = name,
					Email = email,
					Hash = hash
				});

				return result;
			}

			public static Sql Get(int? id, string email)
			{
				Sql result = Sql.Builder.Append(";exec dbo.[User_Get] @Id, @Email", new {
					Id = id,
					Email = email
				});

				return result;
			}
		}

		#endregion

	}   
	 
	#endregion  
}

