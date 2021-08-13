using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ICardTypeDao
    {
        List<CardType> GetAllCardTypes();

        CardType GetCardTypeByName(string cardTypeName);

        CardType GetCardTypeByID(int cardTypeID);
    }
}
