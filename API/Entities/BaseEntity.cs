using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    public abstract class BaseEntity {
        // TODO: auto-update date time
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public int CreatedBy {get; set;}
        public DateTime CreatedDate {get; set;}
        public int? UpdatedBy {get; set;}
        public DateTime UpdatedDate {get; set;}
        public bool IsDeleted {get; set;}
        public DateTime DeletedDate {get; set;}
    }
}