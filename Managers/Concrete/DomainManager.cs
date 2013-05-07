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
