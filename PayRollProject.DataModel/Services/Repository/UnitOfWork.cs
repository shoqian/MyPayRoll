namespace PayRollProject.DataModel.Services.Repository
{
    using PayRollProject.DataModel.Services.Interface;
    using PayRollProject.Entities.Entities;

    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly PayRollDbContext _context;

        public UnitOfWork(PayRollDbContext context)
        {
            this._context = context;
        }

        private GenericCRUDClass<ApplicationUsers>? _userManager;
        private GenericCRUDClass<ApplicationRoles>? _roleManager;



        // کاربران
        public GenericCRUDClass<ApplicationUsers> userManager
        {
            // فقط خواندنی
            get
            {
                if (this._userManager==null)
                {
                    this._userManager = new GenericCRUDClass<ApplicationUsers>(this._context);
                }
                return this._userManager;
            }
        }

        // نقش‌ها
        public GenericCRUDClass<ApplicationRoles> roleManager
        {
            // فقط خواندنی
            get
            {
                if (this._roleManager==null)
                {
                    this._roleManager = new GenericCRUDClass<ApplicationRoles>(this._context);
                }

                return this._roleManager;
            }
        }
        
        public IEntityTransaction BeginTransaction() => new EntityTransaction(_context);
        
        public void Save() => _context.SaveChanges();

        public async void SaveAsync() => await _context.SaveChangesAsync();


        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}