USE [master]
GO
/****** Object:  Database [rgutelvt]    Script Date: 02/03/2023 23:32:04 ******/
CREATE DATABASE [rgutelvt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'rgutelvt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\rgutelvt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'rgutelvt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\rgutelvt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [rgutelvt] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [rgutelvt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [rgutelvt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [rgutelvt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [rgutelvt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [rgutelvt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [rgutelvt] SET ARITHABORT OFF 
GO
ALTER DATABASE [rgutelvt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [rgutelvt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [rgutelvt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [rgutelvt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [rgutelvt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [rgutelvt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [rgutelvt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [rgutelvt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [rgutelvt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [rgutelvt] SET  DISABLE_BROKER 
GO
ALTER DATABASE [rgutelvt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [rgutelvt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [rgutelvt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [rgutelvt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [rgutelvt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [rgutelvt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [rgutelvt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [rgutelvt] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [rgutelvt] SET  MULTI_USER 
GO
ALTER DATABASE [rgutelvt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [rgutelvt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [rgutelvt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [rgutelvt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [rgutelvt] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [rgutelvt] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [rgutelvt] SET QUERY_STORE = ON
GO
ALTER DATABASE [rgutelvt] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [rgutelvt]
GO
/****** Object:  Table [dbo].[detallestb]    Script Date: 02/03/2023 23:32:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detallestb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullname] [varchar](200) NULL,
	[surnames] [varchar](200) NULL,
	[specialty] [varchar](200) NULL,
	[faculty] [varchar](50) NULL,
	[CI] [varchar](50) NULL,
	[imgcarnet] [text] NULL,
	[iduser] [int] NULL,
	[dateupdate] [date] NULL,
	[datecreate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usertb]    Script Date: 02/03/2023 23:32:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usertb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](100) NULL,
	[passwords] [varchar](1000) NULL,
	[dateupdate] [date] NULL,
	[datecreate] [date] NULL,
	[numberverify] [text] NULL,
	[verifyuser] [int] NULL,
	[rol] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[detallestb] ON 

INSERT [dbo].[detallestb] ([id], [fullname], [surnames], [specialty], [faculty], [CI], [imgcarnet], [iduser], [dateupdate], [datecreate]) VALUES (1, N'Polk Brando', N'Vernaza Quiñonez', N'Tecnologia de la Informacion y Comunicacion', N'FACI', N'0850301110', N'img.png', 1, NULL, NULL)
INSERT [dbo].[detallestb] ([id], [fullname], [surnames], [specialty], [faculty], [CI], [imgcarnet], [iduser], [dateupdate], [datecreate]) VALUES (2, N'Joel Ramon', N'Gomez Obando', N'Tecnologia de la Informacion y Comunicacion', N'FACI', N'0950301110', N'img.png', 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[detallestb] OFF
GO
SET IDENTITY_INSERT [dbo].[usertb] ON 

INSERT [dbo].[usertb] ([id], [email], [passwords], [dateupdate], [datecreate], [numberverify], [verifyuser], [rol]) VALUES (1, N'polk@gmail.com', N'123', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[usertb] ([id], [email], [passwords], [dateupdate], [datecreate], [numberverify], [verifyuser], [rol]) VALUES (2, N'polk2@gmail.com', N'123', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[usertb] ([id], [email], [passwords], [dateupdate], [datecreate], [numberverify], [verifyuser], [rol]) VALUES (4, N'darelvernaza68@gmail.com', N'921DEF3BD9A50186F2B6E0B39258447357BE04804B2656111C19EC7D4857B54C', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[usertb] ([id], [email], [passwords], [dateupdate], [datecreate], [numberverify], [verifyuser], [rol]) VALUES (5, N'orsicen@gmail.com', N'921DEF3BD9A50186F2B6E0B39258447357BE04804B2656111C19EC7D4857B54C', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[usertb] OFF
GO
ALTER TABLE [dbo].[detallestb]  WITH CHECK ADD FOREIGN KEY([iduser])
REFERENCES [dbo].[usertb] ([id])
GO
USE [master]
GO
ALTER DATABASE [rgutelvt] SET  READ_WRITE 
GO
