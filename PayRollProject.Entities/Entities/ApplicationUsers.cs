namespace PayRollProject.Entities.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUsers : IdentityUser
    {
        public string FirstName { get; set; }

        public string Family { get; set; }

        public string MelliCode { get; set; }
        
        //// 1 = user
        //// 2 = admin
        public byte UserType { get; set; }
        
        //// 1 = male
        //// 2 = female
        public byte Gender { get; set; }

        //// 1 = active
        //// 2 = deactivate
        public byte UserFlag { get; set; }

        public DateTime? BirthOfDate { get; set; }
    }
}