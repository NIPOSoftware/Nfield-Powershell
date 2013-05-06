using System.Linq;
using Nfield.Infrastructure;
using Nfield.Models;

namespace Nfield.PowerShell
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
