using ServicioDeSolicitudesDePrematricula.Dominio;
using ServicioDeSolicitudesDePrematricula.Errores;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServicioDeSolicitudesDePrematricula
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISolicitudMatriculas" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ISolicitudMatriculas
    {
        [FaultContract(typeof(RepetidoException))] // El manejador de errores
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Solicitud", ResponseFormat = WebMessageFormat.Json)]
        SolicitudMatricula CrearSolicitud(SolicitudMatricula solicitud);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Solicitud/{dniAlumno}", ResponseFormat = WebMessageFormat.Json)]
        SolicitudMatricula ObtenerSolicitud(String dniAlumno);
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Solicitud", ResponseFormat = WebMessageFormat.Json)]
        SolicitudMatricula ModificarSolicitud(SolicitudMatricula solicitud);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Solicitud/{dniAlumno}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarSolicitud(String dniAlumno);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Solicitud", ResponseFormat = WebMessageFormat.Json)]
        List<SolicitudMatricula> ListarSolicitud();
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Solicitud/{nivel}/{grado}", ResponseFormat = WebMessageFormat.Json)]
        List<SolicitudMatricula> ListarGradoNivel(string nivel, string grado);
    }
}
