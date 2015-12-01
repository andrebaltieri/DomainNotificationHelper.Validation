using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainNotificationHelper.Validation.Tests
{
    [TestClass]
    public class AssertionConcernTests
    {
        [TestMethod]
        [TestCategory("AssertRegexMatch")]
        public void AssertRegexIsValid()
        {
            var emailRegex =
               @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var res = AssertionConcern.AssertRegexMatch("andrebaltieri@hotmail.com", emailRegex, "E-mail inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertRegexMatch")]
        public void AssertRegexIsInvalid()
        {
            var emailRegex =
               @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var res = AssertionConcern.AssertRegexMatch("andrebaltieri[at]hotmail[dot]com", emailRegex, "E-mail inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertEmailIsValid")]
        public void AssertEmailIsValid()
        {
            var res = AssertionConcern.AssertEmailIsValid("andrebaltieri@hotmail.com", "E-mail inválido");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertEmailIsValid")]
        public void AssertEmailIsInvalid()
        {
            var res = AssertionConcern.AssertEmailIsValid("andrebaltieri[at]hotmail[dot]com", "E-mail inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertUrlIsValid")]
        public void AssertUrlIsValid()
        {
            var res = AssertionConcern.AssertUrlIsValid("http://andrebaltieri.net/", "URL inválida");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertUrlIsValid")]
        public void AssertUrlIsInvalid()
        {
            var res = AssertionConcern.AssertUrlIsValid("agá tê tê pê dois pontos barra barra andrebaltieri.net", "URL inválido");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertIsNull")]
        public void AssertIsNull()
        {
            var res = AssertionConcern.AssertIsNull(DateTime.Now, "Well... it is not null!");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertCPFIsValid")]
        public void AssertCpfIsValid()
        {
            var cpf = "943.754.516-29";
            var res = AssertionConcern.AssertCPFIsValid(cpf, "CPF inválido.");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertCPFIsValid")]
        public void AssertCpfIsInvalid()
        {
            var cpf = "943.754.516-54";
            var res = AssertionConcern.AssertCPFIsValid(cpf, "CPF inválido.");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertCPFIsValid")]
        public void AssertCpfIsInvalidWhenRepeatDigit()
        {
            var cpf = "11111111111";
            var res = AssertionConcern.AssertCPFIsValid(cpf, "CPF inválido.");
            Assert.IsNotNull(res);
        }
    }
}