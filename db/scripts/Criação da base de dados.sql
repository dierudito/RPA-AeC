USE [master]
GO
/****** Object:  Database [RpaAecDb]    Script Date: 3/29/2023 10:16:02 AM ******/
CREATE DATABASE [RpaAecDb]
GO
USE [RpaAecDb]
GO
CREATE TABLE [dbo].[Pesquisas](
	[Id] [uniqueidentifier] NOT NULL,
	[Termo] [varchar](100) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_Pesquisas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Pesquisas_Logs](
	[Id] [uniqueidentifier] NOT NULL,
	[PesquisaId] [uniqueidentifier] NOT NULL,
	[Descricao] [varchar](1000) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_Pesquisas_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Pesquisas_Resultados](
	[Id] [uniqueidentifier] NOT NULL,
	[PesquisaId] [uniqueidentifier] NOT NULL,
	[Titulo] [varchar](500) NULL,
	[Area] [varchar](100) NULL,
	[Autor] [varchar](200) NULL,
	[Descricao] [varchar](1000) NULL,
	[DataPublicacao] [date] NULL,
	[CapturadoTotalmente] [bit] NOT NULL,
	[AoMenosUmCapturado] [bit] NOT NULL,
 CONSTRAINT [PK_Pesquisas_Resultados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pesquisas_Logs]  WITH CHECK ADD  CONSTRAINT [FK_Pesquisas_Logs_Pesquisas] FOREIGN KEY([PesquisaId])
REFERENCES [dbo].[Pesquisas] ([Id])
GO
ALTER TABLE [dbo].[Pesquisas_Logs] CHECK CONSTRAINT [FK_Pesquisas_Logs_Pesquisas]
GO
ALTER TABLE [dbo].[Pesquisas_Resultados]  WITH CHECK ADD  CONSTRAINT [FK_Pesquisas_Resultados_Pesquisas] FOREIGN KEY([PesquisaId])
REFERENCES [dbo].[Pesquisas] ([Id])
GO
ALTER TABLE [dbo].[Pesquisas_Resultados] CHECK CONSTRAINT [FK_Pesquisas_Resultados_Pesquisas]
GO