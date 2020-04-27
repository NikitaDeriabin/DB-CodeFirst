using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasySportEvent.Models
{
    public class Event
    {
        public int Id { get; set; }

        [DateValidation(ErrorMessage = "Дiапазон дати вiд 2019 року до 2020 року")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        public DateTime Date { get; set; }

        [Display(Name = "Результат")]
        public string Result { get; set; }

        [Display(Name = "Завершено")]
        public bool EndedEvent { get; set; }

        [Display(Name = "Лiга")]
        public int? LeagueId { get; set; }

        [Display(Name = "Команда")]
        public int Team1Id { get; set; }

        [Display(Name = "Команда")]
        public int Team2Id { get; set; }

        [Display(Name = "Гравець")]
        public int Person1Id { get; set; }

        [Display(Name = "Гравець")]
        public int Person2Id { get; set; }

        public virtual League League { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team GuestTeam { get; set; }
        public virtual Person HomePerson { get; set; }
        public virtual Person GuestPerson { get; set; }
    }
}
