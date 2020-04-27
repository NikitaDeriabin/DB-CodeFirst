using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasySportEvent.Models
{
    public class League
    {
        public League()
        {
            Persons = new List<Person>();
            Teams = new List<Team>();
            Events = new List<Event>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Ви можете ввести тільки літери латиниці та пробіл. Перша буква повинна бути прописною")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [Range(1, 20, ErrorMessage = "Кількість має бути від 1 до 20")]
        [Display(Name = "Кількість")]
        public int EventAmount { get; set; }

        [Display(Name = "Регіон")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Event> Events { get; set; }


    }
}
