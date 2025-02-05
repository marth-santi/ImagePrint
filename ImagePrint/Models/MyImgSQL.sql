USE [MyImageDB]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/11/2020 10:22:52 AM ******/
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
/****** Object:  Table [dbo].[Images]    Script Date: 10/11/2020 10:22:52 AM ******/
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
/****** Object:  Table [dbo].[Members]    Script Date: 10/11/2020 10:22:52 AM ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 10/11/2020 10:22:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderId] [int] NOT NULL,
	[ImageId] [smallint] NOT NULL,
	[NumberOfPrints] [int] NOT NULL,
	[SizeId] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/11/2020 10:22:52 AM ******/
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
	[IsComplete] [bit] NOT NULL,
 CONSTRAINT [PK__Orders__C3905BCF3E5AE6FE] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sizes]    Script Date: 10/11/2020 10:22:52 AM ******/
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
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CusId], [F_name], [L_name], [Dob], [Gender], [Phone_No], [Address], [UserCus], [PassCus]) VALUES (CAST(1 AS Numeric(18, 0)), N'Nguyen', N'Tam', CAST(N'1986-02-15' AS Date), N'Male      ', CAST(903014696 AS Numeric(18, 0)), N'294 Pham Van Hai F5 Quan Tan Binh, TPHCM', N'tam', N'202cb962ac59075b964b07152d234b70')
INSERT [dbo].[Customers] ([CusId], [F_name], [L_name], [Dob], [Gender], [Phone_No], [Address], [UserCus], [PassCus]) VALUES (CAST(3 AS Numeric(18, 0)), N'Nguyen', N'Tam', CAST(N'1986-02-15' AS Date), N'N         ', CAST(909090909 AS Numeric(18, 0)), N'HCM', N'vantam1', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Customers] ([CusId], [F_name], [L_name], [Dob], [Gender], [Phone_No], [Address], [UserCus], [PassCus]) VALUES (CAST(4 AS Numeric(18, 0)), N'Nguyen', N'e', CAST(N'2010-12-20' AS Date), N'Male      ', CAST(909090909 AS Numeric(18, 0)), NULL, N'vantam2', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Customers] ([CusId], [F_name], [L_name], [Dob], [Gender], [Phone_No], [Address], [UserCus], [PassCus]) VALUES (CAST(5 AS Numeric(18, 0)), N'Nguyen', N'a', CAST(N'1999-11-22' AS Date), N'Male      ', CAST(32323 AS Numeric(18, 0)), N'HCM', N'vantam3', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Customers] ([CusId], [F_name], [L_name], [Dob], [Gender], [Phone_No], [Address], [UserCus], [PassCus]) VALUES (CAST(6 AS Numeric(18, 0)), N'a', N'z', CAST(N'2000-01-01' AS Date), N'Male      ', CAST(90909090 AS Numeric(18, 0)), N'zxzcva', N'vn', N'202cb962ac59075b964b07152d234b70')
INSERT [dbo].[Customers] ([CusId], [F_name], [L_name], [Dob], [Gender], [Phone_No], [Address], [UserCus], [PassCus]) VALUES (CAST(7 AS Numeric(18, 0)), N'z', N'z', CAST(N'2001-01-01' AS Date), N'Male      ', CAST(80808080 AS Numeric(18, 0)), N'eee', N'zzz', N'202cb962ac59075b964b07152d234b70')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([ImageId], [ImageName]) VALUES (29, N'~/Content/image/image_print/6/RE4xtka_1920x1080.jpg')
INSERT [dbo].[Images] ([ImageId], [ImageName]) VALUES (30, N'~/Content/image/image_print/6/RE4tkvW_1920x1080.jpg')
SET IDENTITY_INSERT [dbo].[Images] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 

INSERT [dbo].[Members] ([Id], [UserM], [PassM]) VALUES (1, N'tam', N'202cb962ac59075b964b07152d234b70')
INSERT [dbo].[Members] ([Id], [UserM], [PassM]) VALUES (2, N'duc', N'202cb962ac59075b964b07152d234b70')
INSERT [dbo].[Members] ([Id], [UserM], [PassM]) VALUES (4, N'vn', N'202cb962ac59075b964b07152d234b70')
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ImageId], [NumberOfPrints], [SizeId]) VALUES (43, 29, 5, 1)
INSERT [dbo].[OrderDetails] ([OrderId], [ImageId], [NumberOfPrints], [SizeId]) VALUES (43, 30, 8, 1)
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [CusId], [FolderImage], [Email_Id], [CreditCardNumber], [IsComplete]) VALUES (38, CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, 0)
INSERT [dbo].[Orders] ([OrderId], [CusId], [FolderImage], [Email_Id], [CreditCardNumber], [IsComplete]) VALUES (41, CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, 0)
INSERT [dbo].[Orders] ([OrderId], [CusId], [FolderImage], [Email_Id], [CreditCardNumber], [IsComplete]) VALUES (42, CAST(5 AS Numeric(18, 0)), NULL, NULL, NULL, 0)
INSERT [dbo].[Orders] ([OrderId], [CusId], [FolderImage], [Email_Id], [CreditCardNumber], [IsComplete]) VALUES (43, CAST(6 AS Numeric(18, 0)), NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Sizes] ON 

INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (1, N'3.5 x 5', CAST(3.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (2, N'4 x 6', CAST(4.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (3, N'5 x 7', CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (4, N'6 x 8', CAST(6.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (5, N'8 x 10', CAST(7.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (6, N'10 x 12', CAST(8.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (7, N'10 x 15', CAST(9.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (8, N'11 x 14', CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (9, N'11 x 17', CAST(11.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (10, N'12 x 15', CAST(12.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (11, N'12 x 18', CAST(13.00 AS Decimal(5, 2)))
INSERT [dbo].[Sizes] ([SizeId], [Size], [Price]) VALUES (12, N'20 x 30', CAST(14.00 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[Sizes] OFF
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
