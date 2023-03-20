using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FilmsApp
{
    public class Films
    {
        private readonly int id;
        public int Id { get { return id; } }
        private string fname;
        public string Fname { get { return fname; } set { fname = value; } }
        private double rate;
        public double Rate { get { return rate; } set { rate = value; } }
    }
}
