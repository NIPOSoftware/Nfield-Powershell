using Nfield.Infrastructure;
using Nfield.PowerShell.State;

namespace Nfield.PowerShell.Managers.Abstract
{
    public interface IDomainManager
    {
        Domain Connect(INfieldConnection connection, string domainName, string userName, string password);
    }
}
