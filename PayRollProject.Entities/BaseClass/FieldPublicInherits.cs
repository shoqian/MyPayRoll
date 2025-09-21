namespace PayRollProject.Entities.BaseClass
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Entities;

    public interface IEntityObject
    {
        // این اینترفیس جهت تشحیص کلاس های مربوط به جداول از سایر کلاسها استفاده می شود
    }


    public abstract class FieldPublicInherits : IEntityObject
    {
        public string UserID { get; set; }

        public DateTime CrateDateTime { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUsers Users { get; set; }
    }
}