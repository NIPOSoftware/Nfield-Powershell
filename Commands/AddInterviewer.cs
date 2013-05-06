using System.Management.Automation;
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Helpers;

namespace Nfield.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "Interviewer")]
    public class AddInterviewer :Cmdlet
    {
        [Parameter(Mandatory = true)]
        public Domain Domain { get; set; }

        [Parameter(ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [CustomInterviewerValidatorAttribute]
        public PSObject Input { get; set; }

        protected override void ProcessRecord()
        {
            var manager = DependencyResolver.Current.Resolve<IInterviewerManager>();
            var helper = DependencyResolver.Current.Resolve<IProcessHelper>();

            helper.ProcessNfieldOrPs<Interviewer>(Input,
                (interviewer) =>
                {
                    WriteObject(manager.Add(Domain.Connection, interviewer));
                });
        }
    }
}
