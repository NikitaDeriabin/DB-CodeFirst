using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySportEvent.Models
{
    public class Sport
    {
        public Sport()
        {
            Regions = new List<Region>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Поле повинно бути заповненим")]
        [Display(Name = "Назва")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Ви можете ввести тільки літери латиниці та пробіл. Перша буква повинна бути прописною")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        //public string Check { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
