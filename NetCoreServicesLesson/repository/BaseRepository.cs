using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;

namespace repository 
{
	public class BaseRepository<T> : IBaseRepository<T>
		where T : BaseEntity
	{
		public readonly IOptions<DbOptions> _dbOptions;
		public readonly string _connectionString;

		public BaseRepository(IOptions<DbOptions> dbOptions)
		{
			_dbOptions = dbOptions;
			_connectionString = _dbOptions.Value.ConnectionString;
		}



		public virtual async Task<T> GetById(Guid id)
		{
			await using var db = await GetSqlConnection();
			return await db.QueryFirstOrDefaultAsync<T>(
				$"SELECT * FROM [{typeof(T).Name}] WHERE [Id] = @Id AND [Isdeleted] = 0", new { id });
		}

		public virtual async Task<IEnumerable<T>> GetAll()
		{
			await using var db = await GetSqlConnection();
			return await db.QueryAsync<T>($"SELECT * FROM [{typeof(T).Name}] WHERE [IsDeleted] = 0");
		}

		

		public virtual async Task Create(T entity)
		{
			try
			{
				await using var db = await GetSqlConnection();

				if (entity.Id == Guid.Empty)
				{
					entity.Id = Guid.NewGuid();
				}

				entity.CreatedDate = DateTime.UtcNow;
				entity.LastSavedDate = DateTime.UtcNow;

				entity.CreatedBy = Guid.NewGuid();        //Заглушка
				entity.LastSavedBy = Guid.NewGuid();      //Заглушка

				var fields = string.Join(", ", typeof(T).GetProperties().Select(property => $"[{property.Name}]"));
				var values = string.Join(", ", typeof(T).GetProperties().Select(property => $"@{property.Name}"));

				await db.ExecuteAsync($"INSERT INTO [{typeof(T).Name}] ({fields}) VALUES ({values})", entity);
			}
			catch (SqlException ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public virtual async Task CreateMany(IEnumerable<T> entities)
		{
			await using var db = await GetSqlConnection();

			foreach (var entity in entities)
			{
				if (entity.Id == Guid.Empty)
				{
					entity.Id = Guid.NewGuid();
				}

				entity.CreatedDate = DateTime.UtcNow;
				entity.LastSavedDate = DateTime.UtcNow;

				entity.CreatedBy = Guid.NewGuid();     //Заглушка
				entity.LastSavedBy = Guid.NewGuid();   //Заглушка
			}

			var fields = string.Join(", ", typeof(T).GetProperties().Select(property => $"[{property.Name}]"));
			var values = string.Join(", ", typeof(T).GetProperties().Select(property => $"@{property.Name}"));

			await db.ExecuteAsync($"INSERT INTO [{typeof(T).Name}] ({fields}) VALUES ({values})", entities);
		}



		public virtual async Task Update(T entity)
		{
			try
			{
				await using var db = await GetSqlConnection();

				entity.LastSavedDate = DateTime.UtcNow;
				entity.LastSavedBy = Guid.NewGuid();   //Заглушка

				var notUpdateFields = new[] { "Id", "CreatedDate", "CreatedBy", "IsDeleted" };
				var parameters = string.Join(", ",
					typeof(T).GetProperties().Where(property => !notUpdateFields.Contains(property.Name))
						.Select(property => $"{property.Name} = @{property.Name}"));

				await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET {parameters} WHERE [Id] = @Id", entity);
			}
			catch (SqlException ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public virtual async Task UpdateMany(IEnumerable<T> entities)
		{
			await using var db = await GetSqlConnection();

            foreach (var entity in entities)
            {
                entity.LastSavedDate = DateTime.UtcNow;
				entity.LastSavedBy = Guid.NewGuid();   //Заглушка
			}
			
            var notUpdateFields = new[] {"Id", "CreatedDate", "CreatedBy", "IsDeleted"};
            var parameters = string.Join(", ",
                    typeof(T).GetProperties().Where(property => !notUpdateFields.Contains(property.Name))
                        .Select(property => $"{property.Name} = @{property.Name}"));

            await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET {parameters} WHERE [Id] = @Id", entities);   
		}



		public virtual async Task Delete(Guid id)
		{
			await using var db = await GetSqlConnection();
			await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET IsDeleted = 1 WHERE Id = @Id", new { id });
		}

		public virtual async Task DeleteMany(IEnumerable<Guid> entityIds)
		{
			await using var db = await GetSqlConnection();

			var ids = string.Join(",", entityIds.Select(id => $"'{id}'"));
			await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET IsDeleted = 1 WHERE Id IN ({ids})");
		}
		


		public virtual async Task Restore(Guid id)
		{
			await using var db = await GetSqlConnection();
			await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET IsDeleted = 0 WHERE Id = @Id", new { id });
		}

		public virtual async Task RestoreMany(IEnumerable<Guid> entityIds)
		{
			await using var db = await GetSqlConnection();

			var ids = string.Join(",", entityIds.Select(id => $"'{id}'"));
			await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET IsDeleted = 0 WHERE Id IN ({ids})");
		}



		protected async Task<SqlConnection> GetSqlConnection()
		{
			var db = new SqlConnection(_connectionString);
			db.Open();
			return db;
		}
	}
}