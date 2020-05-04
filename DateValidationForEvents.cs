using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasySportEvent
{
    public class DateValidationForEvents : ValidationAttribute
    {
        DateTime date;
        private readonly DateTime _downDateLine = new DateTime(2019, 1, 1, 23, 59, 59);
        private readonly DateTime _topDateLine = new DateTime(2022, 1, 1, 23, 59, 59);
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                date = Convert.ToDateTime(value);
                if (date > _downDateLine && date < _topDateLine) return true;

            }
            return false;
        }
    }
}
