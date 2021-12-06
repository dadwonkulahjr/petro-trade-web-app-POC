using HADI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public class SQLChecklistRepo : IChecklistRepo
    {
        private readonly AppDbContext _context;

        public SQLChecklistRepo(AppDbContext context)
        {
            _context = context;
        }

        public Checklist AddChecklist(Checklist newChecklist)
        {
            _context.Add(newChecklist);
            _context.SaveChanges();
            return newChecklist;
        }

        public Checklist DeleteChecklist(int id)
        {
            var checklist = _context.Checklists.Find(id);

            if(checklist != null)
            {
                _context.Checklists.Remove(checklist);
                _context.SaveChanges();
               
            }

            return checklist;
        }

        public IEnumerable<Checklist> GetAllCheckList()
        {
            return _context.Checklists;
        }

        public Checklist GetChecklist(int id)
        {
            var checklist = _context.Checklists.Find(id);
            return checklist; 
        }

        public Checklist UpdateChecklist(Checklist updatedChecklist)
        {
            _context.Checklists.Attach(updatedChecklist);
            _context.SaveChanges();
            return updatedChecklist;
        }
    }
}
