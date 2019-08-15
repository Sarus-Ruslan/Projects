using System;
using EDManagerApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEDManagerApp
{
    /// <summary>
    /// Тестирование Идентификатора ED
    /// </summary>
    [TestClass]
    public class UnitTestEDRefID
    {
        /// <summary>
        /// Тест поля EDNo
        /// </summary>
        [TestMethod]
        public void TestMethodSet_EDNo()
        {
            var testED542 = new ED542();
            Assert.ThrowsException<Exception>(() => (testED542.EDNo = -1), "Значение должно быть больше нуля.");
            Assert.ThrowsException<Exception>(() => (testED542.EDNo = 1000000000), "Значение должно быть меньше 9 разрядов.");
        }

        /// <summary>
        /// Тест поля EDAuthor
        /// </summary>
        [TestMethod]
        public void TestMethodSet_EDAuthor()
        {
            var testED542 = new ED542();
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = ""), "Значение не должно быть пустой строкой");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "1"), "Значение не должно быть меньше 10 символов");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "01234567890"), "Значение не должно быть больше 10 символов");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "a123456789"), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "1 2 456789"), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "+124567890"), "Значение должно содержать только цифровые символы");
        }

    }
}
