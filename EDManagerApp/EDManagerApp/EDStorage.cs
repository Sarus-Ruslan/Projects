using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EDManagerApp
{
    /// <summary>
    /// Хранилище документов
    /// </summary>
    class EDStorage
    {
        /// <summary>
        /// Внутреннее хранилище документов
        /// </summary>
        private BindingList<ED542> edDataSet = new BindingList<ED542>();
        /// <summary>
        /// Путь к файлу хранилищю документов
        /// </summary>
        private string storagePath;
        /// <summary>
        /// Диалог открытия файла хранилища
        /// </summary>
        private OpenFileDialog storageFileDialog;

        /// <summary>
        /// Хранилище документов
        /// </summary>
        public EDStorage()
        {
            this.storageFileDialog = new System.Windows.Forms.OpenFileDialog();
            // 
            // storageFileDialog
            // 
            this.storageFileDialog.Filter = "Text files(*.xml)|*.xml|All files(*.*)|*.*";
            this.storageFileDialog.Title = "Открытие файла-хранилища документов";
        }

        /// <summary>
        /// Данные хранилища
        /// </summary>
        public BindingList<ED542> EdDataSet { get => edDataSet; set => edDataSet = value; }

        /// <summary>
        /// Загрузка данных из внешнего источника
        /// </summary>
        /// <returns>Список загруженных документов</returns>
        public List<ED542> Load()
        {
            List<ED542> newList = new List<ED542>();

            if (this.storageFileDialog.ShowDialog() == DialogResult.OK)
            {
                Logger.WriteLine($"Загрузка хранилища из файла {this.storageFileDialog.FileName}...");
                this.storagePath = this.storageFileDialog.FileName;

                var doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                try
                {
                    // читаем файл в строку
                    string fileText = File.ReadAllText(this.storagePath);
                    doc.LoadXml(fileText);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Ошибка загрузки хранилища документов:\r\n" + ex.Message);
                    Utils.ErrorMessageBox("Ошибка загрузки хранилища документов:\r\n" + ex.Message);
                    return newList;
                }

                string sError = "";
                XmlElement xRoot = doc.DocumentElement;
                foreach (XmlNode xnode in xRoot)
                {
                    if (xnode.Name == "ED542")
                    {
                        var newED542 = new ED542();
                        try
                        {
                            newED542.FromXmlStr(xnode.OuterXml);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine("Ошибка загрузки документа из хранилища:\r\n" + ex.Message);
                            sError += $"{ex.Message}\r\n";
                            // пропускаем узел
                            newED542 = null;
                        }
                        if (newED542 != null)
                        {
                            newList.Add(newED542);
                            Logger.WriteLine($"Загружен документ: {newED542.ToXmlStr()}");
                        }
                    }
                }

                Logger.WriteLine($"Хранилище загружено{(sError.Length>0?" с ошибками":"")}. Документов: {newList.Count}.");
                Utils.InfoMessageBox($"Хранилище загружено{(sError.Length > 0 ? " с ошибками" : "")}.\r\nДокументов: {newList.Count}.");
            }
            return newList;
        }

        /// <summary>
        /// Сохранить данные хранилища
        /// </summary>
        public void Save()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Выбирите файл хранилище";
            saveFileDialog.Filter = "Text files(*.xml)|*.xml|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Logger.WriteLine($"Сохранение хранилища в файл: {saveFileDialog.FileName}...");
                this.storagePath = saveFileDialog.FileName;

                string xmlStorageStr = "<?xml version=\"1.0\" encoding=\"windows - 1251\"?>\r\n";
                xmlStorageStr += "<ED>\r\n";
                foreach (var ed in EdDataSet)
                {
                    xmlStorageStr += $"{ed.ToXmlStr()}\r\n";
                }
                xmlStorageStr += "</ED>";
                File.WriteAllText(this.storagePath, xmlStorageStr);
                Logger.WriteLine($"Хранилище сохранено. Документов: {this.EdDataSet.Count}");
                Utils.InfoMessageBox($"Хранилище сохранено.\r\nДокументов: {this.EdDataSet.Count}");
            }
        }
    }
}
