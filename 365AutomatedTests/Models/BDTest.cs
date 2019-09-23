using _365AutomatedTests.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.Models
{
    public class BDTest : ITableModel
    {
        public string Name { get; set; }
        public string AmpProgram { get; set; }
        public string Subgroups {get;set;}

        /* 
         public List<String> GetSubrgoups => this.Subgroups.Split(';').ToList();

         public List <String> Subgroups { get { return Subgroups; } set { Subgroups = value; } }

         private List<string> subgroups = new List<string>();
         public List<string> Subgroups
         {
             get { return subgroups; }
             set { subgroups = value; }
         }
         */

        public int Volume { get; set; }
        public string IsDNA { get; set; }
        public string IsWAX { get; set; }
        public string IsActive { get; set; }
        public int Doubles { get; set; }
        public string ReagentsOT { get; set; }
        public int CellsCount { get; set; }
        public string CellControl { get; set; }
        public string CellStandarts { get; set; }
        public string CellReagents { get; set; }
        public string Fam { get; set; }
        public string Hex { get; set; }
        public string Rox { get; set; }

    }
}


