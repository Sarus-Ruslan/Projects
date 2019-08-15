using System;
using System.Linq;
using System.Xml;

namespace EDManagerApp
{
    /// <summary>
    /// Тип запроса
    /// </summary>
    public enum RepeatReceptInqCode
    {
        /// <summary>
        /// Получение электронного сообщения
        /// </summary>
        GetED_1 = 1,
        /// <summary>
        /// Получение электронных сообщений определенного типа
        /// </summary>
        GetEDEx_3 = 3
    }

    /// <summary>
    /// Электронный документ ED542 (Запрос на повторное получение сообщения)
    /// </summary>
    public class ED542 : EDRefID
    {
        /// <summary>
        /// Тип запроса
        /// </summary>
        private RepeatReceptInqCode m_RepeatReceptInqCode;
        /// <summary>
        /// Тип ЭС.
        /// </summary>
        private string m_EDTypeNo;
        /// <summary>
        /// Номер АРМ.
        /// </summary>
        private string m_ARMNo;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ED542()
        {
            m_EDTypeNo = "0542";
            m_ARMNo = null;
            m_RepeatReceptInqCode = RepeatReceptInqCode.GetED_1;
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="eD542">Источник</param>
        public ED542(ED542 eD542)
        {
            this.CopyFrom(eD542);
        }
        /// <summary>
        /// Тип запроса
        /// </summary>
        public RepeatReceptInqCode RepeatReceptInqCode
        {
            get => m_RepeatReceptInqCode;
            set
            {
                if (value != RepeatReceptInqCode.GetED_1 && value != RepeatReceptInqCode.GetEDEx_3)
                {
                    throw new Exception($"Допустимые значения RepeatReceptInqCode: {Convert.ToInt32(RepeatReceptInqCode.GetED_1)} или {Convert.ToInt32(RepeatReceptInqCode.GetEDEx_3)}.");
                }
                else
                {
                    m_RepeatReceptInqCode = value;
                }
            }
        }
        /// <summary>
        /// Тип ЭС
        /// </summary>
        public string EDTypeNo {
            get => m_EDTypeNo;
            set
            {
                if ((value != null) && (value.Length > 100))
                {
                    throw new Exception("Строка EDTypeNo должна содержать не более 100 символов.");
                }
                else
                {
                    m_EDTypeNo = value;
                }
            }
        }
        /// <summary>
        /// Номер АРМ
        /// </summary>
        public string ARMNo
        {
            get => m_ARMNo;
            set
            {
                if (value != null)
                {
                    if (value.Length != 2)
                    {
                        throw new Exception("Строка ARMNo должна содержать не более 2 символов.");
                    }
                    else
                    if (!value.All(c => Char.IsDigit(c)))
                    {
                        throw new Exception("Значение ARMNo должно содержать только цифровые символы.");
                    }
                }
                m_ARMNo = value;
            }
        }

        /// <summary>
        /// Формирование xml-строки из данных документа
        /// </summary>
        /// <returns>xml-строка</returns>
        public new string ToXmlStr()
        {
            string xmlStr = $"<ED542 ";
            xmlStr += base.ToXmlStr();
            xmlStr += $" RepeatReceptInqCode=\"{Convert.ToInt32(this.RepeatReceptInqCode)}\"";
            if (this.EDTypeNo != null)
            {
                xmlStr += $" EDTypeNo=\"{this.EDTypeNo}\"";
            }
            if (this.ARMNo != null)
            {
                xmlStr += $" ARMNo=\"{this.ARMNo}\"";
            }
            xmlStr += $"></ED542>";
            return xmlStr;
        }

        /// <summary>
        /// Загрузка данных документа из xml-строки
        /// </summary>
        /// <param name="xmlStr">xml-строка</param>
        public new void FromXmlStr(string xmlStr)
        {
            base.FromXmlStr(xmlStr);

            var doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xmlStr);

            XmlAttributeCollection attrColl = doc.DocumentElement.Attributes;

            var attr = (XmlAttribute)attrColl.GetNamedItem("RepeatReceptInqCode");
            if (attr != null)
            {
                try
                {
                    RepeatReceptInqCode = (RepeatReceptInqCode)Convert.ToInt32(attr.Value);
                }
                catch (Exception e)
                {
                    throw new Exception("[RepeatReceptInqCode] " + e.Message);
                }
            }
            else
            {
                throw new Exception("[RepeatReceptInqCode] Поле не задано.");
            }

            attr = (XmlAttribute)attrColl.GetNamedItem("EDTypeNo");
            EDTypeNo = attr?.Value;

            attr = (XmlAttribute)attrColl.GetNamedItem("ARMNo");
            ARMNo = attr?.Value;

            this.Validate();
        }

        /// <summary>
        /// Проверка корректности данных документа
        /// </summary>
        /// <returns>Флаг корректности</returns>
        public void Validate()
        {
            if (this.RepeatReceptInqCode == RepeatReceptInqCode.GetED_1)
            { // для типа запроса 1 должен быть заполнен идентификатор ЕС
                if (this.EDNo == null || this.EDDate == null || this.EDAuthor == null)
                {
                    throw new Exception("Данные в документе не корректны. Для типа запроса 1 должны быть заполнены все поля идентификатора ЕС [EDNo,EDDate,EDAuthor].");
                }
            }
            else
            if (this.RepeatReceptInqCode == RepeatReceptInqCode.GetEDEx_3)
            { // для типа запроса 3 должен быть заполнен Тип ЭС
                if (this.EDTypeNo == null)
                {
                    throw new Exception("Данные в документе не корректны. Для типа запроса 3 должн быть заполнен тип ЕС [EDTypeNo].");
                }
            }
        }
        /// <summary>
        /// Копирование данных из указанного документа (без проверки корректности)
        /// </summary>
        /// <param name="edSource">Документ источник</param>
        public void CopyFrom(ED542 edSource)
        {
            base.CopyFrom(edSource);
            this.m_RepeatReceptInqCode = edSource.RepeatReceptInqCode;
            this.m_EDTypeNo = edSource.EDTypeNo;
            this.m_ARMNo = edSource.ARMNo;
        }
    }
}