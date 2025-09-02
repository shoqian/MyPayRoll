namespace PayRollProject.DataModel.Services.Repository
{
    using Microsoft.EntityFrameworkCore.Storage;

    using PayRollProject.DataModel.Services.Interface;

    public class EntityTransaction : IEntityTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EntityTransaction(PayRollDbContext context)
        {
            this._transaction = context.Database.BeginTransaction();
        }

        public void Commit() => _transaction.Commit();

        public void Rollback() => _transaction.Rollback();

        public void Dispose() => _transaction.Dispose();
    }
}