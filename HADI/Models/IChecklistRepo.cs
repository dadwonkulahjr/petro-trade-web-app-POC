using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public interface IChecklistRepo
    {
        Checklist GetChecklist(int id);
        Checklist AddChecklist(Checklist newChecklist);
        Checklist DeleteChecklist(int id);
        Checklist UpdateChecklist(Checklist updatedChecklist);
        IEnumerable<Checklist> GetAllCheckList();
    }
}
