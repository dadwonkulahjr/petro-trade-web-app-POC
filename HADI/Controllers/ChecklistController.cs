using HADI.Data;
using HADI.Models;
using HADI.Repository.IRepository;
using HADI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HADI.Controllers
{
    [AllowAnonymous]
    public class ChecklistController : Controller
    {
        private readonly AppDbContext _context;

        public ChecklistController(AppDbContext appContext)
        {
            _context = appContext;
        }

        [HttpGet]
        public ViewResult CheckListReport()
        {
            var model = _context.Checklists
                                .OrderBy(x => x.Name)
                                .ToList();

            return View("list", model);
        }


        [HttpGet]
        public ViewResult Checker()
        {
            var retrivedAllCheckListReports = _context.CheckListReports.ToList();
            CheckListReportViewModel model = new();

            model.AvailableReports = retrivedAllCheckListReports.Select(c => new CustomCheckBoxItem()
            {
                Id = c.Id,
                Content = c.Content,
                IsChecked = false
            }).ToList();

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Add(int? id)
        //{
        //    if (id == null)
        //    {
        //        var retrivedAllCheckListReports = _context.CheckListReports.ToList();
        //        CheckListReportViewModel model = new();

        //        model.AvailableReports = retrivedAllCheckListReports.Select(c => new CustomCheckBoxItem()
        //        {
        //            Id = c.Id,
        //            Content = c.Content,
        //            IsChecked = false
        //        }).ToList();

        //        return View(model);
        //    }
        //    else
        //    {

        //        CheckListReportViewModel model = new();
        //        var checkList = _context.Checklists
        //                                .Include(x => x.CheckListReportBridgeTables)
        //                                .ThenInclude(x => x.CheckListReport)
        //                                .AsNoTracking()
        //                                .FirstOrDefault(x => x.Id == id.Value);

        //        var allCheckListReports = _context.CheckListReports.Select(x => new CustomCheckBoxItem()
        //        {
        //            Id = x.Id,
        //            Content = x.Content,
        //            IsChecked = x.CheckListReportBridgeTables.Any(x => x.CheckListId == checkList.Id)
        //        }).ToList();

        //        model.Name = checkList.Name;
        //        model.StationName = checkList.StationName;
        //        model.Date = checkList.Date;
        //        model.PhoneNumber = checkList.PhoneNumber;
        //        model.AvailableReports = allCheckListReports;

        //        return View(model);
        //    }
        //}

        [HttpPost]
        public IActionResult Checker(CheckListReportViewModel model, CheckList checkList, CheckListReportBridgeTable checkListReportBridgeTable)
        {
            if (!ModelState.IsValid) { return View(model); }


            if (model.Id == 0)
            {
                List<CheckListReportBridgeTable> checkListReportBridgeTables = new();
                checkList.Name = model.Name;
                checkList.StationName = model.StationName;
                checkList.Date = model.Date;
                checkList.PhoneNumber = model.PhoneNumber;
                _context.Checklists.Add(checkList);
                _context.SaveChanges();
                int checkListId = checkList.Id;

                foreach (var checkBoxItem in model.AvailableReports)
                {
                    if (checkBoxItem.IsChecked)
                    {
                        checkListReportBridgeTables.Add(new CheckListReportBridgeTable() { CheckListId = checkListId, CheckListReportId = checkBoxItem.Id });
                    }
                }

                foreach (var checkListReportBridge in checkListReportBridgeTables)
                {
                    _context.CheckListReportBridgeTables.Add(checkListReportBridge);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Checker), nameof(CheckList));
            }
            else
            {
                List<CheckListReportBridgeTable> checkListReportBridgeTables = new();
                checkList.Name = model.Name;
                checkList.StationName = model.StationName;
                checkList.Date = model.Date;
                checkList.PhoneNumber = model.PhoneNumber;
                _context.Checklists.Add(checkList);
                _context.SaveChanges();
                int checkListId = checkList.Id;

                foreach (var checkBoxItem in model.AvailableReports)
                {
                    if (checkBoxItem.IsChecked)
                    {
                        checkListReportBridgeTables.Add(new CheckListReportBridgeTable() { CheckListId = checkListId, CheckListReportId = checkBoxItem.Id });
                    }
                }

                var dataTableSet = _context.CheckListReportBridgeTables.Where(c => c.CheckListId == checkListId).ToList();

                if (dataTableSet != null)
                {
                    var resultList = dataTableSet.Except(checkListReportBridgeTables).ToList();
                    foreach (var list in resultList)
                    {
                        _context.CheckListReportBridgeTables.Remove(list);
                        _context.SaveChanges();
                    }
                }

                var checkListReportBridges = _context.CheckListReportBridgeTables.Where(s => s.CheckListId == checkListId).ToList();

                foreach (var item in checkListReportBridgeTables)
                {
                    if (!checkListReportBridges.Contains(item))
                    {
                        _context.CheckListReportBridgeTables.Add(item);
                        _context.SaveChanges();
                    }
                }



                return RedirectToAction("checker");
            }




        }
        //[HttpGet]
        //public ViewResult CreateChecklist()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ViewResult Checker()
        //{
        //    CheckList model = new();
        //    return View(model);
        //}
        //[HttpPost]
        //public IActionResult Checker(CheckListReportViewModel model)
        //{
        //    if (!ModelState.IsValid) { return View(model); }

        //    CheckListReport report = new();
        //    for (int i = 0; i < model.CheckLists.Count; i++)
        //    {
        //        report.Name = model.Name;
        //        report.StationName = model.StationName;
        //        report.Date = model.Date;
        //        report.PhoneNumber = model.PhoneNumber;
        //        report.ContentType = model.CheckLists[i].ContentType;
        //        report.Content = model.Comment1;
        //        report.Comment1 = model.Comment1;
        //        report.Comment2 = model.Comment1;
        //        report.Comment3 = model.Comment1;
        //        report.Comment4 = model.Comment1;
        //        report.Comment5 = model.Comment1;

        //    }

        //    _context.CheckListReports.Add(report);
        //    _context.SaveChanges();
        //    return RedirectToAction("checker");
        //}





        //[HttpGet]
        //public ViewResult EditChecklist(int id)
        //{
        //    var checklist = _checklistRepo.GetChecklist(id);

        //    Checklist editChecklist = new Checklist
        //    {
        //        Id = id,

        //    };

        //    return View(editChecklist);
        //}

        //[HttpPost]
        //public IActionResult EditChecklist()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Checklist checklist = _checklistRepo.GetChecklist(model.Id);


        //        _checklistRepo.UpdateChecklist(checklist);
        //        return RedirectToAction("allChecklists");
        //    }

        //    return View();
        //}
    }
}
