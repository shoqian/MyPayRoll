namespace PayRollProject.DataModel.Services.Interface
{
    using PayRollProject.Entities.Entities;
    using Syncfusion.EJ2.Base;

    public interface IBaseTableRepository : IUnitOfWork
    {
        void UpdateCountry(CRUDModel<Countries> model);
    }
}