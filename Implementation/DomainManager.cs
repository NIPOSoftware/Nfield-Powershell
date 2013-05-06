using Nfield.Infrastructure;

namespace Nfield.PowerShell.Implementation
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
