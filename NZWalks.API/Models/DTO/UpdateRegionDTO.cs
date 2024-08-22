using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Max 3 Length")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 Length")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
