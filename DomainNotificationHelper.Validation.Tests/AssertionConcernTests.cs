using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainNotificationHelper.Validation.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AssertRegexIsValid()
        {
            var emailRegex =
               @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var res = AssertionConcern.AssertRegexMatch("andrebaltieri@hotmail.com", emailRegex, "E-mail inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        public void AssertRegexIsInvalid()
        {
            var emailRegex =
               @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var res = AssertionConcern.AssertRegexMatch("andrebaltieri[at]hotmail[dot]com", emailRegex, "E-mail inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertEmailIsValid()
        {
            var res = AssertionConcern.AssertEmailIsValid("andrebaltieri@hotmail.com", "E-mail inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        public void AssertEmailIsInvalid()
        {
            var res = AssertionConcern.AssertEmailIsValid("andrebaltieri[at]hotmail[dot]com", "E-mail inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertUrlIsValid()
        {
            var res = AssertionConcern.AssertUrlIsValid("http://andrebaltieri.net/", "URL inválida");
            Assert.IsNull(res);
        }

        [TestMethod]
        public void AssertUrlIsInvalid()
        {
            var res = AssertionConcern.AssertUrlIsValid("agá tê tê pê dois pontos barra barra andrebaltieri.net", "URL inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertIsNull()
        {
            var res = AssertionConcern.AssertIsNull(DateTime.Now, "Well... it is not null!");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertLengthIsValid()
        {
            var res = AssertionConcern.AssertLength("It is a String", 0, 14, "Fill a [0-14] string length");
            Assert.IsNull(res);
        }

        [TestMethod]
        public void AssertLengthIsInvalid()
        {
            var res = AssertionConcern.AssertLength("It is a String", 0, 3, "Fill a [0-3] string length");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertNotEmptyIsValid()
        {
            var res = AssertionConcern.AssertNotEmpty("It is not empty...", "Wrong answer!! It is empty.");
            Assert.IsNull(res);
        }

        [TestMethod]
        public void AssertNotEmptyIsInvalid()
        {
            var res = AssertionConcern.AssertNotEmpty(String.Empty, "Wrong answer!! It is empty.");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertTrueIsValid()
        {
            var value = true;
            var res = AssertionConcern.AssertTrue(value, "Well, it is not a true value.");
            Assert.IsNull(res);
        }

        [TestMethod]
        public void AssertTrueIsInvalid()
        {
            var value = false;
            var res = AssertionConcern.AssertTrue(value, "Well, it is not a true value.");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AssertIsGreaterThanIsValid()
        {
            var stringRes = AssertionConcern.AssertIsGreaterThan(2, 1, "Wrong! It is not a greater value.");
            var dateTimeRes = AssertionConcern.AssertIsGreaterThan(DateTime.Now.AddMinutes(1), DateTime.Now, "Wrong! It is not a greater value.");

            Assert.IsNull(stringRes);
            Assert.IsNull(dateTimeRes);
        }

        [TestMethod]
        public void AssertIsGreaterThanIsInvalid()
        {
            var stringRes = AssertionConcern.AssertIsGreaterThan(1, 2, "Wrong! It is not a greater value.");
            var dateTimeRes = AssertionConcern.AssertIsGreaterThan(DateTime.Now, DateTime.Now.AddMinutes(1), "Wrong! It is not a greater value.");

            Assert.IsNotNull(stringRes);
            Assert.IsNotNull(dateTimeRes);
        }

        [TestMethod]
        public void AssertIsBetweenIsValid()
        {
            var dateTimeRes = AssertionConcern.AssertIsBetween(DateTime.Now, DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1), "Wrong! It is not a between value.");
            var intRes = AssertionConcern.AssertIsBetween(2, 1, 3, "Wrong! It is not a between value.");

            Assert.IsNull(dateTimeRes);
            Assert.IsNull(intRes);
        }

        [TestMethod]
        public void AssertIsBetweenIsInvalid()
        {
            var dateTimeRes = AssertionConcern.AssertIsBetween(DateTime.Now, DateTime.Now.AddMinutes(1), DateTime.Now.AddMinutes(-1), "Wrong! It is not a between value.");
            var intRes = AssertionConcern.AssertIsBetween(1, 2, 3, "Wrong! It is not a between value.");

            Assert.IsNotNull(dateTimeRes);
            Assert.IsNotNull(intRes);
        }
    }
}