using System.Net.Mail;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// An email.
    /// </summary>
    public class Email
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the email "from" address.
        /// </summary>
        /// <value>
        /// The email "from" address.
        /// </value>
        public string EmailFrom { get; set; }

        /// <summary>
        /// Gets or sets the email "to" address.
        /// </summary>
        /// <value>
        /// The email "to" address.
        /// </value>
        public string EmailTo { get; set; }

        /// <summary>
        /// Gets or sets the email "cc" addresses.
        /// </summary>
        /// <value>
        /// The email "cc" address(es).
        /// </value>
        public string[] CC { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email message.
        /// </summary>
        /// <value>
        /// The email message.
        /// </value>
        public string EmailMessage { get; set; }

        /// <summary>
        /// Gets or sets the email attachments.
        /// </summary>
        /// <value>
        /// The email attachments.
        /// </value>
        public Attachment[] EmailAttachments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance's message is HTML.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance's message is HTML; otherwise, <c>false</c>.
        /// </value>
        public bool IsHtml { get; set; }

        #endregion
    }
}
