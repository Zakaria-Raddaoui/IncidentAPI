using System.ComponentModel.DataAnnotations;
namespace IncidentAPI.Models
{
    public class Incident
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Titre Invalide!")]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Description Invalide!")]
        public string Description { get; set; } = null!;
        [Required]
        public string Severity { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
