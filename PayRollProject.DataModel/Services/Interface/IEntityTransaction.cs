namespace PayRollProject.DataModel.Services.Interface
{
    public interface IEntityTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}