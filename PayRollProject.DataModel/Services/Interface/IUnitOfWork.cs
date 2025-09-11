namespace PayRollProject.DataModel.Services.Interface
{
    using PayRollProject.DataModel.Services.Repository;
    using PayRollProject.Entities.Entities;

    public interface IUnitOfWork
    {
        
        GenericCRUDClass<ApplicationUsers> userManager { get; }
        
        GenericCRUDClass<ApplicationRoles> roleManager { get; }
        GenericCRUDClass<Countries> countriesUW { get; }

        IEntityTransaction BeginTransaction();

        void Save();

       void SaveAsync();

    }
}