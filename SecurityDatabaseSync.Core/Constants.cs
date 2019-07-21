namespace SecurityDatabaseSync.Core
{
    public class Constants
    {
        public const string COMMAND_PROGRAM = "-hard, -hard-bulk, -default, -default-bulk, -quit";

        public const string COMMAND_DEFAULT = "-add, -delete, -update, -exit";

        public const string COMMAND_HARD = "-insert, -transfer, -clear, -clear-ident, -exit";

        public const string ENTER_SYNC_TYPE = "Введите тип синхронизации: ";

        public const string ENTER_COMMAND = "Введите команду: ";

        public const string ENTER_DATABASE_IDENTIFIER = "Введите идентификатор: ";

        public const string ENTER_DATABASE_EXPORT = "Введите название базы данных для экспорта: ";

        public const string ENTER_DATABASE_IMPORT = "Введите название базы данных для импорта: ";

        public const string ENTER_DATABASE_TARGET = "Введите название целевой базы данных: ";

        public const string ENTER_DATABASE_NAME = "Введите название базы данных: ";

        public const string INVALID_COMMAND = ">> Введена неверная команда!\n";
    }
}
