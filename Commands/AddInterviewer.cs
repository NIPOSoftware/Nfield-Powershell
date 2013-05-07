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
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Helpers;
using Nfield.PowerShell.Helpers.Abstract;
using Nfield.PowerShell.Helpers.Concrete;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.PowerShell.State;

namespace Nfield.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "Interviewer")]
    public class AddInterviewer :Cmdlet
    {
        [Parameter(Mandatory = true)]
        public Domain Domain { get; set; }

        [Parameter(ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [CustomInterviewerValidator]
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
