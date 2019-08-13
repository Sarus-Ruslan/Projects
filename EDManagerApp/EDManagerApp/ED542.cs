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
    public class ED542
    {
        /// <summary>
        /// Номер ЭС в течение опердня
        /// </summary>
        private int? m_EDNo;
        /// <summary>
        /// Дата составления ЭС
        /// </summary>
        private DateTime? m_EDDate;
        /// <summary>
        /// Уникальный идентификатор составителя ЭС - УИС
        /// </summary>
        private string m_EDAuthor;
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
            m_EDDate = DateTime.Today;
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
        /// Номер ЭС в течение опердня
        /// </summary>
        public int? EDNo
        {
            get => m_EDNo;
            set
            {
                if (value != null)
                {
                    if (value < 0)
                    {
                        throw new Exception("Значение EDNo должно быть больше 0.");
                    }
                    else
                    if (value > 999999999)
                    {
                        throw new Exception("Значение EDNo должно содержать менее 10 разрядов.");
                    }
                }
                m_EDNo = value;
            }
        }
        /// <summary>
        /// Дата составления ЭС
        /// </summary>
        public DateTime? EDDate
        {
            get => m_EDDate;
            set => m_EDDate = value;
        }
        /// <summary>
        /// Уникальный идентификатор составителя ЭС - УИС
        /// </summary>
        public string EDAuthor
        {
            get => m_EDAuthor;
            set
            {
                if (value != null)
                {
                    if (value.Length != 10)
                    {
                        throw new Exception("Значение EDAuthor должно содержать 10 знаков.");
                    }
                    else
                    if (!value.All(c => Char.IsDigit(c)))
                    {
                        throw new Exception("Значение EDAuthor должно содержать только цифровые символы.");
                    }
                }
                m_EDAuthor = value;
            }
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
                if (value != null && value.Length > 100)
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
        public string ToXmlStr()
        {
            string xmlStr = $"<ED542 ";
            if (this.EDNo != null)
            {
                xmlStr += $" EDNo=\"{this.EDNo}\"";
            }
            if (this.EDDate != null)
            {
                xmlStr += $" EDDate=\"{String.Format("{0:yyyy-MM-dd}", this.EDDate)}\"";
            }
            if (this.EDAuthor != null)
            {
                xmlStr += $" EDAuthor=\"{this.EDAuthor}\"";
            }
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
        public void FromXmlStr(string xmlStr)
        {
            var doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xmlStr);

            XmlAttributeCollection attrColl = doc.DocumentElement.Attributes;

            var attr = (XmlAttribute)attrColl.GetNamedItem("EDNo");
            if (attr != null)
            {
                try
                {
                    EDNo = Convert.ToInt32(attr.Value);
                }
                catch (Exception e)
                {
                    throw new Exception("[EDNo] " + e.Message);
                }
            }
            else
            {
                EDNo = null;
            }

            attr = (XmlAttribute)attrColl.GetNamedItem("EDDate");
            if (attr != null)
            {
                try
                {
                    EDDate = Convert.ToDateTime(attr.Value);
                }
                catch (Exception e)
                {
                    throw new Exception("[EDDate] " + e.Message);
                }
            }
            else
            {
                EDDate = null;
            }

            attr = (XmlAttribute)attrColl.GetNamedItem("EDAuthor");
            EDAuthor = attr?.Value;

            attr = (XmlAttribute)attrColl.GetNamedItem("RepeatReceptInqCode");
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
        /// <param name="eD542">Документ источник</param>
        public void CopyFrom(ED542 eD542)
        {
            this.m_EDNo = eD542.EDNo;
            this.m_EDDate = eD542.EDDate;
            this.m_EDAuthor = eD542.EDAuthor;
            this.m_RepeatReceptInqCode = eD542.RepeatReceptInqCode;
            this.m_EDTypeNo = eD542.EDTypeNo;
            this.m_ARMNo = eD542.ARMNo;
        }
    }
}