using HADI.Models;
using System.Collections.Generic;


namespace HADI.Repository.IRepository
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
