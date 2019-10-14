namespace Documents.Manager.Service.Models
{
    /// <summary>
    /// Error.
    /// </summary>
    public class Error
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string message { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public string parameter { get; set; }

        #endregion

        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Models.Error"/> class.
        /// </summary>
        public Error()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Models.Error"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <param name="message">Message.</param>
        /// <param name="parameter">Parameter.</param>
        public Error(string code, string message, string parameter)
        {
            this.code = code;
            this.message = message;
            this.parameter = parameter;
        }

        #endregion
    }
}
