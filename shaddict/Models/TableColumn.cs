using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaddict.Models
{
    /// <summary>
    /// Represents a column in a database table
    /// </summary>
    public class TableColumn
    {
        /// <summary>
        /// Primary key for the column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the column
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Data type of the column
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DataType { get; set; } = string.Empty;

        /// <summary>
        /// Maximum length for string data types
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Precision for numeric data types
        /// </summary>
        public int? Precision { get; set; }

        /// <summary>
        /// Scale for numeric data types
        /// </summary>
        public int? Scale { get; set; }

        /// <summary>
        /// Whether the column allows null values
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Whether the column is a primary key
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Whether the column is a foreign key
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Default value for the column
        /// </summary>
        [StringLength(200)]
        public string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Ordinal position of the column in the table
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Description of the column
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Example values for the column
        /// </summary>
        [StringLength(200)]
        public string ExampleValues { get; set; } = string.Empty;

        /// <summary>
        /// Formula or computation for derived columns
        /// </summary>
        [StringLength(500)]
        public string ComputationFormula { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to the table
        /// </summary>
        public int TableId { get; set; }

        /// <summary>
        /// Navigation property for the table
        /// </summary>
        public virtual DatabaseTable Table { get; set; } = null!;
    }
}
