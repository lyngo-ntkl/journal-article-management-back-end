using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities {
    public class Affiliation: BaseEntity {
        public required int DepartmentId { get; set; }
        public required int InstitutionId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey(nameof(InstitutionId))]
        public virtual Institution Institution { get; set; } = null!;
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Department: SimpleBaseEntity {
        public required string Name { get; set; }
    }

    public class Institution: SimpleBaseEntity {
        public required string Name { get; set; }
        public required int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; } = null!;
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Country: SimpleBaseEntity {
        public required string Name { get; set; }
    }
}