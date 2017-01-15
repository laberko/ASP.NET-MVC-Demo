using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly TestModel _db = new TestModel();

        // GET: Messages
        public async Task<ActionResult> Index(int? id, bool? sent)
        {
            if (id == null || sent == null)
                return View(await _db.Messages.ToListAsync());
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if ((bool)sent)
                return View(employee.SentMessages.ToList());
            return View(employee.Messages.ToList());
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var message = await _db.Messages.FindAsync(id);
            if (message == null)
                return HttpNotFound();
            return View(message);
        }

        // GET: Messages/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ToId = id;
            var employee = await _db.Employees.FindAsync(id);
            ViewBag.ToName = employee.FullName;
            return View(new MessageViewModel
            {
                ToId = (int)id,
                JobDeadLine = DateTime.Now
            });
        }

        // POST: Messages/Create
        [HttpPost]
        public async Task<ActionResult> Create(MessageViewModel message)
        {
            if (!ModelState.IsValid)
                return View(message);
            var employeeTo = await _db.Employees.FindAsync(message.ToId);
            var employeeFrom = await _db.Employees.FindAsync(message.FromId);
            var newMessage = new Message
            {
                Title = message.Title,
                Text = message.Text,
                Date = DateTime.Now,
                From = employeeFrom,
                To = employeeTo,
                FromName = employeeFrom.FullName,
                ToName = employeeTo.FullName
            };
            if (message.HasJob)
            {
                var newJob = new Job
                {
                    Title = message.JobTitle,
                    Content = message.JobContent,
                    DeadLine = message.JobDeadLine ?? DateTime.Now,
                    Author = employeeFrom.FullName
                };
                _db.Jobs.Add(newJob);
                newMessage.ToDo = newJob;
            }
            _db.Messages.Add(newMessage);
            employeeTo.Messages.Add(newMessage);
            employeeFrom.SentMessages.Add(newMessage);
            _db.Entry(employeeTo).State = EntityState.Modified;
            _db.Entry(employeeFrom).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var message = await _db.Messages.FindAsync(id);
            if (message == null)
                return HttpNotFound();
            return View(message);
        }

        // POST: Messages/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Message message)
        {
            if (!ModelState.IsValid)
                return View(message);
            _db.Entry(message).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var message = await _db.Messages.FindAsync(id);
            if (message == null)
                return HttpNotFound();
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var message = await _db.Messages.FindAsync(id);
            _db.Messages.Remove(message);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
