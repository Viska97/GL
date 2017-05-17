using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Лабиринт
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "gl.db");
            string deleteName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "deleteme.txt");
            if (File.Exists(deleteName))
            {
                try
                {
                    File.Delete(databaseName);
                    File.Delete(deleteName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить базу данных!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete(deleteName);
                }
                
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Account());
        }
    }
}
