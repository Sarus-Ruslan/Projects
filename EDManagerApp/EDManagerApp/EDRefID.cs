using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EDManagerApp
{
    /// <summary>
    /// Идентификаторы ЭC
    /// </summary>
    abstract public class EDRefID
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
        /// Конструктор по умолчанию
        /// </summary>
        public EDRefID()
        {
            m_EDDate = DateTime.Today;
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
        /// Копирование данных из указанного документа (без проверки корректности)
        /// </summary>
        /// <param name="edSource">Документ источник</param>
        protected void CopyFrom(EDRefID edSource)
        {
            this.m_EDNo = edSource.EDNo;
            this.m_EDDate = edSource.EDDate;
            this.m_EDAuthor = edSource.EDAuthor;
        }

        /// <summary>
        /// Формирование xml-строки из данных документа
        /// </summary>
        /// <returns>xml-строка</returns>
        protected string ToXmlStr()
        {
            string xmlStr = $"";
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
        }

    }
}
