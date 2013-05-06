using Nfield.Infrastructure;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.PowerShell.State;

namespace Nfield.PowerShell.Managers.Concrete
{
    internal class DomainManager : IDomainManager
    {
        #region IDomainManager Members

        public Domain Connect(INfieldConnection connection, string domainName, string userName, string password)
        {
            var domain = new Domain() { Name = domainName, Connection = connection };
            var isSignInSuccessful = domain.Connection.SignInAsync(domainName, userName, password).Result;
            return isSignInSuccessful ? domain : null;
        }

        #endregion
    }
}
