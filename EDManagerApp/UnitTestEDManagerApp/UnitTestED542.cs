using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDManagerApp;

namespace UnitTestEDManagerApp
{
    /// <summary>
    /// Тестирование документа ED542
    /// </summary>
    [TestClass]
    public class UnitTestED542
    {
        /// <summary>
        /// Тест поля EDNo
        /// </summary>
        [TestMethod]
        public void TestMethodED_EDNo()
        {
            var testED542 = new ED542();
            Assert.ThrowsException<Exception>(() => (testED542.EDNo = -1), "Значение должно быть больше нуля.");
            Assert.ThrowsException<Exception>(() => (testED542.EDNo = 1000000000), "Значение должно быть меньше 9 разрядов.");
        }

        /// <summary>
        /// Тест поля EDAuthor
        /// </summary>
        [TestMethod]
        public void TestMethodED_EDAuthor()
        {
            var testED542 = new ED542();
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = ""), "Значение не должно быть пустой строкой");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "1"), "Значение не должно быть меньше 10 символов");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "01234567890"), "Значение не должно быть больше 10 символов");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "a123456789"), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "1 2 456789"), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.EDAuthor = "+124567890"), "Значение должно содержать только цифровые символы");
        }

        /// <summary>
        /// Тест поля RepeatReceptInqCode
        /// </summary>
        [TestMethod]
        public void TestMethodED_RepeatReceptInqCode()
        {
            var testED542 = new ED542();
            Assert.ThrowsException<Exception>(() => (testED542.RepeatReceptInqCode = 0), "Значение должно содержать только значения из перечисления");
        }

        /// <summary>
        /// Тест поля EDTypeNo
        /// </summary>
        [TestMethod]
        public void TestMethodED_EDTypeNo()
        {
            var testED542 = new ED542();
            string s101 = "1";
            Assert.ThrowsException<Exception>(() => (testED542.EDTypeNo = s101.PadRight(101, '2')), "Значение должно содержать менее 100 символов");
        }

        /// <summary>
        /// Тест поля ARMNo
        /// </summary>
        [TestMethod]
        public void TestMethodED_ARMNo()
        {
            var testED542 = new ED542();
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = ""), "Значение не должно быть пустой строкой");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = "1"), "Значение не должно быть меньше 2 символов");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = "123"), "Значение не должно быть более 2 символов");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = "1 3"), "Значение не должно быть более 2 символов");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = "1 "), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = " 1"), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = "a1"), "Значение должно содержать только цифровые символы");
            Assert.ThrowsException<Exception>(() => (testED542.ARMNo = ".1"), "Значение должно содержать только цифровые символы");
        }
    }
}
