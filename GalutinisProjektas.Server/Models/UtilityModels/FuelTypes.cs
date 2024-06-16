using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.UtilityModels
{
    public class FuelTypes
    {
        // CarbonInterface API fuel types Entity Frame Work model
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FuelName { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]

        public string FuelUnit { get; set; }

    }
}
