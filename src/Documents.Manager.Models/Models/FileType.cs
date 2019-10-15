using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documents.Manager.Models.Interfaces;

namespace Documents.Manager.Models.Models
{
    /// <summary>
    /// File Type.
    /// </summary>
    public class FileType : IBaseEntity
    {
        #region :: Attributes ::

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name the file type
        /// </summary>
        /// <value>The name the file type.</value>
        [MaxLength(250)]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the name the file type
        /// </summary>
        /// <value>The mimeType the file type.</value>
        [MaxLength(250)]
        public string MimeType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>The extension the file type.</value>
        [MaxLength(250)]
        public string Extension { get; private set; }

        /// <summary>
        /// Gets the description the file type
        /// </summary>
        /// <value>The description the file type</value>
        public long MinSize { get; private set; }

        /// <summary>
        /// Gets the description the file type
        /// </summary>
        /// <value>The description the file type</value>
        public long MaxSize { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Status { get; set; }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <value>The files.</value>
        public ICollection<File> Files { get; private set; }

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
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.FileType"/> class.
        /// </summary>
        public FileType() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.FileType"/> class.
        /// </summary>
        public FileType(Guid id, string name, string mimeType, string extension, long minSize, long maxSize, string status)
        {
            Id = id;
            Name = name;
            MimeType = mimeType;
            Extension = extension;
            MinSize = minSize;
            MaxSize = maxSize;
            Status = status;
        }

        #endregion
    }
}
