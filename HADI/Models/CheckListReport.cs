using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public class CheckListReport
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Content { get; set; }

        public IEnumerable<CheckListReportBridgeTable> CheckListReportBridgeTables { get; set; }
    }
}
