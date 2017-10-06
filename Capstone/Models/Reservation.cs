using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservationId { get; set; }
        public int siteId { get; set; }
        public string name { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public DateTime createDate { get; set; }
    }
}
