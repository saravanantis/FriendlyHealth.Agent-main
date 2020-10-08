namespace CustomExceptions
{
    using System;

    /// <summary>
    /// Based class for all handled application exceptions, 
    /// which will contain error code and custom message to be displayed to the end user
    /// </summary>
    public class HandledApplicationException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandledApplicationException"/> class.
        /// </summary>
        /// <param name="errorCode">Error code, to be sent to browser</param>
        /// <param name="customErrorMessage">Custom error message, to be sent to browser</param>
        public HandledApplicationException(string errorCode, string customErrorMessage) : base()
        {
            this.ErrorCode = errorCode;
            this.CustomErrorMessage = customErrorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledApplicationException"/> class.
        /// </summary>
        /// <param name="errorCode">Error code, to be sent to browser</param>
        /// <param name="customErrorMessage">Custom error message, to be sent to browser</param>
        /// <param name="originalMessage">original exception message, if any</param>
        /// <param name="inner">original exception, if any</param>
        public HandledApplicationException(string errorCode, string customErrorMessage, string originalMessage, Exception inner)
            : base(originalMessage, inner)
        {
            this.ErrorCode = errorCode;
            this.CustomErrorMessage = customErrorMessage;
        }

        /// <summary>
        /// Gets the Error code, to be sent to browser.
        /// </summary>
        /// <value>
        /// The Error code, to be sent to browser.
        /// </value>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// Gets the Custom error message, to be sent to browser.
        /// </summary>
        /// <value>
        /// The Custom error message, to be sent to browser.
        /// </value>
        public string CustomErrorMessage { get; private set; }
    }
}
