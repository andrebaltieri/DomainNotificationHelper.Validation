using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DomainNotificationHelper.Events;

namespace DomainNotificationHelper.Validation
{
    public static class AssertionConcern
    {
        public static bool IsSatisfiedBy(params DomainNotification[] validations)
        {
            var notificationsNotNull = validations.Where(validation => validation != null);
            NotifyAll(notificationsNotNull);

            return notificationsNotNull.Count().Equals(0);
        }

        private static void NotifyAll(IEnumerable<DomainNotification> notifications)
        {
            notifications.ToList().ForEach(validation => { DomainEvent.Raise(validation); });
        }

        public static DomainNotification AssertLength(string stringValue, int minimum, int maximum, string message)
        {
            var length = stringValue.Trim().Length;

            return (length < minimum || length > maximum)
                ? new DomainNotification("AssertArgumentLength", message)
                : null;
        }

        public static DomainNotification AssertMatches(string pattern, string stringValue, string message)
        {
            var regex = new Regex(pattern);

            return (!regex.IsMatch(stringValue))
                ? new DomainNotification("AssertArgumentLength", message)
                : null;
        }

        public static DomainNotification AssertNotEmpty(string stringValue, string message)
        {
            return (string.IsNullOrEmpty(stringValue))
                ? new DomainNotification("AssertArgumentNotEmpty", message)
                : null;
        }

        public static DomainNotification AssertNotNull(object object1, string message)
        {
            return (object1 == null)
                ? new DomainNotification("AssertArgumentNotNull", message)
                : null;
        }

        public static DomainNotification AssertIsNull(object object1, string message)
        {
            return (object1 != null)
                ? new DomainNotification("AssertArgumentNull", message)
                : null;
        }

        public static DomainNotification AssertTrue(bool boolValue, string message)
        {
            return (!boolValue)
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertAreEquals(string value, string match, string message)
        {
            return (!(value == match))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterThan(int value1, int value2, string message)
        {
            return (!(value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterThan(decimal value1, decimal value2, string message)
        {
            return (!(value1 > value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertIsGreaterOrEqualThan(int value1, int value2, string message)
        {
            return (!(value1 >= value2))
                ? new DomainNotification("AssertArgumentTrue", message)
                : null;
        }

        public static DomainNotification AssertRegexMatch(string value, string regex, string message)
        {
            return (!Regex.IsMatch(value, regex, RegexOptions.IgnoreCase))
                ? new DomainNotification("AssertRegexNotMatch", message)
                : null;
        }

        public static DomainNotification AssertEmailIsValid(string email, string message)
        {
            var emailRegex =
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return (!Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase))
                ? new DomainNotification("AssertEmailIsInvalid", message)
                : null;
        }

        public static DomainNotification AssertUrlIsValid(string url, string message)
        {
            // Do not validate if no URL is provided
            // You can call AssertNotEmpty before this if you want
            if (String.IsNullOrEmpty(url))
                return null;

            var regex = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

            return (!Regex.IsMatch(url, regex, RegexOptions.IgnoreCase))
                ? new DomainNotification("AssertUrlIsInvalid", message)
                : null;
        }

        public static DomainNotification AssertCpfIsValid(string cpf, string message)
        {
            // Do not validate if no CPF is provided
            // You can call AssertNotEmpty before this if you want
            if (string.IsNullOrEmpty(cpf))
                return null;

            cpf = cpf
                .Replace(".", "")
                .Replace("-", "")
                .Trim();

            if (cpf.Length != 11)
                return new DomainNotification("AssertCPFIsInvalid", message);

            if (cpf.Any(t => cpf.Replace(t.ToString(), "").Length <= 2))
                return new DomainNotification("AssertCPFIsInvalid", message);

            //Start validation
            var sum = 0;
            for (var count = 0; count < 9; count++)
                sum += int.Parse(cpf[count].ToString()) * (10 - count);

            var resto = sum % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digit = resto.ToString();

            sum = 0;
            for (var count = 0; count < 10; count++)
                sum += int.Parse(cpf[count].ToString()) * (11 - count);

            resto = sum % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digit += resto.ToString();

            return (!cpf.EndsWith(digit)) ? new DomainNotification("AssertUrlIsInvalid", message) : null;
        }

        public static DomainNotification AssertCnpjIsValid(string cnpj, string message)
        {
            // Do not validate if no CNPJ is provided
            // You can call AssertNotEmpty before this if you want
            if (string.IsNullOrEmpty(cnpj))
                return null;

            cnpj = cnpj
                .Replace(".", "")
                .Replace("-", "")
                .Replace("/", "")
                .Trim();

            if (cnpj.Length != 14)
                return new DomainNotification("AssertCPFIsInvalid", message);

            if (cnpj.Any(t => cnpj.Replace(t.ToString(), "").Length <= 2))
                return new DomainNotification("AssertCPFIsInvalid", message);

            //Start validation
            var sum = 0;
            var primaryVector = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var secondVector = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (var count = 0; count < 12; count++)
                sum += int.Parse(cnpj[count].ToString()) * primaryVector[count];

            var rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            var digit = rest.ToString();

            sum = 0;
            for (var count = 0; count < 13; count++)
                sum += int.Parse(cnpj[count].ToString()) * secondVector[count];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit += rest.ToString();

            return (!cnpj.EndsWith(digit)) ? new DomainNotification("AssertUrlIsInvalid", message) : null;
        }
    }
}