USE [cw-capstone-db]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Balance] [int] NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[RegistrationToken] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[InfoUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Note] [nvarchar](max) NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConfigUser]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigUser](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
	[InfoUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ConfigUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deposit]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deposit](
	[Id] [uniqueidentifier] NOT NULL,
	[Amount] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[PaymentMethod] [nvarchar](max) NOT NULL,
	[TransactionIdPartner] [nvarchar](max) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Deposit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TimeStart] [datetime2](7) NOT NULL,
	[TimeEnd] [datetime2](7) NOT NULL,
	[Value] [int] NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Rating] [float] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
	[FeedbackFor] [nvarchar](max) NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
	[CreatorId] [uniqueidentifier] NOT NULL,
	[ReceiverId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InfoUser]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoUser](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[PhotoUrl] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_InfoUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[IsSend] [bit] NOT NULL,
	[PackageId] [uniqueidentifier] NULL,
	[TypeOfNotification] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[Id] [uniqueidentifier] NOT NULL,
	[StartAddress] [nvarchar](max) NOT NULL,
	[StartLongitude] [float] NOT NULL,
	[StartLatitude] [float] NOT NULL,
	[DestinationAddress] [nvarchar](max) NOT NULL,
	[DestinationLongitude] [float] NOT NULL,
	[DestinationLatitude] [float] NOT NULL,
	[Distance] [float] NOT NULL,
	[PickupName] [nvarchar](max) NOT NULL,
	[PickupPhone] [nvarchar](max) NOT NULL,
	[ReceiverName] [nvarchar](max) NOT NULL,
	[ReceiverPhone] [nvarchar](max) NOT NULL,
	[Height] [float] NOT NULL,
	[Width] [float] NOT NULL,
	[Length] [float] NOT NULL,
	[Weight] [float] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[PriceShip] [int] NOT NULL,
	[PhotoUrl] [nvarchar](max) NOT NULL,
	[Note] [nvarchar](max) NOT NULL,
	[PickupTimeStart] [datetime2](7) NOT NULL,
	[PickupTimeOver] [datetime2](7) NOT NULL,
	[DeliveryTimeStart] [datetime2](7) NOT NULL,
	[DeliveryTimeOver] [datetime2](7) NOT NULL,
	[ExpiredTime] [datetime2](7) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
	[SenderId] [uniqueidentifier] NOT NULL,
	[DeliverId] [uniqueidentifier] NULL,
	[DiscountId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[Id] [uniqueidentifier] NOT NULL,
	[TypeOfReport] [nvarchar](max) NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Note] [nvarchar](max) NOT NULL,
	[Result] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[Id] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FromName] [nvarchar](max) NOT NULL,
	[FromLatitude] [float] NOT NULL,
	[FromLongitude] [float] NOT NULL,
	[ToName] [nvarchar](max) NOT NULL,
	[DistanceForward] [float] NOT NULL,
	[ToLatitude] [float] NOT NULL,
	[ToLongitude] [float] NOT NULL,
	[InfoUserId] [uniqueidentifier] NOT NULL,
	[DistanceBackward] [float] NOT NULL,
	[DistanceBackwardVirtual] [float] NULL,
	[DistanceForwardVirtual] [float] NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoutePoint]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoutePoint](
	[Id] [uniqueidentifier] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Index] [int] NOT NULL,
	[DirectionType] [nvarchar](max) NOT NULL,
	[RouteId] [uniqueidentifier] NOT NULL,
	[IsVitual] [bit] NOT NULL,
 CONSTRAINT [PK_RoutePoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TransactionType] [nvarchar](max) NOT NULL,
	[CoinExchange] [int] NOT NULL,
	[BalanceWallet] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[DepositId] [uniqueidentifier] NULL,
	[PackageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionPackage]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionPackage](
	[Id] [uniqueidentifier] NOT NULL,
	[FromStatus] [nvarchar](max) NOT NULL,
	[ToStatus] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TransactionPackage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 4/8/2023 11:16:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[MaxVolume] [float] NOT NULL,
	[MaxSize] [float] NOT NULL,
	[InfoUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Id], [UserName], [Password], [Status], [Balance], [Role], [RegistrationToken], [CreatedAt], [InfoUserId]) VALUES (N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', N'admin', N'admin', N'ACTIVE', 0, N'ADMIN', N'', CAST(N'2023-04-08T04:14:23.1066667' AS DateTime2), N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[Account] ([Id], [UserName], [Password], [Status], [Balance], [Role], [RegistrationToken], [CreatedAt], [InfoUserId]) VALUES (N'3b731259-c8f2-4515-7b97-08db37e7bba6', N'admin_balance', N'admin_balance', N'ACTIVE', 0, N'ADMIN_BALANCE', N'', CAST(N'2023-04-08T04:14:23.1066667' AS DateTime2), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'72cafc86-6cf8-4730-5cd7-08db37e7bbb5', N'PROFIT_PERCENTAGE', N'20', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'36db53f4-2fe4-4867-5cd8-08db37e7bbb5', N'PROFIT_PERCENTAGE_REFUND', N'50', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'ba79c5da-4842-4762-5cd9-08db37e7bbb5', N'MINIMUM_DISTANCE', N'1000', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'b4326875-6984-4b4b-5cda-08db37e7bbb5', N'MAX_PICKUP_SAME_TIME', N'50', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'cc6d3312-8d75-4c33-5cdb-08db37e7bbb5', N'MAX_ROUTE_CREATE', N'3', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'6ea41bb4-62b6-4a05-5cdc-08db37e7bbb5', N'DEFAULT_BALANCE_NEW_ACCOUNT', N'100000', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'c02d7a33-4c26-4de8-5cdd-08db37e7bbb5', N'MAX_SUGGEST_COMBO', N'4', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
INSERT [dbo].[Config] ([Id], [Name], [Note], [ModifiedBy], [ModifiedAt]) VALUES (N'3a43d57f-0a4c-49d8-5cde-08db37e7bbb5', N'MAX_CANCEL_IN_DAY', N'2', N'cc86bd3a-51c7-4b14-7b96-08db37e7bba6', CAST(N'2023-04-08T04:14:23.1133333' AS DateTime2))
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Config] ADD  DEFAULT (getutcdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[ConfigUser] ADD  DEFAULT (getutcdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Deposit] ADD  DEFAULT (getutcdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Deposit] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (getutcdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Package] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Package] ADD  DEFAULT (getutcdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Report] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Report] ADD  DEFAULT (getutcdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Report] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [AccountId]
GO
ALTER TABLE [dbo].[Route] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [DistanceBackward]
GO
ALTER TABLE [dbo].[RoutePoint] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsVitual]
GO
ALTER TABLE [dbo].[Transaction] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TransactionPackage] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ConfigUser]  WITH CHECK ADD  CONSTRAINT [FK_ConfigUser_InfoUser_InfoUserId] FOREIGN KEY([InfoUserId])
REFERENCES [dbo].[InfoUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConfigUser] CHECK CONSTRAINT [FK_ConfigUser_InfoUser_InfoUserId]
GO
ALTER TABLE [dbo].[Deposit]  WITH CHECK ADD  CONSTRAINT [FK_Deposit_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deposit] CHECK CONSTRAINT [FK_Deposit_Account_AccountId]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Account_CreatorId] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Account_CreatorId]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Account_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Account_ReceiverId]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Package_PackageId]
GO
ALTER TABLE [dbo].[InfoUser]  WITH CHECK ADD  CONSTRAINT [FK_InfoUser_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InfoUser] CHECK CONSTRAINT [FK_InfoUser_Account_AccountId]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Account_AccountId]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Package_PackageId]
GO
ALTER TABLE [dbo].[Package]  WITH CHECK ADD  CONSTRAINT [FK_Package_Account_DeliverId] FOREIGN KEY([DeliverId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [FK_Package_Account_DeliverId]
GO
ALTER TABLE [dbo].[Package]  WITH CHECK ADD  CONSTRAINT [FK_Package_Account_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [FK_Package_Account_SenderId]
GO
ALTER TABLE [dbo].[Package]  WITH CHECK ADD  CONSTRAINT [FK_Package_Discount_DiscountId] FOREIGN KEY([DiscountId])
REFERENCES [dbo].[Discount] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [FK_Package_Discount_DiscountId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Package_PackageId]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Account_AccountId]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Package_PackageId]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_InfoUser_InfoUserId] FOREIGN KEY([InfoUserId])
REFERENCES [dbo].[InfoUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_InfoUser_InfoUserId]
GO
ALTER TABLE [dbo].[RoutePoint]  WITH CHECK ADD  CONSTRAINT [FK_RoutePoint_Route_RouteId] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Route] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoutePoint] CHECK CONSTRAINT [FK_RoutePoint_Route_RouteId]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account_AccountId]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Deposit_DepositId] FOREIGN KEY([DepositId])
REFERENCES [dbo].[Deposit] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Deposit_DepositId]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Package_PackageId]
GO
ALTER TABLE [dbo].[TransactionPackage]  WITH CHECK ADD  CONSTRAINT [FK_TransactionPackage_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransactionPackage] CHECK CONSTRAINT [FK_TransactionPackage_Package_PackageId]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_InfoUser_InfoUserId] FOREIGN KEY([InfoUserId])
REFERENCES [dbo].[InfoUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_InfoUser_InfoUserId]
GO
