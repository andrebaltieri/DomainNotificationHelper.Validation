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
        public void AssertIsNotEmptyIfConditionTrue()
        {
            bool active = false;

            var res = AssertionConcern.AssertIsNotEmptyIfConditionTrue("Why this is inactive?", !active, "Please, tell me why this is inactive.");
            Assert.IsNull(res);
        }
    }
}