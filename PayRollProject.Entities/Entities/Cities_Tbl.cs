namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using BaseClass;

	[Table("Cities_Tbl")]
	public class CitiesTbl : FieldPublicInherits
	{
		[Key]
		public int CityId { get; set; }

		[Required(ErrorMessage = "وارد کردن نام شهر اجباری است.")]
		[MaxLength(100,ErrorMessage = "تعداد حروف شهر نباید از 100 کاراکتر بیشتر باشد")]
		public string CityName { get; set; }

		[MaxLength(300,ErrorMessage = "توضیحات نباید از 300 کاراکتر بیشتر باشد.")]
		public string Description { get; set; }

		public bool IsDelete { get; set; }

		[ForeignKey(nameof(Province))]
		public int ProvinceID { get; set; }

		public virtual ProvinceTbl Province { get; set; }
	}
}