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

using System.Linq;
using Nfield.Infrastructure;
using Nfield.Models;

namespace Nfield.PowerShell.Managers.Abstract
{
    public interface IInterviewerManager
    {
        Interviewer Add(INfieldConnection connection, Interviewer interviewer);

        IQueryable<Interviewer> GetAll(INfieldConnection connection);

        Interviewer GetByUserName(INfieldConnection connection, string userName);

        Interviewer GetById(INfieldConnection connection, string id);

        Interviewer GetByClientId(INfieldConnection connection, string clientId);

        void Remove(INfieldConnection connection, Interviewer interviewer);

        Interviewer Update(INfieldConnection connection, Interviewer interviewer);
    }
}
