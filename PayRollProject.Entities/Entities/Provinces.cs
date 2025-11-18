using System.ComponentModel.DataAnnotations.Schema;

namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using BaseClass;

	[Table("Provinces")]
	public class Provinces : IEntityObject
	{
		[Key]
		public int ID_Province { get; set; }

		[Required(ErrorMessage = "نام استان نباید خالی باشد")]
		[MaxLength(100,ErrorMessage = "تعداد حروف استان نباید از 100 کاراکتر بیشتر باشد")]
		public string Province_Name { get; set; }

		// ارتباط یک به چند با شهرها
		public virtual ICollection<Cities> Cities { get; set; }
	}
}