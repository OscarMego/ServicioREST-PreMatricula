CREATE DATABASE BDPreMatricula
GO
USE [BDPreMatricula]
GO
/****** Object:  Table [dbo].[TBSolicitud]    Script Date: 31/03/2019 11:27:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBSolicitud](
	[IdSolicitud] [bigint] IDENTITY(1,1) NOT NULL,
	[NombreAlumno] [nvarchar](250) NULL,
	[ApellidosAlumno] [nvarchar](250) NULL,
	[DNIAlumno] [char](8) NULL,
	[Nivel] [char](1) NULL,
	[Grado] [char](1) NULL,
	[Fecha_solicitud] [date] NULL,
	[Fecha_visita] [date] NULL,
	[NombrePadreApoderado] [nvarchar](250) NULL,
	[DNIPadreApoderado] [nvarchar](250) NULL,
	[EmailPadreApoderado] [nvarchar](250) NULL,
	[EstadoSolicitud] [char](1) NULL,
	[FechaHoraRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSolicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
