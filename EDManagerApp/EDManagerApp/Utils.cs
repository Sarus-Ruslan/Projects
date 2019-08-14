using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDManagerApp
{
    /// <summary>
    /// Вспомогательные утилиты
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Вывод окна информационного сообщения
        /// </summary>
        /// <param name="message">Строка информации</param>
        /// <returns></returns>
        public static DialogResult InfoMessageBox(string message)
        {
            return MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Вывод окна сообщения об ошибке
        /// </summary>
        /// <param name="message">Строка ошибки</param>
        /// <returns></returns>
        public static DialogResult ErrorMessageBox(string message)
        {
            return MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
