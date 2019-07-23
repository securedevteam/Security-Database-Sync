using System;

namespace SecurityDatabaseSync.Core
{
    /// <summary>
    /// Основные статические методы проекта.
    /// </summary>
    public static class StaticMethods
    {
        /// <summary>
        /// Отображение результата операции на консоли.
        /// </summary>
        /// <param name="result">результат операции.</param>
        public static void OperationResult(bool result)
        {
            if (result)
            {
                Console.WriteLine(">> Операция выполнена!\n");
            }
            else
            {
                Console.WriteLine(">> Операция не выполнена!\n");
            }
        }
    }
}
