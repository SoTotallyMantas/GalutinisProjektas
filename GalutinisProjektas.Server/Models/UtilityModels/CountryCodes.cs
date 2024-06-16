using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalutinisProjektas.Server.Models.UtilityModels
{
    public class CountryCodes
    {
        // Country codes for entity Frame work model

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string CountryName { get; set; }
    }
}
