using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Issue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public required string Details { get; set; }
        public string? ImprovementSuggestion { get; set; }
    }
}
