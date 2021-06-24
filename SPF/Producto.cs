using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF
{
    public class Producto
    {
        public double Precio_venta { get; set; }
        public double Precio_Compra { get; set; }

        public int cantidad { get; set; }

        public string Nombre { get; set; }
        public string Marca { get; set; }
    }
}
