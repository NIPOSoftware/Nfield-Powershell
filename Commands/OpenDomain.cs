using Nfield.Infrastructure;
using Nfield.PowerShell.Helpers;
using Nfield.PowerShell.Implementation;
using Ninject;
using System;
using System.Management.Automation;       // Windows PowerShell namespace.
using System.Runtime.InteropServices;
using System.Security;

namespace Nfield.PowerShell.Commands
{
    /// <summary>
    /// This class implements the open-domain cmdlet.
    /// </summary>
    [Cmdlet(VerbsCommon.Open, "Domain",
            DefaultParameterSetName = ParameterSetNameNoCredential)]
    public class OpenDomain : Cmdlet
    {
        const string ParameterSetNameCredential = "Credential";
        const string ParameterSetNameNoCredential = "NoCredential";

        static IKernel _kernel;

        static OpenDomain()
        {
            _kernel = new StandardKernel();
            DependencyResolver.Register(type => _kernel.Get(type), type => _kernel.GetAll(type));

            NfieldSdkInitializer.Initialize((bind, resolve) => _kernel.Bind(bind).To(resolve).InTransientScope(),
                                            (bind, resolve) => _kernel.Bind(bind).To(resolve).InSingletonScope(),
                                            (bind, resolve) => _kernel.Bind(bind).ToConstant(resolve));

            _kernel.Bind(typeof(IDomainManager)).To(typeof(DomainManager)).InSingletonScope();
            _kernel.Bind(typeof(IInterviewerManager)).To(typeof(InterviewerManager)).InSingletonScope();
            _kernel.Bind(typeof(IPSObjectMapper)).To(typeof(PSObjectMapper)).InSingletonScope();
            _kernel.Bind(typeof(IProcessHelper)).To(typeof(ProcessHelper)).InSingletonScope();
        }

        [Parameter(Mandatory = true)]
        public string ServerUrl { get; set; }

        [Parameter(Mandatory = true)]
        public string DomainName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNameNoCredential)]
        public string UserName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNameNoCredential)]
        public string Password { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNameCredential)]
        public PSCredential Credential { get; set; }

        static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        protected override void ProcessRecord()
        {
            try
            {
                if (Credential != null)
                {
                    UserName = Credential.UserName;
                    Password = ConvertToUnsecureString(Credential.Password);
                }
                var connection = Nfield.Infrastructure
                        .NfieldConnectionFactory.Create(new Uri(ServerUrl));
                var manager = DependencyResolver.Current.Resolve<IDomainManager>();
                var domain = manager.Connect(connection, DomainName, UserName, Password);
                WriteObject(domain);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
