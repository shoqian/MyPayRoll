namespace PayRollProject.DataModel.Services.Interface
{
	using Repository;
	using PayRollProject.Entities.Entities;

	public interface IUnitOfWork
	{
		// کاربران
		GenericCRUDClass<ApplicationUsers> UserManager { get; }

		// نقش‌ها
		GenericCRUDClass<ApplicationRoles> RoleManager { get; }

		// جدول کشورها
		GenericCRUDClass<Countries> CountriesUw { get; }

		// جداول جغرافیایی می‌توانند اینجا اضافه شوند
		GenericCRUDClass<GeoProvinces> GeoProvincesUw { get; }

		GenericCRUDClass<GeoCounties> GeoCountiesUw { get; }

		GenericCRUDClass<GeoDistricts> GeoDistrictsUw { get; }

		GenericCRUDClass<GeoRuralDistricts> GeoRuralDistrictsUw { get; }

		GenericCRUDClass<GeoNeighborhoods> GeoNeighborhoodsUw { get; }


		IEntityTransaction BeginTransaction();

		void Save();

		void SaveAsync();
	}
}