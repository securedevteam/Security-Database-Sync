using System;

namespace Secure.SecurityDatabaseSync.DAL.Models
{
    /// <summary>
    /// Second model entity.
    /// </summary>
    public class SecondModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier from another system.
        /// </summary>
        public int AnotherSystemId { get; set; }

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
