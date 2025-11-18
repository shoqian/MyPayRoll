namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using BaseClass;

	public class GeoRuralDistricts : FieldPublicInherits
	{
		[Key]
		public int GeoRuralDistricts_ID { get; set; }

		[Required(ErrorMessage = "نام دهستان نباید خالی باشد.")]
		[MaxLength(100, ErrorMessage = "حداکثر نام یک دهستان 100 کاراکتر می باشد.")]
		public string RuralDistricts_Name { get; set; }

		[ForeignKey(nameof(Province))]
		public int GeoProvince_ID { get; set; }

		[ForeignKey(nameof(County))]
		public int GeoCounty_ID { get; set; }

		[ForeignKey(nameof(District))]
		public int GeoDistricts_ID { get; set; }

		public virtual GeoProvinces Province { get; set; }

		public virtual GeoCounties County { get; set; }

		public virtual GeoDistricts District { get; set; }

		public virtual ICollection<GeoNeighborhoods> Neighborhoods { get; set; }
	}
}