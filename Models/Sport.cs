﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
