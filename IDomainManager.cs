using Nfield.Infrastructure;

namespace Nfield.PowerShell
{
    public interface IDomainManager
    {
        Domain Connect(INfieldConnection connection, string domainName, string userName, string password);
    }
}
