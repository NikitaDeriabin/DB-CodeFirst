using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace EasySportEvent.Models
{
    public class Team
    {
        public Team()
        {
            Persons = new List<Person>();
            HomeEvents = new List<Event>();
            GuestEvents = new List<Event>();
           // Squad = new Squad();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Ви можете ввести тiльки лiтери латиницi та пробiл. Перша буква повинна бути прописною")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Поле повинно бути заповненим")]
        [Range(1, 20, ErrorMessage = "Значення може бути вiд 1 до 20")]
        [Display(Name = "Мiсце в рейтингу")]
        public int? Rating { get; set; }//мiсце в лiзi

        [Display(Name = "Очки")]
        [Range(0, 100, ErrorMessage = "Значення може бути вiд 0 до 100")]
        public int? PointAmount { get; set; }

        [Display(Name = "Перемоги")]
        [Range(0, 20, ErrorMessage = "Значення може бути вiд 0 до 20")]
        public int? WinAmount { get; set; }

        
        [Display(Name = "Поразки")]
        [Range(0, 20, ErrorMessage = "Значення може бути вiд 0 до 20")]
        public int? LoseAmount { get; set; }

        [Display(Name = "Нiчия")]
        [Range(0, 20, ErrorMessage = "Значення може бути вiд 0 до 20")]
        public int? DrawAmount { get; set; }


        [Display(Name = "Лiга")]
        public int? LeagueId { get; set; }
        public virtual League League {get; set;}
        //public virtual Squad Squad{ get; set; }

        public virtual ICollection<Person> Persons { get; set; }

        [InverseProperty("HomeTeam")]
        public virtual ICollection<Event> HomeEvents { get; set; }
        [InverseProperty("GuestTeam")]
        public virtual ICollection<Event> GuestEvents { get; set; }
    }
}
