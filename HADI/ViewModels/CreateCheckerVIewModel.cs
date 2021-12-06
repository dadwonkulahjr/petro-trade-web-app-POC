using HADI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.ViewModels
{
    public class CreateCheckerVIewModel : Checklist 
    {
        public string StationName { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
