namespace PayRollProject.Entities.Entities
{
    using System.ComponentModel.DataAnnotations;
    using BaseClass;

    public class Countries : FieldPublicInherits
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public string Description { get; set; }
 
    }
}