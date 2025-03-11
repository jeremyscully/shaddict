using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaddict.Models
{
    /// <summary>
    /// Represents a business entity in the data dictionary
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Primary key for the entity
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the entity
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description of the entity
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for tables associated with this entity
        /// </summary>
        public virtual ICollection<DatabaseTable> Tables { get; set; } = new List<DatabaseTable>();
    }
}
