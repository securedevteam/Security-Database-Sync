namespace SecurityDatabaseSync.Core
{
    /// <summary>
    /// Основные константы проекта.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Команды класса Program.cs.
        /// </summary>
        public const string COMMAND_PROGRAM = "-hard, -hard-bulk, -default, -default-bulk, -quit";

        /// <summary>
        /// Команды для обычной синхронизации.
        /// </summary>
        public const string COMMAND_DEFAULT = "-add, -delete, -update, -exit";

        /// <summary>
        /// Команды для жесткой синхронизации.
        /// </summary>
        public const string COMMAND_HARD = "-insert, -transfer, -clear, -clear-ident, -exit";

        /// <summary>
        /// Сообщение о типе синхронизации.
        /// </summary>
        public const string ENTER_SYNC_TYPE = "Введите тип синхронизации: ";

        /// <summary>
        /// Сообщение о вводе команды.
        /// </summary>
        public const string ENTER_COMMAND = "Введите команду: ";

        /// <summary>
        /// Сообщение о вводе идентификатора.
        /// </summary>
        public const string ENTER_DATABASE_IDENTIFIER = "Введите идентификатор: ";

        /// <summary>
        /// Сообщение о базе данных для экспорта.
        /// </summary>
        public const string ENTER_DATABASE_EXPORT = "Введите название базы данных для экспорта: ";

        /// <summary>
        /// Сообщение о базе данных для импорта.
        /// </summary>
        public const string ENTER_DATABASE_IMPORT = "Введите название базы данных для импорта: ";

        /// <summary>
        /// Сообщение о целевой базе данных.
        /// </summary>
        public const string ENTER_DATABASE_TARGET = "Введите название целевой базы данных: ";

        /// <summary>
        /// Сообщение о вводе названия базы данных.
        /// </summary>
        public const string ENTER_DATABASE_NAME = "Введите название базы данных: ";

        /// <summary>
        /// Сообщение о неверной команде.
        /// </summary>
        public const string INVALID_COMMAND = ">> Введена неверная команда!\n";
    }
}
