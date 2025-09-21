namespace PayRollProject.DataModel.Services.Repository
{
    using PayRollProject.DataModel.Services.Interface;
    using PayRollProject.Entities.Entities;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PayRollDbContext _context;
        private GenericCRUDClass<ApplicationUsers>? _userManager;
        private GenericCRUDClass<ApplicationRoles>? _roleManager;
        private GenericCRUDClass<Countries>? _countriesTbl;

        public UnitOfWork(PayRollDbContext context)
        {
            this._context = context;
        }

        // کاربران
        public GenericCRUDClass<ApplicationUsers> UserManager
        {
            // فقط خواندنی
            get
            {
                if (this._userManager == null)
                {
                    this._userManager = new GenericCRUDClass<ApplicationUsers>(this._context);
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
                    this._roleManager = new GenericCRUDClass<ApplicationRoles>(this._context);
                }

                return this._roleManager;
            }
        }

        // جدول کشورها
        public GenericCRUDClass<Countries> CountriesUw
        {
            get
            {
                if (this._countriesTbl == null)
                {
                    this._countriesTbl = new GenericCRUDClass<Countries>(this._context);
                }

                return this._countriesTbl;
            }
        }

        public IEntityTransaction BeginTransaction() => new EntityTransaction(this._context);

        public void Save() => this._context.SaveChanges();

        public async void SaveAsync() => await this._context.SaveChangesAsync();


        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}