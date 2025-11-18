using Microsoft.EntityFrameworkCore;
using PayRollProject.Entities.Entities;

namespace PayRollProject.DataModel.Services.Repository
{
	using Entities.Models;
	using Interface;

	public class UserRepository : IUserRepository
	{
		private readonly PayRollDbContext _context;

		public UserRepository(PayRollDbContext context)
		{
			_context = context;
		}

		public List<UserListDTO> GetUserList()
		{
			var query = (from user in _context.Users
				where (user.UserType == 1 || user.UserType == 2)
				select new UserListDTO
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

		public void DeactivateUser(string melliCode)
		{
			var currentUser = this._context.Users.FirstOrDefault((ApplicationUsers u) => u.UserName == melliCode);
			if (currentUser != null)
			{
				currentUser.UserFlag = 2; // Deactivate user
				this._context.Users.Attach(currentUser);
				this._context.Entry(currentUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				this._context.SaveChanges();
			}
		}

		public void ActiveUser(string melliCode)
		{
			var currentUser = this._context.Users.FirstOrDefault((ApplicationUsers u) => u.UserName == melliCode);
			if (currentUser != null)
			{
				currentUser.UserFlag = 1; // Active User
				this._context.Users.Attach(currentUser);
				this._context.Entry(currentUser).State = EntityState.Modified;
				this._context.SaveChanges();
			}
		}
	}
}