using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF
{
    public class Factura
    {
        public DateTime Fecha { get; set; }
        public double Total { get; set; }
        public string trabajador{ get; set; }
        public List<compra1> productos { get; set; }
        public string cliente { get; set; }
    }
}
