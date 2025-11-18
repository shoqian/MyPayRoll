namespace PayRollProject.Entities.Entities
{
	using System.ComponentModel.DataAnnotations;
	using BaseClass;

	public class AuditLog : IEntityObject
	{
		[Key]
		public long AuditId { get; set; }

		[Required(ErrorMessage = "فیلد نام اجباری است")]
		[MaxLength(100)]
		public string EntityName { get; set; }

		[Required(ErrorMessage = "فیلد کلید اجباری است.")]
		[MaxLength(100)]
		public string EntityKey { get; set; }

		[MaxLength(50)]
		public string Operation { get; set; }

		public string? UserId { get; set; }

		public DateTime Timestamp { get; set; }

		public string? DateBefore { get; set; }

		public string? DateAfter { get; set; }

		public string? Diff { get; set; }
	}
}