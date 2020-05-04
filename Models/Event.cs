using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySportEvent.Models
{
    public class Event
    {
        public int Id { get; set; }

        [DateValidationForEvents(ErrorMessage = "Дiапазон дати вiд 2019 року до 2022 року")]
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

        [Display(Name = "Команда1")]
        //[ForeignKey("HomeTeam")]
        public int? HomeTeamId { get; set; }

        [Display(Name = "Команда2")]
        //[ForeignKey("GuestTeam")]
        public int? GuestTeamId { get; set; }

        [Display(Name = "Гравець")]
        //[ForeignKey("HomePerson")]
        public int? HomePersonId { get; set; }

        [Display(Name = "Гравець")]
        //[ForeignKey("GuestPerson")]
        public int? GuestPersonId { get; set; }

        public virtual League League { get; set; }
        
        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }

        [ForeignKey("GuestTeamId")]
        public virtual Team GuestTeam { get; set; }

        [ForeignKey("HomePersonId")]
        public virtual Person HomePerson { get; set; }

        [ForeignKey("GuestPersonId")]
        public virtual Person GuestPerson { get; set; }
    }
}
