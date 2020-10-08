

namespace CustomExceptions
{
    /// <summary>
    /// Helper class for custom exception related methods
    /// </summary>
    public static class CustomExceptionsHelper
    {
        /// <summary>
        /// Error Code and Message Separator
        /// </summary>
        public const char ErrorCodeAndMessageSeparator = '|';

        /// <summary>
        /// Returns formatted message for handled exceptions
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="message">error message</param>
        /// <returns>formatted message</returns>
        public static string GetHandledMessage(string errorCode, string message)
        {
            return string.Format("{0}{1}{2}", errorCode, ErrorCodeAndMessageSeparator, message);
        }
    }
}
