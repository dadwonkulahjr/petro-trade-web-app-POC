using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public interface IStation
    {
        Station GetStation(int id);
        IEnumerable<Station> GetAllStation();
        Station AddStation(Station station);
        Station DeleteStation(int id);
        Station UpdateStation(Station stationUpdate);
    }
}
