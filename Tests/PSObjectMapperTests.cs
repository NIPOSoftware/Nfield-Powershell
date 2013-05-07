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

using System.Management.Automation;
using Nfield.Models;
using Nfield.PowerShell.Helpers.Concrete;
using Xunit;

namespace Nfield.PowerShell.Tests
{
    public class PSObjectMapperTests
    {
        [Fact]
        public void TestTo_MapToInterviewer_SetsCorrectProperty()
        {
            const string expectedUserName = "MyUserName";
            PSObject source = new PSObject();
            source.Properties.Add(new PSNoteProperty("username", expectedUserName));
            source.Properties.Add(new PSNoteProperty("ignored", "Boe"));
            source.Properties.Add(new PSNoteProperty("lastname", null)); // should not throw

            PSObjectMapper target = new PSObjectMapper();
            Interviewer result = target.To<Interviewer>(source);

            Assert.Equal(expectedUserName, result.UserName);
        }

        [Fact]
        public void TestFrom_MapFromInterviewer_SetsCorrectProperty()
        {
            const string expectedUserName = "MyUserName";
            Interviewer source = new Interviewer() { UserName = expectedUserName };

            PSObjectMapper target = new PSObjectMapper();
            PSObject result = target.From(source);

            Assert.Equal(expectedUserName, result.Properties["UserName"].Value.ToString());
        }
    }
}
