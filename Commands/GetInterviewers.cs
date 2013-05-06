using Nfield.Infrastructure;
using System.Management.Automation;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.PowerShell.State;

// Windows PowerShell namespace.

namespace Nfield.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "Interviewers")]
    public class GetInterviewers : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public Domain Domain { get; set; }

        protected override void ProcessRecord()
        {
            var manager = DependencyResolver.Current.Resolve<IInterviewerManager>();

            var result = manager.GetAll(Domain.Connection);

            WriteObject(result, true);
        }
    }
}
