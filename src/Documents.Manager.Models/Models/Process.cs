using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documents.Manager.Models.Interfaces;
using Documents.Manager.Models.Validations;

namespace Documents.Manager.Models.Models
{
    /// <summary>
    /// Process
    /// </summary>
    public class Process : IBaseEntity
    {
        #region :: Attributes ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name Process
        /// </summary>
        /// <value>The Name Process.</value>
        [MaxLength(250)]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description Process
        /// </summary>
        /// <value>The Description Process.</value>
        [MaxLength(250)]
        public string Description { get; private set; }

        /// <summary>
        /// Gets the description Process
        /// </summary>
        /// <value>The Description Process.</value>
        [MaxLength(250)]
        public string NameBucket { get; private set; }

        /// <summary>
        /// Gets or private sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets a value indicating whether this is deleted.
        /// </summary>
        /// <value><c>true</c> if is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>The deleted at.</value>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        [Required, NotEmpty]
        public Guid ProcessTypeId { get; private set; }

        /// <summary>
        /// Gets the Process Type.
        /// </summary>
        /// <value>The Process Type.</value>
        public ProcessType ProcessType { get; private set; }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The files.</value>
        public ICollection<File> Files { get; private set; }

        #endregion

        #region :: Constructors ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Process"/> class.
        /// </summary>
        public Process()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Process"/> class.
        /// </summary>

        #endregion
    }
}
