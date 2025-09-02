namespace PayRollProject.DataModel.Services.Repository
{
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    public class GenericCRUDClass<Entity>
        where Entity : class
    {
        private readonly PayRollDbContext _context;

        private readonly DbSet<Entity> _table;

        public GenericCRUDClass(PayRollDbContext context)
        {
            _context = context;
            _table = context.Set<Entity>();
        }

        #region Select Or Get By Id Or Select All

        /// <summary>
        /// پیدا کردن یک ردیف از اطلاعات توسط شاخص آیدی.
        /// </summary>
        /// <param name="id">
        /// شاحص آیدی.
        /// </param>
        /// <returns>
        /// حروجی جدول یک ردیف از اطلاعات میباشد <see cref="Entity?"/>.
        /// </returns>
        public virtual Entity? GetById(object id)
        {
            return this._table.Find(id);
        }

        /// <summary>
        /// تابع نمایش اطلاعات یک جدول طبق شرط و جوین اطلاعات داده شده.
        /// </summary>
        /// <param name="whereVariable">
        /// شرط مشخص جدول.
        /// </param>
        /// <param name="joinString">
        /// دشتورات جوین برای جداول داده شده.
        /// </param>
        /// <returns>
        /// خروجی تابع هم یک جدول مشخص میباشد..
        /// </returns>
        public virtual IEnumerable<Entity> Get(
            Expression<Func<Entity, bool>>? whereVariable = null,
            string joinString = "")
        {
            IQueryable<Entity> query = this._table;
            if (whereVariable != null)
            {
                query = query.Where(whereVariable);
            }

            if (joinString != "")
            {
                foreach (string item in joinString.Split(","))
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

        #endregion

        #region Cearte Or Add Item

        /// <summary>
        /// اضافه کردن اطلاعات به جدول مشخص در دیتابیس.
        /// </summary>
        /// <param name="entity">
        /// اطلاعات مشخص.
        /// </param>
        /// <returns>
        /// این تابع بدون خروجی میباشد <see cref="void"/>.
        /// </returns>
        public virtual void Create(Entity entity)
        {
            _table.Add(entity);
        }

        #endregion

        #region Update

        /// <summary>
        /// بروزرسانی اطلاعات یک جدول مشخص در دیتابیس.
        /// </summary>
        /// <param name="entity">
        /// اطلاعات لازم بروزرسانی شده.
        /// </param>
        /// <returns>
        /// این تابع بدون خروجی میباشد <see cref="void"/>.
        /// </returns>
        public virtual void Update(Entity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete

        /// <summary>
        /// حذف اطلاعات مشخص از جدول در دیتابیس.
        /// </summary>
        /// <param name="entity">
        /// محتوای انتخاب شده.
        /// </param>
        /// <returns>
        /// این تابع بدون خروجی می‌باشد. <see cref="void"/>.
        /// </returns>
        public virtual void Delete(Entity entity)
        {
            if (this._context.Entry(entity).State == EntityState.Detached)
            {
                this._table.Attach(entity);
            }

            this._table.Remove(entity);
        }

        /// <summary>
        /// حذف اطلاعات جدول توسط id.
        /// </summary>
        /// <param name="id">
        /// ورودی یک object از id.
        /// </param>
        /// <returns>
        /// این تابع بدون خروجی میباشد <see cref="void"/>.
        /// </returns>
        public virtual void DeleteById(object id)
        {
            var entity = this.GetById(id);
            if (entity != null) Delete(entity);
        }

        /// <summary>
        /// حذف اطلاعات جدول بصورت یکسری و یک رنج از اطلاعات مشخص.
        /// </summary>
        /// <param name="entities">
        /// یکسری اطلاعات مشخص.
        /// </param>
        /// <returns>
        /// The <see cref="void"/>.
        /// </returns>
        public virtual void DeleteByRange(IEnumerable<Entity> entities) => this._table.RemoveRange(entities);

        #endregion


        
    }
}