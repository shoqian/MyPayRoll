namespace PayRollProject.Entities.BaseClass
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Entities;

    public interface IEntityObject
    {
        // این اینترفیس جهت تشحیص کلاس‌های مربوط به جداول از سایر کلاسها استفاده می‌شود
    }


    public abstract class FieldPublicInherits : IEntityObject
    {
        public string UserID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual ApplicationUsers Users { get; set; }
    }
}