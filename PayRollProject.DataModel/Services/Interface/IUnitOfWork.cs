namespace PayRollProject.DataModel.Services.Interface
{
    using PayRollProject.DataModel.Services.Repository;
    using PayRollProject.Entities.Entities;

    public interface IUnitOfWork
    {
        
        GenericCRUDClass<ApplicationUsers> UserManager { get; }
        
        GenericCRUDClass<ApplicationRoles> RoleManager { get; }
        
        GenericCRUDClass<Countries> CountriesUw { get; }

        IEntityTransaction BeginTransaction();

        void Save();

       void SaveAsync();

    }
}