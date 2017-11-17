CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 17/11/2017 13:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Barcode] [nvarchar](13) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shop]    Script Date: 17/11/2017 13:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shop](
	[ShopId] [int] IDENTITY(1,1) NOT NULL,
	[ShopAddress] [nvarchar](100) NULL,
	[ShopCity] [nvarchar](100) NULL,
	[ShopName] [nvarchar](100) NOT NULL,
	[ShopPhoneNumber] [nvarchar](max) NULL,
	[ShopPostCode] [nvarchar](5) NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShopProduct]    Script Date: 17/11/2017 13:54:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShopProduct](
	[ShopId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ShopProduct] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171101110215_Initial', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171103191812_Shop', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171108193037_ShopProduct', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171110115358_ShopProductKey', N'2.0.0-rtm-26452')
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'2860144021617', N'Banane Cavendish (Bio Village Equitable)', 1)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'3252920034239', N'Lait Bio en poudre Grandeur Nature', 2)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'5411188124337', N'Soya cuisine Provamel', 3)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'4016249140700', N'Hof-Pastete (Pâté végétal) Champignon Allos', 4)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'7610313412815', N'Bouillon de légumes Herbamare', 5)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'3229820174891', N'Terrine aubergine bio', 8007)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'3380390036405', N'Maïs Pop Corn Bio - 500G - Priméal', 8009)
INSERT [dbo].[Product] ([Barcode], [ProductName], [ProductId]) VALUES (N'3307130802557', N'Petits pois très fins et Carottes à l''étuvée', 9014)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Shop] ON 

INSERT [dbo].[Shop] ([ShopId], [ShopAddress], [ShopCity], [ShopName], [ShopPhoneNumber], [ShopPostCode]) VALUES (1, NULL, N'INGERSHEIM', N'BioCoop Unis Vers Bio', NULL, N'68040')
INSERT [dbo].[Shop] ([ShopId], [ShopAddress], [ShopCity], [ShopName], [ShopPhoneNumber], [ShopPostCode]) VALUES (2, NULL, N'LOGELBACH', N'Naturéo', NULL, N'68124')
SET IDENTITY_INSERT [dbo].[Shop] OFF
INSERT [dbo].[ShopProduct] ([ShopId], [Price], [ProductId]) VALUES (1, CAST(4.70 AS Decimal(18, 2)), 8007)
INSERT [dbo].[ShopProduct] ([ShopId], [Price], [ProductId]) VALUES (1, CAST(3.20 AS Decimal(18, 2)), 8009)
INSERT [dbo].[ShopProduct] ([ShopId], [Price], [ProductId]) VALUES (1, CAST(7.00 AS Decimal(18, 2)), 9014)
INSERT [dbo].[ShopProduct] ([ShopId], [Price], [ProductId]) VALUES (2, CAST(10.00 AS Decimal(18, 2)), 8007)
INSERT [dbo].[ShopProduct] ([ShopId], [Price], [ProductId]) VALUES (2, CAST(3.32 AS Decimal(18, 2)), 8009)
INSERT [dbo].[ShopProduct] ([ShopId], [Price], [ProductId]) VALUES (2, CAST(6.00 AS Decimal(18, 2)), 9014)
/****** Object:  Index [IX_ShopProduct_ProductId]    Script Date: 17/11/2017 13:54:21 ******/
CREATE NONCLUSTERED INDEX [IX_ShopProduct_ProductId] ON [dbo].[ShopProduct]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShopProduct]  WITH CHECK ADD  CONSTRAINT [FK_ShopProduct_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShopProduct] CHECK CONSTRAINT [FK_ShopProduct_Product_ProductId]
GO
ALTER TABLE [dbo].[ShopProduct]  WITH CHECK ADD  CONSTRAINT [FK_ShopProduct_Shop_ShopId] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([ShopId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShopProduct] CHECK CONSTRAINT [FK_ShopProduct_Shop_ShopId]
GO
USE [master]
GO
ALTER DATABASE [CoopShopInfosContext-55f4370d-157f-4bfb-8ee7-998ed26d3309] SET  READ_WRITE 
GO
