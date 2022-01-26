using System;
using System.Collections.Generic;

namespace TrincaChurras.Models
{
    public class Barbecue
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public double Value_per_person { get; set; }
        public int Participants_count { get; set; }
        public string Image { get; set; }
        public List<Person> Participants { get; set; }
    }
}
