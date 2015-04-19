using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Models
{
    public class SelectionAlgorithm
    {
        public string Name { get; set; }
        public ISelectorService Class { get; set; }
    }
}
