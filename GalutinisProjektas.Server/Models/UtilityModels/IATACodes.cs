using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.UtilityModels
{
    public class IATACodes
    {
        // IATA codes for airports Entity Frame Work model

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string IATA { get; set; }
        [Required]
        public string AirportName { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
