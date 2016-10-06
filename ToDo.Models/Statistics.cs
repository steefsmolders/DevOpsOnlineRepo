using System.Collections.Generic;

namespace ToDo.Models
{
    public class Statistics
    {
        public int TotalCount { get; set; }

        public int PeopleCount { get; set; }

        public int BigTaskCount { get; set; }

        public int SmallTaskCount { get; set; }

        public long AvarageHours { get; set; }

        public long AvarageHoursPerPerson { get; set; }

        public List<DateStatic> NextMonthStatistic { get; set; }
    }
}