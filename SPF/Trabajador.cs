using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF
{
    public class Trabajador
    {
        public string Cedula { get; set; }

        public DateTime fechaDeIngreso { get; set; }

        public string Nombre_Completo { get; set; }

        public string Departamento { get; set; }
        public string Cargo { get; set; }

        public string usuario { get; set; }
        public string contraseña { get; set; }
    }
}
