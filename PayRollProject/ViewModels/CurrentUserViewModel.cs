namespace PayRollProject.ViewModels
{
	public class CurrentUserViewModel
	{
		public string Id { get; set; }

		public string FirstName { get; set; }

		public string Family { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public bool IsActive { get; set; }

		public string FullName => string.IsNullOrWhiteSpace(this.FirstName) && string.IsNullOrWhiteSpace(this.Family)
			? this.Email
			: $"{this.FirstName} {this.Family}";
	}
}