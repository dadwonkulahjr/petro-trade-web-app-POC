using HADI.Data;
using HADI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HADI.Controllers
{
    [AllowAnonymous]
    public class CheckListReportController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CheckListReportController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = _appDbContext.CheckListReports.ToList();
            return View("list", model);
        }

        [HttpGet]
        public IActionResult Add(int? id)
        {
            if (id == null)
            {
                CheckListReport model = new();
                return View(model);
            }

            var checkListModel = _appDbContext.CheckListReports.FirstOrDefault(x => x.Id == id.Value);
            if (checkListModel is null)
            {
                return NotFound();
            }


            return View(checkListModel);
        }

        [HttpPost]
        public IActionResult Add(CheckListReport model)
        {
            if (!ModelState.IsValid) { return View(model); }


            if (model.Id == 0)
            {
                //Create
                _appDbContext.CheckListReports.Add(model);
                _appDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), nameof(CheckListReport));

            }
            else
            {
                //Update
                var checkListReportFound = _appDbContext.CheckListReports.FirstOrDefault(x => x.Id == model.Id);

                if(checkListReportFound is null)
                {
                    return NotFound();
                }
                else
                {
                    checkListReportFound.Content = model.Content;
                    _appDbContext.CheckListReports.Update(checkListReportFound);
                    _appDbContext.SaveChanges();
                    return RedirectToAction(nameof(Index), nameof(CheckListReport));

                }

            }
        }
    }
}
