using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Index(nameof(Word), IsUnique = true)]
    public class Keyword: SimpleBaseEntity
    {
        public required string Word { get; set; }
        public virtual ICollection<Article> Articles { get; set; } = null!;
    }
}
