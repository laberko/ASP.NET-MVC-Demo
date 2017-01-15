using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly TestModel _db = new TestModel();

        // GET: Jobs
        public async Task<ActionResult> Index(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employee = await _db.Employees.FindAsync(id);
            ViewBag.ForName = employee.FullName;
            return View(employee.Jobs);
        }

        // GET: Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var job = await _db.Jobs.FindAsync(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var job = await _db.Jobs.FindAsync(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Job job)
        {
            if (!ModelState.IsValid)
                return View(job);
            _db.Entry(job).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Employees");
        }

        // GET: Jobs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var job = await _db.Jobs.FindAsync(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var job = await _db.Jobs.FindAsync(id);
            foreach (var message in _db.Messages.Where(m => m.ToDo != null && m.ToDo.JobId == job.JobId))
                message.ToDo = null;
            _db.Jobs.Remove(job);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Employees");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
