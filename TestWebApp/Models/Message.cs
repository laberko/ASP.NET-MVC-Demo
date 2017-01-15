using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Display(Name = "Прочитано")]
        public bool IsRead { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "От кого")]
        public Employee From { get; set; }

        public virtual Job ToDo { get; set; }

        [Display(Name = "Кому")]
        public Employee To { get; set; }

        public string ToName { get; set; }
        public string FromName { get; set; }

    }
}