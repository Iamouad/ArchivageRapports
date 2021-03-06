USE [master]
GO
/****** Object:  Database [aspnet-realMiniProjet]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE DATABASE [aspnet-realMiniProjet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'aspnet-realMiniProjet-20200304111542.mdf', FILENAME = N'C:\Users\lenovo\Desktop\csharpProject\ReportsCsharp\realMiniProjet\App_Data\aspnet-realMiniProjet-20200304111542.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'aspnet-realMiniProjet-20200304111542_log.ldf', FILENAME = N'C:\Users\lenovo\Desktop\csharpProject\ReportsCsharp\realMiniProjet\App_Data\aspnet-realMiniProjet-20200304111542_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [aspnet-realMiniProjet] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
	EXEC [aspnet-realMiniProjet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [aspnet-realMiniProjet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET ARITHABORT OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET  ENABLE_BROKER 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET  MULTI_USER 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [aspnet-realMiniProjet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [aspnet-realMiniProjet] SET QUERY_STORE = OFF
GO
USE [aspnet-realMiniProjet]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory]
(
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
	CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchivedReports]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedReports]
(
	[Id_report] [int] IDENTITY(1,1) NOT NULL,
	[Id_prof] [nvarchar](128) NOT NULL,
	[Id_type] [int] NOT NULL,
	[Id_niv] [int] NOT NULL,
	[Id_filiere] [int] NOT NULL,
	[RemarqueProf] [nvarchar](512) NOT NULL,
	[ReportPath] [nvarchar](256) NOT NULL,
	[DateUniv] [nvarchar](256) NOT NULL,
	[Sujet] [nvarchar](256) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id_report] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles]
(
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
insert into AspNetRoles
	(Id,Name)
values
	(1, 'ADMIN'),
	(2, 'PROFESSOR'),
	(3, 'STUDENT');
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/13/2020 6:18:33 PM ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins]
(
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles]
(
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers]
(
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NULL,
	CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Filieres]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filieres]
(
	[Id_filiere] [int] IDENTITY(1,1) NOT NULL,
	[Nom_filiere] [nvarchar](128) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id_filiere] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
insert into Filieres
	(Nom_filiere)
values
	('G.Informatique'),
	('G.Industriel'),
	('G.Civil');
/****** Object:  Table [dbo].[Type_Reports]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_Reports]
(
	[Id_type] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](128) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
insert into Type_Reports
	(Type)
values
	('Rapport stage'),
	('Mini projet'),
	('Pfe');
/****** Object:  Table [dbo].[Groupes]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groupes]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_prof] [nvarchar](128) NOT NULL,
	[Id_fil] [int] NOT NULL,
	[Id_niv] [int] NOT NULL,
	[Delais] [datetime] NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Levels]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Levels]
(
	[Id_niveau] [int] IDENTITY(1,1) NOT NULL,
	[Nom_niveau] [nvarchar](128) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id_niveau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Script for SelectTopNRows command from SSMS  ******/
insert into Levels
	(Nom_niveau)
values
	('CI 1'),
	('CI 2'),
	('CI 3');
/****** Object:  Table [dbo].[Reports]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports]
(
	[Id_report] [int] IDENTITY(1,1) NOT NULL,
	[Id_prof] [nvarchar](128) NOT NULL,
	[Id_filiere] [int] NOT NULL,
	[Id_grp] [int] NOT NULL,
	[Id_type] [int] NOT NULL,
	[Id_niv] [int] NOT NULL,
	[ReportPath] [nvarchar](256) NOT NULL,
	[DateUniv] [nvarchar](256) NOT NULL,
	[DateDepot] [date] NOT NULL,
	[Sujet] [nvarchar](512) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id_report] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Cne] [nvarchar](20) NOT NULL,
	[Id_fil] [int] NOT NULL,
	[Id_niv] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students_ArchivedReport]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students_ArchivedReport]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_student] [int] NOT NULL,
	[Id_archivedReport] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students_Groupes]    Script Date: 3/13/2020 6:18:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students_Groupes]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_groupe] [int] NOT NULL,
	[Id_student] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 3/13/2020 6:18:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ArchivedReports]  WITH CHECK ADD FOREIGN KEY([Id_filiere])
REFERENCES [dbo].[Filieres] ([Id_filiere])
GO
ALTER TABLE [dbo].[ArchivedReports]  WITH CHECK ADD FOREIGN KEY([Id_type])
REFERENCES [dbo].[Type_Reports] ([Id_type])
GO
ALTER TABLE [dbo].[ArchivedReports]  WITH CHECK ADD FOREIGN KEY([Id_niv])
REFERENCES [dbo].[Levels] ([Id_niveau])
GO
ALTER TABLE [dbo].[ArchivedReports]  WITH CHECK ADD FOREIGN KEY([Id_prof])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Groupes]  WITH CHECK ADD FOREIGN KEY([Id_prof])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Groupes]  WITH CHECK ADD FOREIGN KEY([Id_fil])
REFERENCES [dbo].[Filieres] ([Id_filiere])
GO
ALTER TABLE [dbo].[Groupes]  WITH CHECK ADD FOREIGN KEY([Id_niv])
REFERENCES [dbo].[Levels] ([Id_niveau])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([Id_filiere])
REFERENCES [dbo].[Filieres] ([Id_filiere])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([Id_grp])
REFERENCES [dbo].[Groupes] ([Id])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([Id_type])
REFERENCES [dbo].[Type_Reports] ([Id_type])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([Id_niv])
REFERENCES [dbo].[Levels] ([Id_niveau])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([Id_prof])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([Id_fil])
REFERENCES [dbo].[Filieres] ([Id_filiere])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([Id_niv])
REFERENCES [dbo].[Levels] ([Id_niveau])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Students_ArchivedReport]  WITH CHECK ADD FOREIGN KEY([Id_archivedReport])
REFERENCES [dbo].[ArchivedReports] ([Id_report])
GO
ALTER TABLE [dbo].[Students_ArchivedReport]  WITH CHECK ADD FOREIGN KEY([Id_student])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Students_Groupes]  WITH CHECK ADD FOREIGN KEY([Id_groupe])
REFERENCES [dbo].[Groupes] ([Id])
GO
ALTER TABLE [dbo].[Students_Groupes]  WITH CHECK ADD FOREIGN KEY([Id_student])
REFERENCES [dbo].[Students] ([Id])
GO
USE [master]
GO
ALTER DATABASE [aspnet-realMiniProjet] SET  READ_WRITE 
GO

