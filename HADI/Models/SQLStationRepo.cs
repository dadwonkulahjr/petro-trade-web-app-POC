using HADI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public class SQLStationRepo : IStation
    {
        private readonly AppDbContext _context;

        public SQLStationRepo(AppDbContext context)
        {
            _context = context;
        }

        public Station AddStation(Station station)
        {
            _context.Add(station);
            _context.SaveChanges();
            return station;
        }

        public Station DeleteStation(int id)
        {
            Station station = _context.Stations.Find(id);

            if(station != null)
            {
                _context.Stations.Remove(station);
                _context.SaveChanges();
            }

            return station;

        }

        public IEnumerable<Station> GetAllStation()
        {
            return _context.Stations;
        }

        public Station GetStation(int id)
        {
            Station station = _context.Stations.Find(id);
            return station;
        }

        public Station UpdateStation(Station stationUpdate)
        {
            _context.Stations.Attach(stationUpdate);
            _context.SaveChanges();
            return stationUpdate;
        }
    }
}
