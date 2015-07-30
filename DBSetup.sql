

USE [master]
GO
/****** Object:  Database [McdellDataBase]    Script Date: 07/29/2015 21:07:41 ******/
CREATE DATABASE [McdellDataBase] ON  PRIMARY 
( NAME = N'McdellDataBase', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\McdellDataBase.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'McdellDataBase_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\McdellDataBase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [McdellDataBase] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [McdellDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [McdellDataBase] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [McdellDataBase] SET ANSI_NULLS OFF
GO
ALTER DATABASE [McdellDataBase] SET ANSI_PADDING OFF
GO
ALTER DATABASE [McdellDataBase] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [McdellDataBase] SET ARITHABORT OFF
GO
ALTER DATABASE [McdellDataBase] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [McdellDataBase] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [McdellDataBase] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [McdellDataBase] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [McdellDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [McdellDataBase] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [McdellDataBase] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [McdellDataBase] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [McdellDataBase] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [McdellDataBase] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [McdellDataBase] SET  DISABLE_BROKER
GO
ALTER DATABASE [McdellDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [McdellDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [McdellDataBase] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [McdellDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [McdellDataBase] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [McdellDataBase] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [McdellDataBase] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [McdellDataBase] SET  READ_WRITE
GO
ALTER DATABASE [McdellDataBase] SET RECOVERY SIMPLE
GO
ALTER DATABASE [McdellDataBase] SET  MULTI_USER
GO
ALTER DATABASE [McdellDataBase] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [McdellDataBase] SET DB_CHAINING OFF
GO
USE [McdellDataBase]
GO
/****** Object:  Table [dbo].[Catalogs]    Script Date: 07/29/2015 21:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Catalogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[ParentId] [int] NULL,
	
 CONSTRAINT [PK_Catalogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 07/29/2015 21:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 07/29/2015 21:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[Price] [money] NULL,
	[CatalogId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Catalogs_Catalogs]    Script Date: 07/29/2015 21:07:42 ******/
ALTER TABLE [dbo].[Catalogs]  WITH NOCHECK ADD  CONSTRAINT [FK_Catalogs_Catalogs] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Catalogs] ([Id])
GO
ALTER TABLE [dbo].[Catalogs] CHECK CONSTRAINT [FK_Catalogs_Catalogs]
GO

/****** Object:  ForeignKey [FK_Products_Catalog]    Script Date: 07/29/2015 21:07:42 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Catalog] FOREIGN KEY([CatalogId])
REFERENCES [dbo].[Catalogs] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Catalog]
GO



--truncate table catalogs
insert into Catalogs (Name) Values
('Television'),
('LCD'),
('LED'),
('Plasma'),
('Computers'),
('Tablets'),
('Tablet Sub-Cat'),
('High-Def')

go

update Catalogs 
set ParentId = (select Id from Catalogs where Name = 'Television')
where Name in ('LCD','LED','Plasma','High-Def')
go
update Catalogs 
set ParentId = (select Id from Catalogs where Name = 'Computers')
where Name in ('Tablets')
go
update Catalogs 
set ParentId = (select Id from Catalogs where Name = 'Tablets')
where Name in ('Tablet Sub-Cat')

go
insert into Products (Name,Description,Price,CatalogId)
values
('Sony LCD','LCD55888',29.95,2),
('Sony Plasma','Plasma',36.54,4),
('Sony LED','LED',54.00,3),
('Sony Plasma1','Plasma1',50.00,4),
('Sony LED1','LED1',50.00,3),
('Sony Tablet1','tablet1',50.00,6),
('Sony LCD','LCD',50.00,2),
('Sony Plasma2','Plasma2',50.00,4),
('Sony LED2','LED2',50.00,3),
('Sony Tablet2','tablet2',50.00,6),
('tablet prod A','table prod a',20.00,6),
('edit new LCD','this is the description',140.00,2),
('High def TV','a description',34.00,8),
('tablet 2','asdf asf ',123.50,7),
('tablet 3','tablet 3',123.00,7)
