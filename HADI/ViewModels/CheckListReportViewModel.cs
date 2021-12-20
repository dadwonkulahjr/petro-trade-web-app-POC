using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HADI.ViewModels
{
    public class CheckListReportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Station Name")]
        public string StationName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public List<CustomCheckBoxItem> AvailableReports { get; set; }
    }
}
