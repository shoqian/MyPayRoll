namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using BaseClass;

	public class GeoNeighborhoods : FieldPublicInherits
	{
		[Key]
		public int GeoNeighborhoods_ID { get; set; }

		[Required(ErrorMessage = "نام محله نباید خالی باشد.")]
		[MaxLength(100, ErrorMessage = "حداکثر نام یک محله 100 کاراکتر می باشد.")]
		public string Neighborhoods_Name { get; set; }

		[MaxLength(100, ErrorMessage = "نام شهر نباید از 100 کاراکتر بیشتر باشد.")]
		public string City_Name { get; set; }

		[ForeignKey(nameof(Province))]
		public int GeoProvince_ID { get; set; }

		[ForeignKey(nameof(County))]
		public int GeoCounty_ID { get; set; }

		[ForeignKey(nameof(District))]
		public int GeoDistricts_ID { get; set; }

		[ForeignKey(nameof(RuralDistrict))]
		public int GeoRuralDistricts_ID { get; set; }

		public virtual GeoProvinces Province { get; set; }

		public virtual GeoCounties County { get; set; }

		public virtual GeoDistricts District { get; set; }

		public virtual GeoRuralDistricts RuralDistrict { get; set; }
	}
}