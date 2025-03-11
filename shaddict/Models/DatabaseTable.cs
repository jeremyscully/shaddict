using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaddict.Models
{
    /// <summary>
    /// Represents a database table in the data dictionary
    /// </summary>
    public class DatabaseTable
    {
        /// <summary>
        /// Primary key for the table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Schema name of the table
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Schema { get; set; } = string.Empty;

        /// <summary>
        /// Name of the table
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the table (schema.name)
        /// </summary>
        [NotMapped]
        public string FullName => $"{Schema}.{Name}";

        /// <summary>
        /// Description of the table
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Date when the table was created in the data dictionary
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Foreign key to the entity
        /// </summary>
        public int? EntityId { get; set; }

        /// <summary>
        /// Navigation property for the entity
        /// </summary>
        public virtual Entity? Entity { get; set; }

        /// <summary>
        /// Navigation property for columns in this table
        /// </summary>
        public virtual ICollection<TableColumn> Columns { get; set; } = new List<TableColumn>();
    }
}
