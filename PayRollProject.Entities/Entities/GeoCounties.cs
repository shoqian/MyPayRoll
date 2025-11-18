namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using BaseClass;

	// کلاس مربوط به شهرستان‌ها
	public class GeoCounties : FieldPublicInherits
	{
		[Key]
		public int GeoCounty_ID { get; set; }

		[Required(ErrorMessage = "نام شهرستان نباید خالی باشد.")]
		[MaxLength(100, ErrorMessage = "حداکثر نام یک شهرستان 100 کاراکتر می باشد.")]
		public string County_Name { get; set; }


		[ForeignKey(nameof(Province))]
		public int GeoProvince_ID { get; set; }

		public virtual GeoProvinces Province { get; set; }

		public virtual ICollection<GeoDistricts> Districts { get; set; }
	}
}