namespace Documents.Manager.Models.Filters
{
    /// <summary>
    /// Filter the base model.
    /// </summary>
    public abstract class FilterModelBase
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int page { get; set; }

        /// <summary>
        /// Gets or sets the limit of entities to return.
        /// </summary>
        /// <value>The limit.</value>
        public int limit { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Filters.FilterModelBase"/> class.
        /// </summary>
        protected FilterModelBase()
        {
            page = 1;
            limit = 100;
        }

        #endregion
    }
}