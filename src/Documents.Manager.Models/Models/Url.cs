using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documents.Manager.Models.Interfaces;
using Documents.Manager.Models.Validations;

namespace Documents.Manager.Models.Models
{
    /// <summary>
    /// Url
    /// </summary>
    public class Url : IBaseEntity
    {
        #region Attributes

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required, NotEmpty]
        public Guid FileId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public File File { get; private set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>The address.</value>
        [Required, Url]
        public string Address { get; private set; }

        /// <summary>
        /// Gets the Evaluation Two.
        /// </summary>
        /// <value>The values.</value>
        [Required]
        public DateTime ExpiresAt { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Documents.Manager.Models.Models.Url"/>
        /// is deleted.
        /// </summary>
        /// <value><c>true</c> if is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>The deleted at.</value>
        public DateTime? DeletedAt { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Models.Url"/> class.
        /// </summary>
        public Url()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Models.Url"/> class.
        /// </summary>
        public Url(Guid id, string address, DateTime expiresAt, Guid fileId)
        {
            Id = id;
            Address = address;
            ExpiresAt = expiresAt;
            FileId = fileId;
        }

        #endregion
    }
}