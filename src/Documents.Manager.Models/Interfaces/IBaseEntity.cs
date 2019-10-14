using System;
namespace Documents.Manager.Models.Interfaces
{
    /// <summary>
    /// Base entity.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Onboarding.Catalogs.Models.Interfaces.BaseEntity"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if is deleted; otherwise, <c>false</c>.</value>
        bool IsDeleted { get; }

    }
}
