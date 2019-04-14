using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicioDeSolicitudesDePrematricula.Dominio;
using ServicioDeSolicitudesDePrematricula.Errores;
using System.Web.Script.Serialization;
namespace RestTestPreMatricula
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSolicitudCrear()
        {
            DateTime fecha = DateTime.Now;
            JavaScriptSerializer js = new JavaScriptSerializer();
            SolicitudMatricula solicitudMatricula = new SolicitudMatricula()
            {
                NombreAlumno = "Jorge",
                ApellidosAlumno = "Gonzales",
                DNI = "99551133",
                Nivel = 'S',
                grado = 'Q',
                FechaSolicitud = fecha,
                FechaVisita = fecha,
                NombrePadreApoderado = "ApoderadoName",
                DNIPadreApoderado = "99663322",
                EmailPadreApoderado = "aporderadoCorreo@correo.com",
                EstadoSolicitud = 'P'//Creamos solicitud en pendiente
            };

            string jsonSolicitud = js.Serialize(solicitudMatricula);
            byte[] ByteMatricula = Encoding.UTF8.GetBytes(jsonSolicitud);
            HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud") as HttpWebRequest;
            request.Method = "POST";
            request.ContentLength = ByteMatricula.Length;
            request.ContentType = "application/json";
            var rqt = request.GetRequestStream();

            rqt.Write(ByteMatricula, 0, ByteMatricula.Length);
            HttpWebResponse rsp = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(rsp.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            SolicitudMatricula solicitudCreado = js.Deserialize<SolicitudMatricula>(tramaJson);
            Assert.AreEqual("Jorge", solicitudCreado.NombreAlumno);
            Assert.AreEqual("Gonzales", solicitudCreado.ApellidosAlumno);
            Assert.AreEqual("99551133", solicitudCreado.DNI);
            Assert.AreEqual('S', solicitudCreado.Nivel);
            Assert.AreEqual('Q', solicitudCreado.grado);
            Assert.AreEqual(fecha.Date, solicitudCreado.FechaSolicitud.Date);
            Assert.AreEqual(fecha.Date, solicitudCreado.FechaVisita.Date);
            Assert.AreEqual("ApoderadoName", solicitudCreado.NombrePadreApoderado);
            Assert.AreEqual("99663322", solicitudCreado.DNIPadreApoderado);
            Assert.AreEqual("aporderadoCorreo@correo.com", solicitudCreado.EmailPadreApoderado);
            Assert.AreEqual('P', solicitudCreado.EstadoSolicitud);
        }
        [TestMethod]
        public void TestSolicitudCrearRepetido()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                DateTime fecha = DateTime.Now;
                SolicitudMatricula solicitudMatricula = new SolicitudMatricula()
                {
                    NombreAlumno = "Jorge",
                    ApellidosAlumno = "Gonzales",
                    DNI = "99551133",
                    Nivel = 'S',
                    grado = 'Q',
                    FechaSolicitud = fecha,
                    FechaVisita = fecha,
                    NombrePadreApoderado = "ApoderadoName",
                    DNIPadreApoderado = "99663322",
                    EmailPadreApoderado = "aporderadoCorreo@correo.com",
                    EstadoSolicitud = 'P'//Creamos solicitud en pendiente
                };

                string jsonSolicitud = js.Serialize(solicitudMatricula);
                byte[] ByteMatricula = Encoding.UTF8.GetBytes(jsonSolicitud);
                HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud") as HttpWebRequest;
                request.Method = "POST";
                request.ContentLength = ByteMatricula.Length;
                request.ContentType = "application/json";
                var rqt = request.GetRequestStream();
            }
            catch (WebException error)
            {
                HttpStatusCode codigo = ((HttpWebResponse)error.Response).StatusCode;
                StreamReader reader = new StreamReader(error.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException repetidoException = js.Deserialize<RepetidoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("101", repetidoException.Codigo);
                Assert.AreEqual("El alumno ya existe", repetidoException.Descripcion);
            }


        }
        [TestMethod]
        public void TestSolicitudObtener()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud/99551133") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            SolicitudMatricula solicitud = js.Deserialize<SolicitudMatricula>(tramaJson);
            Assert.AreEqual("Jorge", solicitud.NombreAlumno);
            Assert.AreEqual("Gonzales", solicitud.ApellidosAlumno);
            Assert.AreEqual("99551133", solicitud.DNI);
            Assert.AreEqual('S', solicitud.Nivel);
            Assert.AreEqual('Q', solicitud.grado);
            Assert.AreEqual("ApoderadoName", solicitud.NombrePadreApoderado);
            Assert.AreEqual("99663322", solicitud.DNIPadreApoderado);
            Assert.AreEqual("aporderadoCorreo@correo.com", solicitud.EmailPadreApoderado);
            Assert.AreEqual('P', solicitud.EstadoSolicitud);
        }
        [TestMethod]
        public void TestSolicitudModificar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            DateTime fecha = DateTime.Now;
            SolicitudMatricula solicitud = new SolicitudMatricula()
            {
                NombreAlumno = "Jorge 2",
                ApellidosAlumno = "Gonzales 2",
                DNI = "99551133",
                Nivel = 'S',
                grado = 'Q',
                FechaSolicitud = fecha,
                FechaVisita = fecha,
                NombrePadreApoderado = "ApoderadoName",
                DNIPadreApoderado = "99663322",
                EmailPadreApoderado = "aporderadoCorreo@correo.com",
                EstadoSolicitud = 'C'//CERRAMOS LA SOLICITUD
            };

            string jsonSolicitud = js.Serialize(solicitud);
            byte[] ByteMatricula = Encoding.UTF8.GetBytes(jsonSolicitud);
            HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud") as HttpWebRequest;
            request.Method = "PUT";
            request.ContentLength = ByteMatricula.Length;
            request.ContentType = "application/json";
            var rqt = request.GetRequestStream();
            rqt.Write(ByteMatricula, 0, ByteMatricula.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            SolicitudMatricula solicitudModificado = js.Deserialize<SolicitudMatricula>(tramaJson);
            Assert.AreEqual("Jorge 2", solicitudModificado.NombreAlumno);
            Assert.AreEqual("Gonzales 2", solicitudModificado.ApellidosAlumno);
            Assert.AreEqual("99551133", solicitudModificado.DNI);
            Assert.AreEqual('S', solicitudModificado.Nivel);
            Assert.AreEqual('Q', solicitudModificado.grado);
            Assert.AreEqual(fecha.Date, solicitudModificado.FechaSolicitud.Date);
            Assert.AreEqual(fecha.Date, solicitudModificado.FechaVisita.Date);
            Assert.AreEqual("ApoderadoName", solicitudModificado.NombrePadreApoderado);
            Assert.AreEqual("99663322", solicitudModificado.DNIPadreApoderado);
            Assert.AreEqual("aporderadoCorreo@correo.com", solicitudModificado.EmailPadreApoderado);
            Assert.AreEqual('C', solicitudModificado.EstadoSolicitud);
        }

        [TestMethod]
        public void TestSolicitudEliminar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest requestD = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud/88776655") as HttpWebRequest;
            requestD.Method = "DELETE";
            HttpWebResponse responseD = requestD.GetResponse() as HttpWebResponse;

            HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud/88776655") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            SolicitudMatricula solicitud = js.Deserialize<SolicitudMatricula>(tramaJson);
            Assert.IsNull(solicitud);

        }


        [TestMethod]
        public void TestSolicitudEliminarCerrado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                HttpWebRequest requestD = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud/99551133") as HttpWebRequest;
                requestD.Method = "DELETE";
                HttpWebResponse responseD = requestD.GetResponse() as HttpWebResponse;
            }
            catch (WebException error)
            {
                HttpStatusCode codigo = ((HttpWebResponse)error.Response).StatusCode;
                StreamReader reader = new StreamReader(error.Response.GetResponseStream());
                string tramaJson = reader.ReadToEnd();
                RepetidoException repetidoException = js.Deserialize<RepetidoException>(tramaJson);
                Assert.AreEqual(HttpStatusCode.Conflict, codigo);
                Assert.AreEqual("103", repetidoException.Codigo);
                Assert.AreEqual("No puede eliminar una solicitud aceptada.", repetidoException.Descripcion);
            }
        }


        [TestMethod]
        public void TestSolicitudListar()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            List<SolicitudMatricula> solicitud = js.Deserialize<List<SolicitudMatricula>>(tramaJson);
            Assert.AreEqual(2, solicitud.Count);
        }
        [TestMethod]
        public void TestSolicitudListarNivelGrado()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest request = WebRequest.Create("http://localhost:7981/SolicitudMatriculas.svc/Solicitud/P/S") as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            List<SolicitudMatricula> solicitud = js.Deserialize<List<SolicitudMatricula>>(tramaJson);
            Assert.AreEqual(1, solicitud.Count);
        }
    }
}
