namespace PayRollProject.DataModel.Services.Repository
{
    using PayRollProject.DataModel.Services.Interface;
    using PayRollProject.Entities.Entities;

    using Syncfusion.EJ2.Base;

    public class BaseTableRepository : UnitOfWork, IBaseTableRepository
    {
        private readonly PayRollDbContext _context;

        public BaseTableRepository(PayRollDbContext context)
            : base(context)
        {
            _context = context;
        }

        public void UpdateCountry(CRUDModel<Countries> model)
        {
            var query = this.CountriesUw.GetById(model.Value.CountryID);
            if (query != null)
            {
                query.CountryName = model.Value.CountryName;
                query.Description = model.Value.Description;

                this.CountriesUw.Update(query);
                this.Save();
            }
        }
    }
}