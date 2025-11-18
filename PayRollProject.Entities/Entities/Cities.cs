namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using BaseClass;

	[Table("Cities")]
	public class Cities : FieldPublicInherits
	{
		[Key]
		public int City_ID { get; set; }

		[Required(ErrorMessage = "پیش شماره نباید خالی باشد.")]
		[MaxLength(4,ErrorMessage = "حداکثر کد باید 4 رقمی باشد.")]
		public string Prefix { get; set; }

		[Required(ErrorMessage = "نام شهر نباید خالی باشد.")]
		[MaxLength(100,ErrorMessage = "تعداد حروف شهر نباید از 100 کاراکتر بیشتر باشد.")]
		public string City_Name { get; set; }

		[ForeignKey(nameof(Province))]
		public int ID_Province { get; set; }

		public virtual Provinces Province { get; set; }


	}
}