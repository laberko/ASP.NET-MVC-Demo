using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace TestWebApp.Models
{
    public class Employee
    {
        public Employee()
        {
            Messages = new HashSet<Message>();
            SentMessages = new HashSet<Message>();
        }
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        public string Password { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        public int? ManagerId { get; set; }
        [Display(Name = "Входящие")]
        public virtual ICollection<Message> Messages { get; set; }

        [Display(Name = "Отправленные")]
        public virtual ICollection<Message> SentMessages { get; set; }


        [NotMapped]
        [Display(Name = "Руководитель")]
        public Employee Manager
        {
            get
            {
                Employee manager;
                using (var db = new TestModel())
                    manager = db.Employees.FirstOrDefault(e => e.UserId == ManagerId);
                return manager;
            }
        }

        [NotMapped]
        public string FullName => SecondName + " " + FirstName;


        [NotMapped]
        [Display(Name = "Подчиненные")]
        public List<Employee> Managed
        {
            get
            {
                var people = new List<Employee>();
                using (var db = new TestModel())
                {
                    people.AddRange(db.Employees.Where(employee => employee.ManagerId == UserId));
                }
                return people;
            }
        }

        [NotMapped]
        public List<Job> Jobs => (from message in Messages where message.ToDo != null select message.ToDo).ToList();

        [NotMapped]
        public IEnumerable<SelectListItem> People
        {
            get
            {
                var people = new List<Employee>();
                using (var db = new TestModel())
                    people.AddRange(db.Employees.Where(e => e.UserId != UserId));
                var list = new SelectList(people, "UserId", "FullName");
                return list;
            }
        }
    }
}