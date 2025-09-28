namespace PayRollProject.DataModel.Services.Repository
{
    using Entities.Models;
    using Interface;

    public class UserRepository(PayRollDbContext context) : IUserRepository
    {
        public List<UserListDTO> GetUserList()
        {
            var query = (from user in context.Users
                         where (user.UserType == 1 || user.UserType == 2)
                         select new UserListDTO()
                         {
                             Id = user.Id,
                             UserName = user.UserName,
                             FirstName = user.FirstName,
                             Family = user.Family,
                             Email = user.Email,
                             MelliCode = user.MelliCode,
                             PhoneNumber = user.PhoneNumber,
                             Gender = user.Gender,
                             GenderText = (user.Gender == 1 ? "آقا" : "خانم"),
                             UserType = user.UserType,
                             UserTypeText = (user.UserType == 1 ? "ادمین" : user.UserType == 2 ? "کاربر" : "پرسنل"),
                             UserFlag = user.UserFlag,
                             UserFlagText = (user.UserFlag == 1 ? "فعال" : "غیرفعال")
                         }).ToList();
            return query;
        }
    }
}