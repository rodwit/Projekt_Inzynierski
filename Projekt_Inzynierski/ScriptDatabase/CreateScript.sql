USE [master]
GO
/****** Object:  Database [Program]    Script Date: 2019-12-05 22:16:21 ******/
CREATE DATABASE [Program]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Program', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Program.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Program_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Program_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Program] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Program].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Program] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Program] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Program] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Program] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Program] SET ARITHABORT OFF 
GO
ALTER DATABASE [Program] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Program] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Program] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Program] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Program] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Program] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Program] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Program] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Program] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Program] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Program] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Program] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Program] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Program] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Program] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Program] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Program] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Program] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Program] SET  MULTI_USER 
GO
ALTER DATABASE [Program] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Program] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Program] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Program] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Program] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Program] SET QUERY_STORE = OFF
GO
USE [Program]
GO
/****** Object:  Table [dbo].[Contractors]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contractors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_User] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Post_town] [nvarchar](50) NOT NULL,
	[Post_code] [varchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[NIP] [char](10) NULL,
	[REGON] [varchar](14) NULL,
	[PESEL] [char](11) NULL,
 CONSTRAINT [PK_Contractors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_Products]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_Products](
	[Id_invoice] [int] NOT NULL,
	[Id_product] [int] NOT NULL,
	[Id_tax] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Price] [decimal](19, 4) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_user] [int] NOT NULL,
	[Id_contractor] [int] NOT NULL,
	[Id_receiver] [int] NULL,
	[Issue_date] [date] NOT NULL,
	[Due_date] [date] NOT NULL,
	[Number] [int] NOT NULL,
	[Id_payment] [int] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment_Method]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment_Method](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Payment_Method] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_user] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Id_unit] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tax_rates]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tax_rates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](15) NOT NULL,
	[Value] [decimal](19, 4) NOT NULL,
 CONSTRAINT [PK_Tax_rates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Detail]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Detail](
	[Id_user] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Post_town] [nvarchar](50) NOT NULL,
	[Post_code] [varchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[NIP] [char](10) NOT NULL,
	[REGON] [varchar](14) NULL,
	[Bank] [nvarchar](50) NULL,
	[Bank_account] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[E-mail] [varchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Contractors] ON 

INSERT [dbo].[Contractors] ([Id], [Id_User], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [PESEL]) VALUES (1, 1, N'Mirek Inc.', N'Sosna', N'43-321', N'Sosnowiec', N'Miœka 100', N'5697412398', NULL, NULL)
INSERT [dbo].[Contractors] ([Id], [Id_User], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [PESEL]) VALUES (2, 1, N'Elektron Co.', N'Kraków', N'42-124', N'Kraków', N'Kazimierza 24', N'4634896512', NULL, NULL)
INSERT [dbo].[Contractors] ([Id], [Id_User], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [PESEL]) VALUES (3, 1, N'Kontrolka Janusz Kowalski', N'Wadowice', N'34-100', N'Wadowice', N'Jana Paw³a 10', N'5513695839', NULL, NULL)
INSERT [dbo].[Contractors] ([Id], [Id_User], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [PESEL]) VALUES (4, 1, N'BestPC Irena D³ugosz', N'Wroc³aw', N'55-100', N'Wroc³aw', N'Brzoza 12', N'9984021253', NULL, NULL)
INSERT [dbo].[Contractors] ([Id], [Id_User], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [PESEL]) VALUES (5, 1, N'"P4 SPÓ£KA Z OGRANICZON¥ ODPOWIEDZIALNOŒCI¥"', N'Warszawa', N'02-677', N'Warszawa', N'ul. Taœmowa 7', N'9512120077', NULL, NULL)
INSERT [dbo].[Contractors] ([Id], [Id_User], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [PESEL]) VALUES (6, 1, N'MORELE.NET SPÓ£KA Z OGRANICZON¥ ODPOWIEDZIALNOŒCI¥', N'Kraków', N'31-553', N'Kraków', N'ul. Fabryczna 20A', N'9451972201', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Contractors] OFF
INSERT [dbo].[Invoice_Products] ([Id_invoice], [Id_product], [Id_tax], [Amount], [Price]) VALUES (16, 9, 1, 1, CAST(1219.5100 AS Decimal(19, 4)))
INSERT [dbo].[Invoice_Products] ([Id_invoice], [Id_product], [Id_tax], [Amount], [Price]) VALUES (16, 10, 1, 10, CAST(105.6900 AS Decimal(19, 4)))
INSERT [dbo].[Invoice_Products] ([Id_invoice], [Id_product], [Id_tax], [Amount], [Price]) VALUES (13, 9, 1, 100, CAST(1219.5100 AS Decimal(19, 4)))
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([Id], [Id_user], [Id_contractor], [Id_receiver], [Issue_date], [Due_date], [Number], [Id_payment]) VALUES (13, 1, 3, NULL, CAST(N'2019-10-25' AS Date), CAST(N'2019-11-25' AS Date), 1, 2)
INSERT [dbo].[Invoices] ([Id], [Id_user], [Id_contractor], [Id_receiver], [Issue_date], [Due_date], [Number], [Id_payment]) VALUES (16, 1, 4, NULL, CAST(N'2019-12-04' AS Date), CAST(N'2019-12-04' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[Invoices] OFF
SET IDENTITY_INSERT [dbo].[Payment_Method] ON 

INSERT [dbo].[Payment_Method] ([Id], [Name]) VALUES (1, N'Gotówka')
INSERT [dbo].[Payment_Method] ([Id], [Name]) VALUES (2, N'Przelew')
SET IDENTITY_INSERT [dbo].[Payment_Method] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (2, 1, N'Metr', 1)
INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (4, 1, N'Maka', 6)
INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (6, 2, N'Cola', 7)
INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (7, 1, N'Ser', 1)
INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (8, 2, N'Ser', 1)
INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (9, 1, N'Ryzen 5 3700X', 1)
INSERT [dbo].[Products] ([Id], [Id_user], [Name], [Id_unit]) VALUES (10, 1, N'Led 300', 8)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Tax_rates] ON 

INSERT [dbo].[Tax_rates] ([Id], [Name], [Value]) VALUES (1, N'23%', CAST(0.2300 AS Decimal(19, 4)))
INSERT [dbo].[Tax_rates] ([Id], [Name], [Value]) VALUES (2, N'8%', CAST(0.0800 AS Decimal(19, 4)))
INSERT [dbo].[Tax_rates] ([Id], [Name], [Value]) VALUES (3, N'5%', CAST(0.0500 AS Decimal(19, 4)))
INSERT [dbo].[Tax_rates] ([Id], [Name], [Value]) VALUES (4, N'0%', CAST(0.0000 AS Decimal(19, 4)))
INSERT [dbo].[Tax_rates] ([Id], [Name], [Value]) VALUES (5, N'zw.', CAST(0.0000 AS Decimal(19, 4)))
SET IDENTITY_INSERT [dbo].[Tax_rates] OFF
SET IDENTITY_INSERT [dbo].[Units] ON 

INSERT [dbo].[Units] ([Id], [Name]) VALUES (1, N'szt')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (2, N'zestaw')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (3, N'h')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (4, N'mb')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (5, N'kpl')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (6, N'kg')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (7, N'l')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (8, N'm')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (9, N'm2')
INSERT [dbo].[Units] ([Id], [Name]) VALUES (10, N'm3')
SET IDENTITY_INSERT [dbo].[Units] OFF
INSERT [dbo].[User_Detail] ([Id_user], [Name], [Post_town], [Post_code], [City], [Street], [NIP], [REGON], [Bank], [Bank_account]) VALUES (1, N'Frima Widmo', N'Kraków', N'32-123', N'Kraków', N'Mikolaja 15', N'5361256475', NULL, N'Alior', N'Nr. konta bankowego :D')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [E-mail], [Password]) VALUES (1, N't@t', N't')
INSERT [dbo].[Users] ([id], [E-mail], [Password]) VALUES (2, N'r@r', N'r')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT ((1)) FOR [Id_payment]
GO
ALTER TABLE [dbo].[Invoice_Products]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Products_Invoices] FOREIGN KEY([Id_invoice])
REFERENCES [dbo].[Invoices] ([Id])
GO
ALTER TABLE [dbo].[Invoice_Products] CHECK CONSTRAINT [FK_Invoice_Products_Invoices]
GO
ALTER TABLE [dbo].[Invoice_Products]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Products_Products] FOREIGN KEY([Id_product])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Invoice_Products] CHECK CONSTRAINT [FK_Invoice_Products_Products]
GO
ALTER TABLE [dbo].[Invoice_Products]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Products_Tax_rates] FOREIGN KEY([Id_tax])
REFERENCES [dbo].[Tax_rates] ([Id])
GO
ALTER TABLE [dbo].[Invoice_Products] CHECK CONSTRAINT [FK_Invoice_Products_Tax_rates]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Users] FOREIGN KEY([Id_user])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Users]
GO
ALTER TABLE [dbo].[User_Detail]  WITH CHECK ADD  CONSTRAINT [FK_User_Detail_Users] FOREIGN KEY([Id_user])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[User_Detail] CHECK CONSTRAINT [FK_User_Detail_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddContractor]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddContractor]
	@userId int,
	@name nvarchar(50) = '%',
	@postTown nvarchar(50) = '%',
	@postCode varchar(50) = '%',
	@city nvarchar(50) = '%',
	@street nvarchar(50) = '%',
	@nip char(50) = '%',
	@regon char(50) = '%',
	@pesel char(50) = '%'
AS
BEGIN
	if (@nip = 'null') set @nip = null
	if (@regon= 'null') set @regon = null
	if (@pesel = 'null') set @pesel = null
	insert into Contractors values(@userId,@name, @postTown, @postCode, @city, @street,@nip,@regon,@pesel)
end
GO
/****** Object:  StoredProcedure [dbo].[AddInvoice]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddInvoice]
	@userId int,
	@contractorId int,
	@recieverId int,
	@issueDate date,
	@dueDate date,
	@paymentId int,
	@result int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @first date;
	declare @last date;
	declare @table table (idx int identity(1,1), numb int);
	if (@recieverId = -1) set @recieverId = null;

	set @first = DATEADD(month, DATEDIFF(month, 0, @issueDate), 0);
	set @last = EOMONTH(@issueDate);
	insert into @table 
	select Number from Invoices where Id_user=@userId and  Issue_date between @first and @last order by Number asc;

	declare @counter int;
	declare @max int;
	set @counter = 1;
	set @max = (select max(idx) from @table);
	while(@counter <= @max)
	begin
		DECLARE @colVar INT;
		SELECT @colVar = numb FROM @table WHERE idx = @counter;
		if (@counter != @colVar) break;
		SET @counter = @counter + 1
	end
	print @counter;

	insert into Invoices values (@userId,@contractorId, @recieverId, @issueDate, @dueDate, @counter,@paymentId);

	set @result = (select Id from Invoices where Id_user = @userId and Id_contractor = @contractorId
	and Issue_date = @issueDate and Due_date = @dueDate and Number = @counter);

	return @result;
END
GO
/****** Object:  StoredProcedure [dbo].[EditContractor]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EditContractor] 
	@userId int,
	@name nvarchar(50) = '%',
	@postTown nvarchar(50) = '%',
	@postCode varchar(50) = '%',
	@city nvarchar(50) = '%',
	@street nvarchar(50) = '%',
	@nip char(50) = '%',
	@regon char(50) = '%',
	@pesel char(50) = '%'
AS
BEGIN
	if (@nip = 'null') set @nip = null
	if (@regon= 'null') set @regon = null
	if (@pesel = 'null') set @pesel = null
	update Contractors set Name = @name,Post_town= @postTown,Post_code= @postCode,City= @city,Street= @street,NIP= @nip,REGON= @regon,PESEL= @pesel 
	where Id = @userId
end
GO
/****** Object:  StoredProcedure [dbo].[LogInUser]    Script Date: 2019-12-05 22:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LogInUser] 
	@e_mail varchar(50) = '%',
	@password nvarchar(50) = '%',
	@result varchar(50) = '%' out
AS
BEGIN
	set @result = (select Users.id from Users where [E-mail] = @e_mail and Password = @password)
	if @result is null
	begin
		set @result = 'null'
	end
return 0
end
GO
USE [master]
GO
ALTER DATABASE [Program] SET  READ_WRITE 
GO
