using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicioDeSolicitudesDePrematricula.Dominio
{
    [DataContract]
    public class SolicitudMatricula
    {
        [DataMember]
        public Int64 IDSolicitud { get; set; }

        [DataMember]
        public String NombreAlumno { get; set; }
        [DataMember]
        public String ApellidosAlumno { get; set; }
        [DataMember]
        public String  DNI { get; set; }
        [DataMember]
        public char Nivel { get; set; }
        [DataMember]
        public char grado { get; set; }
        [DataMember]
        public DateTime FechaSolicitud{ get; set; }
        [DataMember]
        public DateTime FechaVisita { get; set; }
        [DataMember]
        public String NombrePadreApoderado { get; set; }
        [DataMember]
        public String DNIPadreApoderado { get; set; }
        [DataMember]
        public String EmailPadreApoderado { get; set; }
        [DataMember]
        public char EstadoSolicitud { get; set; }
        [DataMember]
        public DateTime FechaHoraRegistro { get; set; }
        
    }
}