using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.Models
{
    class InsurancedPatient:Patient
    {
        public string Dispetcher { get; set; }
        public string PolicyNumber { get; set; }
        public string Validity { get; set; }
    }
}
