using ServicioDeSolicitudesDePrematricula.Dominio;
using ServicioDeSolicitudesDePrematricula.Errores;
using ServicioDeSolicitudesDePrematricula.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicioDeSolicitudesDePrematricula
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SolicitudMatriculas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SolicitudMatriculas.svc o SolicitudMatriculas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SolicitudMatriculas : ISolicitudMatriculas
    {
        private readonly SolicitudMatriculaDao DAO = new SolicitudMatriculaDao();

        public Dominio.SolicitudMatricula CrearSolicitud(Dominio.SolicitudMatricula solicitud)
        {
            if (DAO.ObtenerSolicitud(solicitud.DNI) != null)//validamos si el alumno ya existe
            {
                throw new FaultException<RepetidoException>(new RepetidoException() //de existir generamos una excepcion indicando lo sucedido
                {
                    Codigo = "101",
                    Descripcion = "El alumno ya existe"
                }, new FaultReason("Error al intentar creacion"));
            }
            return DAO.Crear(solicitud);
        }

        public Dominio.SolicitudMatricula ObtenerSolicitud(string dni)
        {
            return DAO.ObtenerSolicitud(dni);
        }

        public Dominio.SolicitudMatricula ModificarSolicitud(Dominio.SolicitudMatricula solicitud)
        {
            return DAO.Modificar(solicitud);
        }

        public void EliminarSolicitud(string dni)
        {

            SolicitudMatricula solicitudMatricula = DAO.ObtenerSolicitud(dni);
            if (solicitudMatricula != null && solicitudMatricula.EstadoSolicitud.Equals("C"))//validamos si la solicitud está cerrado
            {
                throw new FaultException<RepetidoException>(new RepetidoException() //de existir generamos una excepcion indicando lo sucedido
                {
                    Codigo = "103",
                    Descripcion = "No puede eliminar una solicitud aceptada."
                }, new FaultReason("Error al intentar creacion"));
            }

            DAO.Eliminar(dni);
        }

        public List<Dominio.SolicitudMatricula> ListarSolicitud()
        {
            return DAO.Listar();
        }

        public List<SolicitudMatricula> ListarGradoNivel(string nivel, string grado)
        {
            return DAO.ListarGradoNivel(nivel, grado);
        }
    }
}
