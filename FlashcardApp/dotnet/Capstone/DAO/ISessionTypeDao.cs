using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ISessionTypeDao
    {
        List<SessionType> GetAllSessionTypes();

        SessionType GetSessionTypeByName(string sessionTypeName);

        SessionType GetSessionTypeByID(int sessionTypeID);
    }
}
