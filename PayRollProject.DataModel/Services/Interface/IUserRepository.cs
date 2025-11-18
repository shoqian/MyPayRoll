namespace PayRollProject.DataModel.Services.Interface
{
    using Entities.Models;

    public interface IUserRepository
    {
        List<UserListDTO> GetUserList();
        
        void DeactivateUser(string melliCode);

        void ActiveUser(string melliCode);
    }
}