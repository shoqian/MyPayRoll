namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using BaseClass;

	// کلاس مربوط به استان‌ها
	public class GeoProvinces : FieldPublicInherits
	{
		[Key]
		public int GeoProvince_ID { get; set; }

		[Required(ErrorMessage = "نام استان نباید خالی باشد.")]
		[MaxLength(100,ErrorMessage = "حداکثر نام یک استان 100 کاراکتر می باشد.")]
		public string Province_Name { get; set; }

		public virtual ICollection<GeoCounties> Counties { get; set; }
	}
}