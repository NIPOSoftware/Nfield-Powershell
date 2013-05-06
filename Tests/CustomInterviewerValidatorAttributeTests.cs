﻿using System.Management.Automation;
using Nfield.PowerShell.Helpers;
using Nfield.PowerShell.Helpers.Concrete;
using Xunit;
using Nfield.Models;

namespace Nfield.PowerShell.Tests
{
    
    public class CustomInterviewerValidatorAttributeTests
    {
        [Fact]
        public void TestValidate_InterviewerIsNull_ThrowsValidationMetadataException()
        {
            Interviewer stubInterviewer = null;

            var validator = new CustomInterviewerValidatorAttribute();

            Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));

        }

        [Fact]
        public void TestValidate_InterviewerUserNameIsNull_ThrowsValidationMetadataException()
        {
            var stubInterviewer = new Interviewer(){UserName = null};

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("Required"));

        }

        [Fact]
        public void TestValidate_InterviewerUserNameLengthMoreThanFifty_ThrowsValidationMetadataException()
        {
            var userNameString =
                "Lorem ipsum dolor sit amet, te est tollit definiebas, ullum appareat adversarium an usu. Offendit";
            var stubInterviewer = new Interviewer() { UserName = userNameString };

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("maximum length"));
            Assert.True(ex.Message.Contains("UserName"));
        }

        [Fact]
        public void TestValidate_InterviewerPasswordIsNull_ThrowsValidationMetadataException()
        {
            var stubInterviewer = new Interviewer() {UserName = "username", Password = null};

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("Required"));
            Assert.True(ex.Message.Contains("Password"));
        }

        [Fact]
        public void TestValidate_InterviewerFirstNameLengthMoreThanFifty_ThrowsValidationMetadataException()
        {
            var firstNameString =
                "Lorem ipsum dolor sit amet, te est tollit definiebas, ullum appareat adversarium an usu. Offendit";
            var stubInterviewer = new Interviewer()
                {
                    UserName = "username",
                    Password = "password",
                    FirstName = firstNameString
                };

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("maximum length"));
            Assert.True(ex.Message.Contains("FirstName"));

        }

        [Fact]
        public void TestValidate_InterviewerLastNameLengthMoreThanFifty_ThrowsValidationMetadataException()
        {
            var lastNameString =
                "Lorem ipsum dolor sit amet, te est tollit definiebas, ullum appareat adversarium an usu. Offendit";
            var stubInterviewer = new Interviewer()
                {
                    UserName = "username",
                    Password = "password",
                    FirstName = "firstname",
                    LastName = lastNameString
                };

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("maximum length"));
            Assert.True(ex.Message.Contains("LastName"));

        }

        [Fact]
        public void TestValidate_InterviewerEmailAddressLengthMoreThanFifty_ThrowsValidationMetadataException()
        {
            var EmailAddressString =
                "Lorem ipsum dolor sit amet, te est tollit definiebas, ullum appareat adversarium an usu. Offendit";
            var stubInterviewer = new Interviewer()
                {
                    UserName = "username",
                    Password = "password",
                    FirstName = "firstname",
                    LastName = "lastname",
                    EmailAddress = EmailAddressString
                };

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("maximum length"));
            Assert.True(ex.Message.Contains("EmailAddress"));

        }

        [Fact]
        public void TestValidate_InterviewerTelephoneNumberLengthMoreThanThirty_ThrowsValidationMetadataException()
        {
            var TelephoneNumberString =
                "Lorem ipsum dolor sit amet, te est";
            var stubInterviewer = new Interviewer()
            {
                UserName = "username",
                Password = "password",
                FirstName = "firstname",
                LastName = "lastname",
                EmailAddress = "email@niposoftware.com",
                TelephoneNumber = TelephoneNumberString
            };

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("maximum length"));
            Assert.True(ex.Message.Contains("TelephoneNumber"));

        }

        [Fact]
        public void TestValidate_InterviewerClientInterviewerIdLengthMoreThanEight_ThrowsValidationMetadataException()
        {
            var ClientInterviewerIdString =
                "MoreThan8Characters";
            var stubInterviewer = new Interviewer()
            {
                UserName = "username",
                Password = "password",
                FirstName = "firstname",
                LastName = "lastname",
                EmailAddress = "email@niposoftware.com",
                TelephoneNumber = "TelephoneNumber",
                ClientInterviewerId = ClientInterviewerIdString
            };

            var validator = new CustomInterviewerValidatorAttribute();

            var ex = Assert.Throws<ValidationMetadataException>(() => validator.Validate(stubInterviewer));
            Assert.True(ex.Message.Contains("ClientInterviewerId"));

        }
    }
}
