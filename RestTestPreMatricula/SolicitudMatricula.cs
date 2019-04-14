using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicioDeSolicitudesDePrematricula.Dominio
{
    public class SolicitudMatricula
    {
        public Int64 IDSolicitud { get; set; }

        public String NombreAlumno { get; set; }

        public String ApellidosAlumno { get; set; }

        public String DNI { get; set; }

        public char Nivel { get; set; }

        public char grado { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public DateTime FechaVisita { get; set; }

        public String NombrePadreApoderado { get; set; }

        public String DNIPadreApoderado { get; set; }

        public String EmailPadreApoderado { get; set; }

        public char EstadoSolicitud { get; set; }

        public DateTime FechaHoraRegistro { get; set; }
    }
}