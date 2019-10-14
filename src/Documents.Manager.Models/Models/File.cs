using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documents.Manager.Models.Interfaces;
using Documents.Manager.Models.Validations;

namespace Documents.Manager.Models.Models
{
    /// <summary>
    /// File information.
    /// </summary>
    public class File : IBaseEntity
    {
        #region :: Properties ::

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
        public Guid OwnerId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required, NotEmpty]
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        [MaxLength(250)]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the S3 identity.
        /// </summary>
        /// <value>The etag s3.</value>
        [MaxLength(250)]
        public string ETag { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version</value>
        [MaxLength(250)]
        public string Version { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required, NotEmpty]
        public Guid FileTypeId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public FileType FileType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required, NotEmpty]
        public Guid ProcessId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Process Process { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required, NotEmpty]
        public Guid FileClassificationId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public FileClassification FileClassification { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [Required, NotEmpty]
        public Guid ApplicationId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Application Application { get; private set; }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The files.</value>
        public ICollection<Url> Urls { get; private set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Documents.Manager.Models.File"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        #endregion

        #region :: Constructors ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.File"/> class.
        /// </summary>
        public File() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.File"/> class.
        /// </summary>
        

        #endregion
    }
}
