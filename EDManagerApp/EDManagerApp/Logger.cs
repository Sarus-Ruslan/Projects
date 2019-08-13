using System;
using System.IO;
using System.Reflection;

namespace EDManagerApp
{
    /// <summary>
    /// Лог файл
    /// </summary>
    static class Logger
    {
        /// <summary>
        /// Запись в лог строки
        /// </summary>
        /// <param name="message">Строка</param>
        public static void Write(string message)
        {
            using (var sw = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\log.txt", true))
            {
                sw.Write(message);
                Console.Write(message);
            }
        }

        /// <summary>
        /// Запись в лог строки с признаком конца строки
        /// </summary>
        /// <param name="message">Строка</param>
        public static void WriteLine(string message)
        {
            using (var sw = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\log.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString()}:   {message}");
                Console.WriteLine(message);
            }
        }
    }
}
