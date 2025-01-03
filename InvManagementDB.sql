USE [master]
GO
/****** Object:  Database [InvManagement]    Script Date: 18/11/2023 4:34:11 p. m. ******/
CREATE DATABASE [InvManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InvManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\InvManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InvManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\InvManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [InvManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InvManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InvManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InvManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InvManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InvManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InvManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [InvManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InvManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InvManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InvManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InvManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InvManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InvManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InvManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InvManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InvManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InvManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InvManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InvManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InvManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InvManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InvManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InvManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InvManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InvManagement] SET  MULTI_USER 
GO
ALTER DATABASE [InvManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InvManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InvManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InvManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InvManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InvManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InvManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [InvManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [InvManagement]
GO
/****** Object:  Table [dbo].[Inventorys]    Script Date: 18/11/2023 4:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventorys](
	[Id_Inventory] [int] IDENTITY(1,1) NOT NULL,
	[Inv_Date] [datetime] NULL,
	[FinalDate] [datetime] NULL,
	[UserName] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Inventory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Inventorys]    Script Date: 18/11/2023 4:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Inventorys](
	[Code] [varchar](13) NULL,
	[Id_Inventory] [int] NULL,
	[Price] [float] NULL,
	[Margin] [float] NULL,
	[PriceToCost] [float] NULL,
	[Amount] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 18/11/2023 4:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Code] [varchar](13) NOT NULL,
	[Barcode] [varchar](13) NULL,
	[NameProduct] [varchar](60) NULL,
	[Reference] [varchar](15) NULL,
	[Brand] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 18/11/2023 4:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_Role] [int] IDENTITY(1,1) NOT NULL,
	[Description_Role] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18/11/2023 4:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Identification] [varchar](10) NOT NULL,
	[First_Name] [varchar](30) NULL,
	[Last_Name] [varchar](30) NULL,
	[Name_User] [varchar](30) NULL,
	[Id_Role] [int] NULL,
	[Email] [varchar](50) NULL,
	[User_Password] [varchar](70) NULL,
	[Statu] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[Identification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Inventorys] ON 

INSERT [dbo].[Inventorys] ([Id_Inventory], [Inv_Date], [FinalDate], [UserName]) VALUES (2, CAST(N'2023-12-01T00:00:00.000' AS DateTime), CAST(N'2023-12-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Inventorys] ([Id_Inventory], [Inv_Date], [FinalDate], [UserName]) VALUES (3, CAST(N'2024-01-06T00:00:00.000' AS DateTime), CAST(N'2024-01-11T00:00:00.000' AS DateTime), N'BRISAAC\braya')
SET IDENTITY_INSERT [dbo].[Inventorys] OFF
GO
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000001000074', N'0000001000074', N'SIERRA P/MADERA 24x3.0x25.4 56D IFEL', N'6460000', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000001000127', N'0000001000127', N'CAPUCHA PARA SOLDAR EN JEAN TIPO CHAVO VARIOS', N'', N'VARIOS')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000001000135', N'0000001000135', N'BROCA PARA METAL HSS DE 3.5mmx3" VARIOS', N'1234', N'VARIOS')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023012150', N'0000023012150', N'MANOMETRO SECO C2 1/2 0-150PSI ASTRO', N'7254800', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023020000', N'0000023020000', N'MANOMETRO GLICER C2 1/2 0-200PSI ASTRO', N'7260000', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023020150', N'0000023020150', N'MANOMETRO GLICERINA C2 1/2 0-150PSI ASTRO', N'7259800', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023020300', N'0000023020300', N'MANOMETRO GLICE C2 1/2 0-300PSI ASTRO', N'7260200', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023020500', N'0000023020500', N'MANOMETRO GLICE C2-1/2 0-5000PSI ASTRO', N'7261100', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023021100', N'0000023021100', N'MANOMETRO GLICERINA C2 1/2 0-100 PSI ASTRO', N'7259600', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000023021260', N'0000023021260', N'MANOMETRO GLICERINA C2 1/2 0-60PSI ASTRO', N'7259400', N'ASTRO')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026000006', N'0000026000006', N'PRENSA TIPO ALACRAN 6"x80  PARA CARPINTERIA IFEL', N'6476600', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026000007', N'0000026000007', N'PRENSA TIPO ALACRAN 8"x80 PARA CARPINTERIA IFEL', N'6476800', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026000011', N'0000026000011', N'PRENSA TIPO ALACRAN 20"x140 PARA CARPINTERIA IFEL', N'6477600', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026000013', N'0000026000013', N'PRENSA TIPO ALACRAN 32"x140 OPARA CARPINTERIA IFEL', N'6478000', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026000027', N'0000026000027', N'EXTRACTOR DE POLEAS 8 X 3 IFEL', N'6258600', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026050291', N'0000026050291', N'SACABOCADO DE GOLPE 16mm IFEL', N'6269616', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026050326', N'0000026050326', N'LLAVE PARA FILTRO DE ACEITE EN LONA IFEL', N'6257604', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026599411', N'0000026599411', N'RODEL COMPLETO 8mm IFEL-TOOLS', N'599411', N'IFEL-TOOLS')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026645980', N'0000026645980', N'DISCO SIERRA CIRCULAR P/MADERA 22X3.0X25.4 IFEL', N'6459800', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0000026646125', N'0000026646125', N'DISCO SIERRA CIRCULAR MADERA 26x3.5x25.4x56D IFEL', N'6460200', N'IFEL')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0503261704820', N'0503261704820', N'SIERRA COPA BIMETALICA 3" MORSE', N'5155946', N'MORSE')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0503261781290', N'0503261781290', N'SIERRA COPA BIMETALICA 3/4" 19mm MORSE', N'5155904', N'MORSE')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0503261781670', N'0503261781670', N'SIERRA COPA BIMETALICA 1" 25mm MORSE', N'5155910', N'MORSE')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'0503261783270', N'0503261783270', N'SIERRA COPA BIMETALICA DE 2" 51mm MORSE', N'5155931', N'MORSE')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'1074775069185', N'1074775069185', N'DESTORNILLADOR DE COPA 3/16" STANLEY', N'69-182', N'STANLEY')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'8413797019582', N'8413797019582', N'RODEL 5/16" 8mm RUBI-TOOLS', N'01958', N'RUBI-TOOLS')
INSERT [dbo].[Products] ([Code], [Barcode], [NameProduct], [Reference], [Brand]) VALUES (N'8711938361256', N'8711938361256', N'MARTILLO DE UÑA 29mm MANGO EN FIBRA DE VIDRIO RANGER', N'', N'RANGER')
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id_Role], [Description_Role]) VALUES (1, N'Administrador')
INSERT [dbo].[Roles] ([Id_Role], [Description_Role]) VALUES (2, N'Usuario')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[Users] ([Identification], [First_Name], [Last_Name], [Name_User], [Id_Role], [Email], [User_Password], [Statu]) VALUES (N'1003317250', N'Luis', N'Carretero', N'alejo', 2, N'luiscarretero@hotmail.com', N'luis1234', N'Habilitado')
INSERT [dbo].[Users] ([Identification], [First_Name], [Last_Name], [Name_User], [Id_Role], [Email], [User_Password], [Statu]) VALUES (N'1007835742', N'Sharick', N'Sanguino', N'kcirahs', 2, N'kcirahs19@gmail.com', N'sharick1234', N'Habilitado')
INSERT [dbo].[Users] ([Identification], [First_Name], [Last_Name], [Name_User], [Id_Role], [Email], [User_Password], [Statu]) VALUES (N'1066348730', N'Luis', N'Vega', N'luis301', 1, N'luisvm301@gmail.com', N'luis123', N'Habilitado')
INSERT [dbo].[Users] ([Identification], [First_Name], [Last_Name], [Name_User], [Id_Role], [Email], [User_Password], [Statu]) VALUES (N'1193587387', N'Brayan', N'Caro', N'brisaac', 1, N'brayancarob.2003@gmail.com', N'bicb2003', N'Habilitado')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__6C3F81BED7C73363]    Script Date: 18/11/2023 4:34:12 p. m. ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Name_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product_Inventorys]  WITH CHECK ADD FOREIGN KEY([Id_Inventory])
REFERENCES [dbo].[Inventorys] ([Id_Inventory])
GO
ALTER TABLE [dbo].[Product_Inventorys]  WITH CHECK ADD FOREIGN KEY([Code])
REFERENCES [dbo].[Products] ([Code])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Id_Role])
REFERENCES [dbo].[Roles] ([Id_Role])
GO
/****** Object:  StoredProcedure [dbo].[p_AddInventory]    Script Date: 18/11/2023 4:34:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_AddInventory]
	@Inv_Date DATETIME,
	@FinalDate DATETIME
AS
BEGIN
	INSERT INTO Inventorys(Inv_Date, FinalDate)
	VALUES(@Inv_Date, @FinalDate)
END;
GO
/****** Object:  StoredProcedure [dbo].[P_AddProduct]    Script Date: 18/11/2023 4:34:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_AddProduct]
    @Code VARCHAR(13),
    @Barcode VARCHAR(13),
    @NameProduct VARCHAR(60),
    @Reference VARCHAR(15), 
    @Brand VARCHAR(20)
AS
BEGIN
    INSERT INTO Products(Code, Barcode, NameProduct, Reference, Brand)
    VALUES (@Code, @Barcode, @NameProduct, @Reference, @Brand)
END;
GO
/****** Object:  StoredProcedure [dbo].[P_InsertUser]    Script Date: 18/11/2023 4:34:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_InsertUser]
    @Identification VARCHAR(10),
    @First_Name VARCHAR(30),
    @Last_Name VARCHAR(30),
    @Name_User VARCHAR(30), 
    @Id_Role INT,
    @Email VARCHAR(50),
    @User_Password VARCHAR(12),
	@Statu VARCHAR(15)
AS
BEGIN
    INSERT INTO Users (Identification, First_Name, Last_Name, Name_User, Id_Role, Email, User_Password, Statu)
    VALUES (@Identification, @First_Name, @Last_Name, @Name_User, @Id_Role, @Email, @User_Password, @Statu)
END;
GO
/****** Object:  StoredProcedure [dbo].[p_viewInventary]    Script Date: 18/11/2023 4:34:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[p_viewInventary]
	@Id_Inventory int
as
begin
	select p_i.Code, p.Barcode, p.Product_Name, p_i.Price, p_i.Margin, p_i.Amount, p_i.PriceToCost, p.Reference from PRODUCTS_INVENTORYS p_i join 
	INVENTORYS i on p_i.Id_Inventory = i.Id_Inventory  join PRODUCTS p on  p.Code= p_i.Code where i.Id_Inventory= p_i.Id_Inventory;
end;
GO
USE [master]
GO
ALTER DATABASE [InvManagement] SET  READ_WRITE 
GO
