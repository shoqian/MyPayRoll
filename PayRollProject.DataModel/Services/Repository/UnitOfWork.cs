namespace PayRollProject.DataModel.Services.Repository
{
	using Interface;
	using PayRollProject.Entities.Entities;

	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly PayRollDbContext _context;

		// مدیریت جداول عمومی
		private GenericCRUDClass<ApplicationUsers>? _userManager;
		private GenericCRUDClass<ApplicationRoles>? _roleManager;
		private GenericCRUDClass<Countries>? _countriesTbl;
private GenericCRUDClass<ProvinceTbl> ? _provinceTbl;
private GenericCRUDClass<CitiesTbl> ? _cityTbl;

		// مدیریت جداول جغرافیایی
		private GenericCRUDClass<GeoProvinces> _geoProvincesTbl;
		private GenericCRUDClass<GeoCounties> _geoCountiesTbl;
		private GenericCRUDClass<GeoDistricts> _geoDistrictsTbl;
		private GenericCRUDClass<GeoRuralDistricts> _geoRuralDistrictsTbl;
		private GenericCRUDClass<GeoNeighborhoods> _geoNeighborhoodsTbl;

		public UnitOfWork(PayRollDbContext context)
		{
			this._context = context;
		}

		// پیاده‌سازی properties 
		// کاربران
		public GenericCRUDClass<ApplicationUsers> UserManager
		{
			// فقط خواندنی
			get
			{
				if (this._userManager == null)
				{
					this._userManager = new(this._context);
				}

				return this._userManager;
			}
		}

		// نقش‌ها
		public GenericCRUDClass<ApplicationRoles> RoleManager
		{
			// فقط خواندنی
			get
			{
				if (this._roleManager == null)
				{
					this._roleManager = new(this._context);
				}

				return this._roleManager;
			}
		}

		// جدول کشورها
		public GenericCRUDClass<Countries> CountriesUw =>
			this._countriesTbl ??= new(this._context);
		// جدول استان‌ها
		public GenericCRUDClass<ProvinceTbl> ProvincesUw
		{
			// فقط خواندنی
			get
			{
				if (this._provinceTbl == null)
				{
					this._provinceTbl = new GenericCRUDClass<ProvinceTbl>(this._context);
				}

				return this._provinceTbl;
			}
		}
		// جدول شهرها
		public GenericCRUDClass<CitiesTbl> CitiesUw => this._cityTbl ??= new(this._context);
		// جداول جغرافیایی می‌توانند اینجا اضافه شوند
		public GenericCRUDClass<GeoProvinces> GeoProvincesUw =>
			this._geoProvincesTbl ??= new(this._context);

		public GenericCRUDClass<GeoCounties> GeoCountiesUw =>
			this._geoCountiesTbl ??= new(this._context);

		public GenericCRUDClass<GeoDistricts> GeoDistrictsUw =>
			this._geoDistrictsTbl ??= new(this._context);

		public GenericCRUDClass<GeoRuralDistricts> GeoRuralDistrictsUw =>
			this._geoRuralDistrictsTbl ??= new GenericCRUDClass<GeoRuralDistricts>(this._context);

		public GenericCRUDClass<GeoNeighborhoods> GeoNeighborhoodsUw =>
			this._geoNeighborhoodsTbl ??= new(this._context);

		public IEntityTransaction BeginTransaction() => new EntityTransaction(this._context);

		public void Save() => this._context.SaveChanges();

		public async void SaveAsync() => await this._context.SaveChangesAsync();

		public void Dispose() => this._context.Dispose();
	}
}