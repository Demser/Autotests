using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.Models
{
    class PreorderData
    {       
        public string City { get; set; }
        public string Insurance { get; set; }
        public TakingType TakingType { get; set; }
        public int CartNumber { get; set; }
        public string FlaerNumber { get; set; }
        public bool IsEmployee { get; set; }
    }
    enum TakingType { Nothing, Mobile, CITO }
}
