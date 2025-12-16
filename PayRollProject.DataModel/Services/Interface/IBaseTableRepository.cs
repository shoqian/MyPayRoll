namespace PayRollProject.DataModel.Services.Interface
{
    using PayRollProject.Entities.Entities;
    using Syncfusion.EJ2.Base;

    public interface IBaseTableRepository : IUnitOfWork
    {
        void UpdateCountry(CRUDModel<Countries> model);

void UpdateProvince(CRUDModel<ProvinceTbl> model);

void DeleteProvince(int provinceId);

void RestoreProvince(int provinceId);

        void UpdateCity(CRUDModel<CitiesTbl> model);

        void DeleteCity(int cityId);

        void RestoreCity(int cityId);

        }
}