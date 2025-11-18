namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using BaseClass;

	public class GeoDistricts : FieldPublicInherits
	{
		[Key]
		public int GeoDistricts_ID { get; set; }

		[Required(ErrorMessage = "نام بخش نباید خالی باشد.")]
		[MaxLength(100, ErrorMessage = "حداکثر نام یک بخش 100 کاراکتر می باشد.")]
		public string Districts_Name { get; set; }

		[ForeignKey(nameof(Province))]
		public int GeoProvince_ID { get; set; }

		[ForeignKey(nameof(County))]
		public int GeoCounty_ID { get; set; }

		public virtual GeoProvinces Province { get; set; }

		public virtual GeoCounties County { get; set; }

		public virtual ICollection<GeoRuralDistricts> RuralDistricts { get; set; }
	}
}