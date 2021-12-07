using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using EFDataApp.Models;
using PagedList;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebCalculator1.Controllers
{
   public class LoginController : Controller
    {
        public ApplicationContext db;
        public LoginController(ApplicationContext context)
        {
            this.db = context;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? ID)
        {
            if (ID != null)
            {
                Operation operation = await db.Operations.FirstOrDefaultAsync(p => p.ID == ID);
                if (operation != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return View(operation);
                    }
                    else
                    {
                        return View("Login");
                    }
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<RedirectToActionResult> Edit(Operation operation)
        {
            db.Operations.Update(operation);
            await db.SaveChangesAsync();
            return RedirectToAction("Info");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid? ID)
        {
            if (ID != null)
            {
                Operation operation = await db.Operations.FirstOrDefaultAsync(p => p.ID == ID);
                if (operation != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return View(operation);
                    }
                    else
                    {
                        return View("Login");
                    }
                }

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Delete(Guid? ID)
        {
          
            Operation operation = await db.Operations.FirstOrDefaultAsync(p => p.ID == ID);
              
            db.Operations.Remove(operation);
            await db.SaveChangesAsync();
            return RedirectToAction("Info");
        }

        [HttpGet]
        public ViewResult Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return View("Login");
            }

        }
        [HttpPost]
        public async Task<RedirectToActionResult> Add(string Expression, double Result, DateTime Date, string Error, string Browser, string IP, Operation operation)
        {
            operation.Expression = Expression;
            operation.Result = Result;
            operation.Date = Date;
            operation.Error = Error;
            operation.Browser = Browser;
            operation.IP = IP;
            db.Operations.Add(operation);
            await db.SaveChangesAsync();
            return RedirectToAction("Info");
        }
        public ViewResult Info(int? page)
        {
            var operations = from s in db.Operations
                             select s;
            operations = operations.OrderByDescending(s => s.Date);
            int pageSize = 10;
            int pageIndex = (page ?? 1);
            return View(operations.ToPagedList(pageIndex, pageSize));
        }
    }
}
