using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SondajMaliyetClass.Models
{
    public class TumGiderler
    {
        public int giderId { get; set; }
        public string giderAdi { get; set; }
        public double topMaliyet { get; set; }
        public double metreMaliyet { get; set; }
        public double sondajDerinligi { get; set; }
        public double dovizKuru { get; set; }
        public DateTime tarih { get; set; }
    }
}
