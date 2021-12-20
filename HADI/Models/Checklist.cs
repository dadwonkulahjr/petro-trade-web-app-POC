using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HADI.Models
{
    public class CheckList
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string StationName { get; set; }
        [Required, Column(TypeName ="date")]

        public DateTime Date { get; set; }
        [Required, StringLength(255)]

        public string PhoneNumber { get; set; }

        public IEnumerable<CheckListReportBridgeTable> CheckListReportBridgeTables { get; set; }

    }
}
