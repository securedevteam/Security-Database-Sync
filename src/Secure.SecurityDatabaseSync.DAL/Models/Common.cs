using System;

namespace Secure.SecurityDatabaseSync.DAL.Models
{
    /// <summary>
    /// Common entity.
    /// </summary>
    public class Common
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Internal identifier.
        /// </summary>
        public string InternalNumber { get; set; }

        /// <summary>
        /// Identifier of system.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Updated time.
        /// </summary>
        public DateTime Updated { get; set; }
    }
}
