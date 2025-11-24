using System.ComponentModel.DataAnnotations;
using PayRollProject.Entities.BaseClass;

namespace PayRollProject.Entities.Entities
{
	public class ProvinceTbl : FieldPublicInherits
	{
		[Key]
		public int ProvinceId { get; set; }

		[Required(ErrorMessage = "وارد کردن نام استان اجباری است.")]
		[MaxLength(100, ErrorMessage = "تعداد حروف استان نباید از 100 کاراکتر بیشتر باشد")]
		public string ProvinceName { get; set; }

		[MaxLength(300, ErrorMessage = "توضیحات نباید از 300 کاراکتر بیشتر باشد.")]
		public string? Description { get; set; }

		public bool IsDelete { get; set; }

		public virtual ICollection<CitiesTbl> Cities { get; set; }

	}
}