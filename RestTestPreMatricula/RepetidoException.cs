using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicioDeSolicitudesDePrematricula.Errores
{
   
    public class RepetidoException
    {
        public String Codigo { get; set; }
        public String Descripcion { get; set; }
    }
}