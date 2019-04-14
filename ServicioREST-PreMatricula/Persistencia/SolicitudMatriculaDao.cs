using ServicioDeSolicitudesDePrematricula.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


namespace ServicioDeSolicitudesDePrematricula.Persistencia
{
    public class SolicitudMatriculaDao
    {
        private string conexion = "Data Source=OSCAR-PC;Initial Catalog=BDPreMatricula;Integrated Security=True";
        public SolicitudMatricula Crear(SolicitudMatricula solicitud)
        {
            SolicitudMatricula solicitudCreado = null;
            String sql = "INSERT  INTO [dbo].[TBSolicitud]( [NombreAlumno] ,[ApellidosAlumno] ,[DNIAlumno] ,[Nivel] ,[Grado] ,[Fecha_solicitud] ,[Fecha_visita] ,[NombrePadreApoderado] ,[DNIPadreApoderado] ,[EmailPadreApoderado] ,[EstadoSolicitud] ,[FechaHoraRegistro])";
            sql = sql + " VALUES  ( @NombreAlumno ,@ApellidosAlumno ,@DNIAlumno ,@Nivel ,@Grado ,@Fecha_solicitud ,@Fecha_visita ,@NombrePadreApoderado ,@DNIPadreApoderado ,@EmailPadreApoderado ,@EstadoSolicitud ,GETDATE());";
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();
                using (SqlCommand comando = new SqlCommand(sql, cn))
                {
                    comando.Parameters.Add(new SqlParameter("@NombreAlumno", solicitud.NombreAlumno));
                    comando.Parameters.Add(new SqlParameter("@ApellidosAlumno", solicitud.ApellidosAlumno));
                    comando.Parameters.Add(new SqlParameter("@DNIAlumno", solicitud.DNI));
                    comando.Parameters.Add(new SqlParameter("@Nivel", solicitud.Nivel));
                    comando.Parameters.Add(new SqlParameter("@Grado", solicitud.grado));
                    comando.Parameters.Add(new SqlParameter("@Fecha_solicitud", solicitud.FechaSolicitud));
                    comando.Parameters.Add(new SqlParameter("@Fecha_visita", solicitud.FechaVisita));
                    comando.Parameters.Add(new SqlParameter("@NombrePadreApoderado", solicitud.NombrePadreApoderado));
                    comando.Parameters.Add(new SqlParameter("@DNIPadreApoderado", solicitud.DNIPadreApoderado));
                    comando.Parameters.Add(new SqlParameter("@EmailPadreApoderado", solicitud.EmailPadreApoderado));
                    comando.Parameters.Add(new SqlParameter("@EstadoSolicitud", solicitud.EstadoSolicitud));
                    comando.ExecuteNonQuery();
                }

            }
            solicitudCreado = ObtenerSolicitud(solicitud.DNI);
            return solicitudCreado;
        }
        public SolicitudMatricula ObtenerSolicitud(String dni)
        {
            SolicitudMatricula solicitud = null;
            String sql = "SELECT [IdSolicitud],[NombreAlumno],[ApellidosAlumno],[DNIAlumno],[Nivel],[Grado],[Fecha_solicitud],[Fecha_visita],[NombrePadreApoderado],[DNIPadreApoderado],[EmailPadreApoderado],[EstadoSolicitud],[FechaHoraRegistro] FROM [dbo].[TBSolicitud] WHERE DNIAlumno=@dni";

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();
                using (SqlCommand comando = new SqlCommand(sql, cn))
                {
                    comando.Parameters.Add(new SqlParameter("@dni", dni));
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            solicitud = new SolicitudMatricula()
                            {
                                IDSolicitud = (Int64)dr["IdSolicitud"],
                                NombreAlumno = dr["NombreAlumno"].ToString(),
                                ApellidosAlumno = dr["ApellidosAlumno"].ToString(),
                                DNI = dr["DNIAlumno"].ToString(),
                                Nivel = Char.Parse(dr["Nivel"].ToString()),
                                grado = Char.Parse(dr["Grado"].ToString()),
                                FechaSolicitud = DateTime.Parse(dr["Fecha_solicitud"].ToString()),
                                FechaVisita = DateTime.Parse(dr["Fecha_visita"].ToString()),
                                NombrePadreApoderado = dr["NombrePadreApoderado"].ToString(),
                                DNIPadreApoderado = dr["DNIPadreApoderado"].ToString(),
                                EmailPadreApoderado = dr["EmailPadreApoderado"].ToString(),
                                EstadoSolicitud = Char.Parse(dr["EstadoSolicitud"].ToString()),
                                FechaHoraRegistro = DateTime.Parse(dr["FechaHoraRegistro"].ToString())
                            };
                        }
                    }
                }
            }
            return solicitud;
        }
        public SolicitudMatricula Modificar(SolicitudMatricula solicitud)
        {
            SolicitudMatricula solicitudMod = null;
            String sql = "UPDATE [dbo].[TBSolicitud]SET [NombreAlumno] = @NombreAlumno,[ApellidosAlumno] = @ApellidosAlumno,[Nivel] = @Nivel,[Grado] = @Grado,[Fecha_solicitud] = @Fecha_solicitud,[Fecha_visita] = @Fecha_visita,[NombrePadreApoderado] = @NombrePadreApoderado,[DNIPadreApoderado] = @DNIPadreApoderado,[EmailPadreApoderado] = @EmailPadreApoderado,[EstadoSolicitud] = @EstadoSolicitud WHERE  [DNIAlumno] = @DNIAlumno";
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();
                using (SqlCommand comando = new SqlCommand(sql, cn))
                {
                    comando.Parameters.Add(new SqlParameter("@NombreAlumno", solicitud.NombreAlumno));
                    comando.Parameters.Add(new SqlParameter("@ApellidosAlumno", solicitud.ApellidosAlumno));
                    comando.Parameters.Add(new SqlParameter("@DNIAlumno", solicitud.DNI));
                    comando.Parameters.Add(new SqlParameter("@Nivel", solicitud.Nivel));
                    comando.Parameters.Add(new SqlParameter("@Grado", solicitud.grado));
                    comando.Parameters.Add(new SqlParameter("@Fecha_solicitud", solicitud.FechaSolicitud));
                    comando.Parameters.Add(new SqlParameter("@Fecha_visita", solicitud.FechaVisita));
                    comando.Parameters.Add(new SqlParameter("@NombrePadreApoderado", solicitud.NombrePadreApoderado));
                    comando.Parameters.Add(new SqlParameter("@DNIPadreApoderado", solicitud.DNIPadreApoderado));
                    comando.Parameters.Add(new SqlParameter("@EmailPadreApoderado", solicitud.EmailPadreApoderado));
                    comando.Parameters.Add(new SqlParameter("@EstadoSolicitud", solicitud.EstadoSolicitud));
                    comando.ExecuteNonQuery();
                }

            }
            solicitudMod = ObtenerSolicitud(solicitud.DNI);
            return solicitudMod;
        }
        public void Eliminar(String dni)
        {
            String sql = "DELETE FROM [TBSolicitud] WHERE  [DNIAlumno] = @DNIAlumno";
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();
                using (SqlCommand comando = new SqlCommand(sql, cn))
                {
                    comando.Parameters.Add(new SqlParameter("@DNIAlumno", dni));
                    comando.ExecuteNonQuery();
                }
            }
        }
        public List<SolicitudMatricula> Listar()
        {
            String sql = "SELECT * FROM [TBSolicitud]";
            List<SolicitudMatricula> listSolicitud = null;
            SolicitudMatricula solicitud = null;
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();
                using (SqlCommand comando = new SqlCommand(sql, cn))
                {
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        listSolicitud = new List<SolicitudMatricula>();

                        while (dr.Read())
                        {
                            solicitud = new SolicitudMatricula()
                            {
                                IDSolicitud = (Int64)dr["IdSolicitud"],
                                NombreAlumno = dr["IdSolicitud"].ToString(),
                                ApellidosAlumno = dr["NombreAlumno"].ToString(),
                                DNI = dr["DNIAlumno"].ToString(),
                                Nivel = Char.Parse(dr["Nivel"].ToString()),
                                grado = Char.Parse(dr["Grado"].ToString()),
                                FechaSolicitud = DateTime.Parse(dr["Fecha_solicitud"].ToString()),
                                FechaVisita = DateTime.Parse(dr["Fecha_visita"].ToString()),
                                NombrePadreApoderado = dr["NombrePadreApoderado"].ToString(),
                                DNIPadreApoderado = dr["DNIPadreApoderado"].ToString(),
                                EmailPadreApoderado = dr["EmailPadreApoderado"].ToString(),
                                EstadoSolicitud = Char.Parse(dr["EstadoSolicitud"].ToString()),
                                FechaHoraRegistro = DateTime.Parse(dr["FechaHoraRegistro"].ToString())
                            };
                            listSolicitud.Add(solicitud);
                        }
                    }
                }
            }
            return listSolicitud;
        }
        public List<SolicitudMatricula> ListarGradoNivel(string nivel, string grado)
        {
            String sql = "SELECT * FROM [TBSolicitud] where Grado=@Grado and Nivel=@Nivel";
            List<SolicitudMatricula> listSolicitud = null;
            SolicitudMatricula solicitud = null;
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();
                using (SqlCommand comando = new SqlCommand(sql, cn))
                {
                    comando.Parameters.Add(new SqlParameter("@Nivel", nivel));
                    comando.Parameters.Add(new SqlParameter("@Grado", grado));
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        listSolicitud = new List<SolicitudMatricula>();

                        while (dr.Read())
                        {
                            solicitud = new SolicitudMatricula()
                            {
                                IDSolicitud = (Int64)dr["IdSolicitud"],
                                NombreAlumno = dr["IdSolicitud"].ToString(),
                                ApellidosAlumno = dr["NombreAlumno"].ToString(),
                                DNI = dr["DNIAlumno"].ToString(),
                                Nivel = Char.Parse(dr["Nivel"].ToString()),
                                grado = Char.Parse(dr["Grado"].ToString()),
                                FechaSolicitud = DateTime.Parse(dr["Fecha_solicitud"].ToString()),
                                FechaVisita = DateTime.Parse(dr["Fecha_visita"].ToString()),
                                NombrePadreApoderado = dr["NombrePadreApoderado"].ToString(),
                                DNIPadreApoderado = dr["DNIPadreApoderado"].ToString(),
                                EmailPadreApoderado = dr["EmailPadreApoderado"].ToString(),
                                EstadoSolicitud = Char.Parse(dr["EstadoSolicitud"].ToString()),
                                FechaHoraRegistro = DateTime.Parse(dr["FechaHoraRegistro"].ToString())
                            };
                            listSolicitud.Add(solicitud);
                        }
                    }
                }
            }
            return listSolicitud;
        }
    }
}