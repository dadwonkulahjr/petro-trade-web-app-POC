using HADI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly IChecklistRepo _checklistRepo;

        public ChecklistController(IChecklistRepo checklistRepo)
        {
            _checklistRepo = checklistRepo;
        }

        [HttpGet]
        public ViewResult CreateChecklist()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Checker()
        {
            return View();
        }




        [HttpPost]
        public IActionResult CreateChecklist(Checklist model)
        {
            if (ModelState.IsValid)
            {

                Checklist newChecklist = new Checklist
                {
                    Content = model.Content,
                    Comment = model.Comment
                };

                _checklistRepo.AddChecklist(newChecklist);
                return RedirectToAction("allChecklists");
            }

            return View();
        }

        public ViewResult AllChecklists()
        {
            var model = _checklistRepo.GetAllCheckList();
            return View(model);
        }

        public ViewResult EditChecklist(int id) 
        {
            var checklist = _checklistRepo.GetChecklist(id);

            Checklist editChecklist = new Checklist
            {
                Content = checklist.Content
            };

            return View(editChecklist);
        }

        [HttpPost ]
        public IActionResult EditChecklist(Checklist model)
        {
            if (ModelState.IsValid)
            {
                Checklist checklist = _checklistRepo.GetChecklist(model.Id);
                checklist.Content = model.Content;
                checklist.Comment = model.Comment;
               
                
                _checklistRepo.UpdateChecklist(checklist);
                return RedirectToAction("index");
            }

            return View();
        }
    }
}
