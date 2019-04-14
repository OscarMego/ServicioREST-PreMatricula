using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicioDeSolicitudesDePrematricula.Errores
{
    [DataContract]
    public class RepetidoException
    {
        [DataMember]
        public String Codigo { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
    }
}