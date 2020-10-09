USE [MyImageDB]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/9/2020 10:04:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CusId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[F_name] [varchar](255) NOT NULL,
	[L_name] [varchar](255) NOT NULL,
	[Dob] [date] NOT NULL,
	[Gender] [char](10) NULL,
	[Phone_No] [numeric](18, 0) NOT NULL,
	[Address] [varchar](255) NULL,
	[UserCus] [varchar](255) NOT NULL,
	[PassCus] [varchar](255) NOT NULL,
 CONSTRAINT [PK__Customer__2F1871105BB8D7DE] PRIMARY KEY CLUSTERED 
(
	[CusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 10/9/2020 10:04:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ImageId] [smallint] IDENTITY(1,1) NOT NULL,
	[ImageName] [varchar](255) NOT NULL,
 CONSTRAINT [PK__Images__7516F70CF1DA8879] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 10/9/2020 10:04:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserM] [varchar](255) NOT NULL,
	[PassM] [varchar](255) NOT NULL,
 CONSTRAINT [PK__Members__3214EC07B0AF7237] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 10/9/2020 10:04:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderId] [int] NOT NULL,
	[ImageId] [smallint] NOT NULL,
	[NumberOfPrints] [int] NOT NULL,
	[SizeId] [int] NOT NULL,
 CONSTRAINT [PK__OrderDet__14C134BF33ED6DD7] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ImageId] ASC,
	[SizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/9/2020 10:04:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CusId] [numeric](18, 0) NOT NULL,
	[FolderImage] [varchar](255) NULL,
	[Email_Id] [varchar](255) NULL,
	[CreditCardNumber] [numeric](18, 0) NULL,
 CONSTRAINT [PK__Orders__C3905BCF3E5AE6FE] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sizes]    Script Date: 10/9/2020 10:04:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sizes](
	[SizeId] [int] IDENTITY(1,1) NOT NULL,
	[Size] [varchar](50) NULL,
	[Price] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[SizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Image__1A14E395] FOREIGN KEY([ImageId])
REFERENCES [dbo].[Images] ([ImageId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Image__1A14E395]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__1920BF5C] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__1920BF5C]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Sizes] FOREIGN KEY([SizeId])
REFERENCES [dbo].[Sizes] ([SizeId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Sizes]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__CusId__145C0A3F] FOREIGN KEY([CusId])
REFERENCES [dbo].[Customers] ([CusId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__CusId__145C0A3F]
GO
