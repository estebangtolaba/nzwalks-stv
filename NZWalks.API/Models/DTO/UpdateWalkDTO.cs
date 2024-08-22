using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 length.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Max 1000 length.")]
        public string Description { get; set; }
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
