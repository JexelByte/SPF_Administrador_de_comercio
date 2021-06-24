using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF
{
    public class Factura_Compra
    {
        public List<compra1> productos { get; set; }
        public DateTime fecha { get; set; }
        public double total { get; set; }
    }
}
