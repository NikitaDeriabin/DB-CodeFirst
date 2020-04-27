using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySportEvent.Models
{
    public class Squad
    {
        [Key]
        [ForeignKey("Team")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        [Display(Name = "iм'я")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Ви можете ввести тiльки лiтери латиницi та пробiл. Перша буква повинна бути прописною")]
        public string Name { get; set; } // ім'я гравця

        [Display(Name = "Позицiя")]
        [Required(ErrorMessage = "Поле повинно бути заповненим")]
        public string Position { get; set; }

        [Display(Name = "Команда")]
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        //public virtual Person Person { get; set; }

    }
}
