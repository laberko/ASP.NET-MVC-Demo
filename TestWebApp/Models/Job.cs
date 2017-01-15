using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Display(Name = "Выполнена")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Название задачи")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Содержание задачи")]
        public string Content { get; set; }

        [Display(Name = "Крайний срок")]
        public DateTime DeadLine { get; set; }
    }
}