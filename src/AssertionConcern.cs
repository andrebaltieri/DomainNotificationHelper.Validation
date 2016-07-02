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

        public static DomainNotification AssertIsGreaterThan(DateTime value1, DateTime value2, string message)
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

        public static DomainNotification AssertIsBetween(DateTime value1, DateTime value2, DateTime value3, string message)
        {
            var isBetween = false;

            if (value2 < value3)
                isBetween = value2 <= value1 && value1 <= value3;
            else
                isBetween = !(value3 < value1 && value1 < value2);

            return (!isBetween)
                ? new DomainNotification("AssertArgumentBetween", message)
                : null;
        }

        public static DomainNotification AssertIsBetween(int value1, int value2, int value3, string message)
        {
            return (!(value1 >= value2 && value1 <= value3))
                ? new DomainNotification("AssertArgumentBetween", message)
                : null;
        }
    }
}