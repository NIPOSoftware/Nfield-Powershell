using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using Nfield.Infrastructure;
using Nfield.Models;

namespace Nfield.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "Interviewer")]
    public class UpdateInterviewer : Cmdlet
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
                    WriteObject(manager.Update(Domain.Connection, interviewer));
                });
        }
    }
}
