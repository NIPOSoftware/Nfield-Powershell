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
