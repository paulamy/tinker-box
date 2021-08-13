using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ISessionDao
    {
        List<Session> GetAllSessions(int userID);

        Session GetSessionByID(int userID, int sessionID);

        Session AddSession(int userID, Session newSession);

    }
}
