using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StationName { get; set; }
        public DateTime Date { get; set; }
        public Checklist Checklist { get; set; }
        public string Ischecked { get; set; }
    }
}
