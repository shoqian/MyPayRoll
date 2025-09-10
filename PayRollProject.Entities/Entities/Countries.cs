namespace PayRollProject.Entities.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Countries
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public string Description { get; set; }
    }
}