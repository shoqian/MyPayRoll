namespace PayRollProject.DataModel.Services.Interface
{
	public interface IAuditService
	{
		void AddLog<TEntity>(TEntity before,TEntity after,string operation,string userId)
			where TEntity : class;

		void LogChange<TEntity>(TEntity? before,TEntity? after,string operation,string userId)
			where TEntity : class;
	}
}