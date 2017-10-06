using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        public int site_id { get; set; }
        public int campground_id { get; set; }
        public int site_number { get; set; }
        public int max_occupancy { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int totalDays { get; set; }
        public decimal totalCost { get; set; }
        public int maxRVLength { get; set; }
        public bool accesible { get; set; } //<---This might be wrong
        public bool utilities { get; set; } //<---This might be wrong
    }
}
