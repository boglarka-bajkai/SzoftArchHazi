using System.ComponentModel.DataAnnotations.Schema;

namespace SzoftArchHazi.Api
{
    public class OnDutyDate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int OnDutyId { get; set; }    
        
        public DateTime DutyDay { get; set; }

        public bool IsFixed { get; set; }
    }
}