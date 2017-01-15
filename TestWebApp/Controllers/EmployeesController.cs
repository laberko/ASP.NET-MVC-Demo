using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TestWebApp.Models;
using WebMatrix.WebData;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;

namespace TestWebApp.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly TestModel _db = new TestModel();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            return View(await _db.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        // GET: Employees/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            WebSecurity.CreateUserAndAccount(employee.Login, employee.Password);
            _db.Employees.Add(new Employee
            {
                Login = employee.Login,
                Password = employee.Password,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName
            });
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            _db.Entry(employee).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(employee.Login);
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(employee.Login, true);
            foreach (var message in _db.Messages.Where(m => m.To.UserId == id))
                message.To = null;
            foreach (var message in _db.Messages.Where(m => m.From.UserId == id))
                message.From = null;
            _db.Employees.Remove(employee);
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
