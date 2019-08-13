using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace EDManagerApp
{
    /// <summary>
    /// Проверка валидности xml-документа
    /// </summary>
    public class XmlValidator
    {
        /// <summary>
        /// Флаг валидности документа
        /// </summary>
        private bool isValid = false;

        /// <summary>
        /// Строка ошибки валидации
        /// </summary>
        private string validError = "";

        /// <summary>
        /// Получаем флаг валидности xml-документа
        /// </summary>
        public bool IsValid { get => isValid; }

        /// <summary>
        /// Установка/Получение ошибки валидации
        /// </summary>
        public string ValidError { get => validError; set => validError = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public XmlValidator()
        {
        }

        /// <summary>
        /// Валидация xml-документа 
        /// </summary>
        /// <param name="xml">xml-строка</param>
        /// <param name="schemaUri">Путь к файлу xsd-схемы</param>
        /// <returns>Результат валидации</returns>
        public bool ValidateXmlDoc(string xml, string schemaUri)
        {
            this.ValidError = "";
            if (!String.IsNullOrWhiteSpace(xml))
            {
                try
                {
                    var srXml = new StringReader(xml);
                    return ValidateXmlDoc(srXml, schemaUri);
                }
                catch (Exception ex)
                {
                    this.ValidError = ex.Message;
                }
            }
            return false;
        }

        /// <summary>
        /// Валидация xml-документа 
        /// </summary>
        /// <param name="xml">текстовый xml-поток</param>
        /// <param name="xsdSchemaUri">Путь к файлу xsd-схемы</param>
        /// <returns></returns>
        public bool ValidateXmlDoc(StringReader xml, string xsdSchemaUri)
        {
            this.ValidError = "";
            if (xml == null || String.IsNullOrWhiteSpace(xsdSchemaUri))
            {
                return false;
            }

            this.isValid = true;

            try
            {
                var settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsdSchemaUri);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                XmlReader rdr = XmlReader.Create(xml, settings);

                while (rdr.Read())
                {
                }

                rdr.Close();

                return this.isValid;
            }
            catch (Exception ex)
            {
                this.ValidError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Обработка ошибки валидации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            // Произошла ошибка валидации
            this.isValid = false;
            this.ValidError = args.Message;
        }
    }
}
