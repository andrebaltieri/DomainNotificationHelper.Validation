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
            var res = AssertionConcern.AssertCpfIsValid(cpf, "CPF inválido.");
            Assert.IsNull(res);
        }

        [TestMethod]
        [TestCategory("AssertCPFIsValid")]
        public void AssertCpfIsInvalid()
        {
            var cpf = "943.754.516-54";
            var res = AssertionConcern.AssertCpfIsValid(cpf, "CPF inválido.");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertCPFIsValid")]
        public void AssertCpfIsInvalidWhenRepeatDigit()
        {
            var cpf = "11111111111";
            var res = AssertionConcern.AssertCpfIsValid(cpf, "CPF inválido.");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        [TestCategory("AssertCNPJIsValid")]
        public void AssertCnpjIsValid()
        {
            var cnpj = "24.219.611/0001-93";
            var result = AssertionConcern.AssertCnpjIsValid(cnpj, "CNPJ inválido.");
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("AssertCNPJIsValid")]
        public void AssertCnpjIsInvalid()
        {
            var cnpj = "24.219.666/0001-93";
            var result = AssertionConcern.AssertCnpjIsValid(cnpj, "CNPJ inválido.");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("AssertCNPJIsValid")]
        public void AssertCnpjIsInvalidWhenRepeatDigit()
        {
            var cnpj = "11.111.111/0001-11";
            var result = AssertionConcern.AssertCnpjIsValid(cnpj, "CNPJ inválido.");
            Assert.IsNotNull(result);
        }
    }
}