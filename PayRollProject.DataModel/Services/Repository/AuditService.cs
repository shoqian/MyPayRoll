using System.Reflection;
using System.Text.Json;
using PayRollProject.DataModel.Services.Interface;
using PayRollProject.Entities.Entities;

namespace PayRollProject.DataModel.Services.Repository
{
	public class AuditService : IAuditService
	{
		private readonly PayRollDbContext _context;

		public AuditService(PayRollDbContext context)
		{
			this._context = context;
		}

		public void AddLog<TEntity>(TEntity before, TEntity after, string operation, string userId)
			where TEntity : class
		{
			var entityName = typeof(TEntity).Name;
			var keyProperty = typeof(TEntity).GetProperties()
				.FirstOrDefault((PropertyInfo p) => p.Name.EndsWith("_ID"));
			var keyValue = keyProperty?.GetValue(after ?? before)?.ToString() ?? "N/A";


			var beforeJson = before != null ? JsonSerializer.Serialize(before) : null;
			var afterJson = after != null ? JsonSerializer.Serialize(after) : null;

			var diff = this.CreateDiffString(before, after);

			var log = new AuditLog
			{
				EntityName = entityName,
				EntityKey = $"{keyProperty?.Name}:{keyValue}",
				Operation = operation,
				UserId = userId,
				DateBefore = beforeJson,
				DateAfter = afterJson,
				Diff = diff
			};
			this._context.Set<AuditLog>().Add(log);
		}

		public void LogChange<TEntity>(TEntity? before, TEntity? after, string operation, string userId)
			where TEntity : class
		{
			var entityName = typeof(TEntity).Name;
			var keyProperty = typeof(TEntity).GetProperties().FirstOrDefault((PropertyInfo p) => p.Name.EndsWith("_ID"));
			var keyValue = keyProperty?.GetValue(after ?? before)?.ToString() ?? "N/A";

			var beforeJson = before != null ? JsonSerializer.Serialize(before) : null;
			var afterJson = after != null ? JsonSerializer.Serialize(after) : null;


			// string operation;
			// if (before == null && after != null)
			// {
			// 	operation = "Create";
			// }
			// else if (before != null && after == null)
			// {
			// 	operation = "Delete";
			// }
			// else
			// {
			// 	operation = "Update";
			// }
			var diff = this.CreateDiffString(before, after);
			var log = new AuditLog
			{
				EntityName = entityName,
				EntityKey = $"{keyProperty?.Name}:{keyValue}",
				Operation = operation,
				UserId = userId,
				DateBefore = beforeJson,
				DateAfter = afterJson,
				Diff = diff
			};
			this._context.Set<AuditLog>().Add(log);
		}

		private string CreateDiffString<TEntity>(TEntity before, TEntity after)
		{
			if (before == null || after == null) return string.Empty;

			var properties = typeof(TEntity).GetProperties();
			var diff = new List<string>();

			foreach (var prop in properties)
			{
				var beforeValue = prop.GetValue(before);
				var afterValue = prop.GetValue(after);

				if (beforeValue != afterValue)
				{
					diff.Add($"{prop.Name}: {beforeValue} => {afterValue}");
				}
			}

			return string.Join(", ", diff);
		}
	}
}