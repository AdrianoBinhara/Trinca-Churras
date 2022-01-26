using System;
namespace TrincaChurras.Models
{
    public class Barbecue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public double Value_per_person { get; set; }
        public int Participants_count { get; set; }
    }
}
