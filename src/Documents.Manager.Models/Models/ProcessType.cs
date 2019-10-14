using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documents.Manager.Models.Interfaces;

namespace Documents.Manager.Models.Models
{
    /// <summary>
    /// Process Type.
    /// </summary>
    public class ProcessType : IBaseEntity
    {
        #region :: Attributes ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name the process type
        /// </summary>
        /// <value>The name the process type.</value>
        [MaxLength(250)]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description the process type
        /// </summary>
        /// <value>The description the process type</value>
        [MaxLength(250)]
        public string Description { get; private set; }

        /// <summary>
        /// Gets the process.
        /// </summary>
        /// <value>The process.</value>
        public ICollection<Process> Processes { get; private set; }

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

        #endregion

        #region :: Constructors ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Models.ProcessType"/> class.
        /// </summary>
        public ProcessType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Models.ProcessType"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public ProcessType(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        #endregion
    }
}
