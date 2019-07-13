using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityDatabaseSync.DAL.Models
{
    /// <summary>
    /// Тестовая модель.
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// Уникальный код.
        /// </summary>
        [Key]
        public string Code { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата и время.
        /// </summary>
        public DateTime Current { get; set; }
    }
}
