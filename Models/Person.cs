using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySportEvent.Models
{
    public class Person
    {
        public Person()
        {
            HomeEvents = new List<Event>();
            GuestEvents = new List<Event>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [Display(Name = "iм'я")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Ви можете ввести тiльки лiтери латиницi та пробiл. Перша буква повинна бути прописною")]
        public string Name { get; set; }
        

        [DateValidation(ErrorMessage = "Дiапазон дати вiд 1950 року до 2020 року")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата народження")]
        public DateTime DateBirth { get; set; }

        [Display(Name = "Позицiя")]
        public string Position { get; set; }

        [Display(Name = "Країна")]
        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        public string Country { get; set; }

        [Display(Name = "Очки")]
        [Range(1, 100, ErrorMessage = "Значення може бути вiд 1 до 100")]
        public int? PointAmount { get; set; }

        [Display(Name = "Перемоги")]
        [Range(1, 20, ErrorMessage = "Значення може бути вiд 1 до 20")]
        public int? WinAmount { get; set; }

        [Display(Name = "Поразки")]
        [Range(1, 20, ErrorMessage = "Значення може бути вiд 1 до 20")]
        public int? LoseAmoutn { get; set; }

        //public virtual Squad Squad { get; set; }

        [Display(Name = "Команда")]
        public int? TeamId { get; set; }
        
        [Display(Name = "Лiга")]
        public int? LeagueId { get; set; }
        public virtual Team Team { get; set; }
        public virtual League League { get; set; }

        [InverseProperty("HomePerson")]
        public virtual ICollection<Event> HomeEvents { get; set; }

        [InverseProperty("GuestPerson")]
        public virtual ICollection<Event> GuestEvents { get; set; }

    }
}
