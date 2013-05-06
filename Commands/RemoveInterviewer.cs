using System.Management.Automation;
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Helpers.Abstract;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.PowerShell.State;

namespace Nfield.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "Interviewer")]
    public class RemoveInterviewer : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public Domain Domain { get; set; }

        [Parameter(ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public PSObject Input { get; set; }

        protected override void ProcessRecord()
        {
            var manager = DependencyResolver.Current.Resolve<IInterviewerManager>();
            var helper = DependencyResolver.Current.Resolve<IProcessHelper>();

            helper.ProcessNfieldOrPs<Interviewer>(Input,
                (interviewer) =>
                {
                    manager.Remove(Domain.Connection, interviewer);
                });
        }
    }
}
