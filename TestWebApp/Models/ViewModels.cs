using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace TestWebApp.Models
{
    public class EmployeeViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
    }

    public class MessageViewModel
    {
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required]
        public int ToId { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Отправитель")]
        public int FromId { get; set; }

        [Display(Name = "Добавить задачу")]
        public bool HasJob { get; set; }

        [Display(Name = "Название задачи")]
        public string JobTitle { get; set; }

        [Display(Name = "Содержание задачи")]
        public string JobContent { get; set; }

        [Display(Name = "Крайний срок")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM.dd.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JobDeadLine { get; set; }




        public static IEnumerable<SelectListItem> OtherPeople(int myId)
        {
            var people = new List<Employee>();
            using (var db = new TestModel())
                people.AddRange(db.Employees.Where(e => e.UserId != myId));
            var list = new SelectList(people, "UserId", "FullName");
            return list;
        }
    }
}