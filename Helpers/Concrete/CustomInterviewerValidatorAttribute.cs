//    This file is part of Nfield.PowerShell.
//
//    Nfield.PowerShell is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    Nfield.PowerShell is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public License
//    along with Nfield.PowerShell.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Management.Automation;
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Helpers.Abstract;

namespace Nfield.PowerShell.Helpers.Concrete
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CustomInterviewerValidatorAttribute : ValidateEnumeratedArgumentsAttribute
    {
        private string message;

        protected override void ValidateElement(object element)
        {
            var processHelper = DependencyResolver.Current.Resolve<IProcessHelper>();
            processHelper.ProcessItem<Interviewer>(element,
                (interviewer) =>
                    {
                        Validate(interviewer);
                    });

        }

        public void Validate(Interviewer interviewer)
        {
            if (interviewer == null)
                throw new ValidationMetadataException(message + "\nValidator Failure: Argument Is Null");

            if (string.IsNullOrEmpty(interviewer.UserName))
                throw new ValidationMetadataException(message + "\nValidator Failure: UserName is Required");

            if (!string.IsNullOrEmpty(interviewer.UserName) && interviewer.UserName.Trim().Length > 50)
                throw new ValidationMetadataException(message + "\nValidator Failure: UserName maximum length is 50");

            if (string.IsNullOrEmpty(interviewer.Password))
                throw new ValidationMetadataException(message + "\nValidator Failure: Password is Required");

            if (!string.IsNullOrEmpty(interviewer.FirstName) && interviewer.FirstName.Trim().Length > 50)
                throw new ValidationMetadataException(message + "\nValidator Failure: FirstName maximum length is 50");

            if (!string.IsNullOrEmpty(interviewer.LastName) && interviewer.LastName.Trim().Length > 50)
                throw new ValidationMetadataException(message + "\nValidator Failure: LastName maximum length is 50");

            if (!string.IsNullOrEmpty(interviewer.EmailAddress) && interviewer.EmailAddress.Trim().Length > 50)
                throw new ValidationMetadataException(message + "\nValidator Failure: EmailAddress maximum length is 50");

            if (!string.IsNullOrEmpty(interviewer.TelephoneNumber) && interviewer.TelephoneNumber.Trim().Length > 30)
                throw new ValidationMetadataException(message + "\nValidator Failure: TelephoneNumber maximum length is 30");

            if (!string.IsNullOrEmpty(interviewer.ClientInterviewerId) && interviewer.ClientInterviewerId.Trim().Length != 8)
                throw new ValidationMetadataException(message + "\nValidator Failure: ClientInterviewerId should be 8 digit");
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Error Message is null or empty");
                }
                message = value;
            }
        }
    }
}
