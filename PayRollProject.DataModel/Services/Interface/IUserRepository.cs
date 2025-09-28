namespace PayRollProject.DataModel.Services.Interface
{
    using Entities.Models;

    public interface IUserRepository
    {
        List<UserListDTO> GetUserList();
    }
}