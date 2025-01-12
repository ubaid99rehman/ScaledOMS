USE [OMS]
GO
SET IDENTITY_INSERT [dbo].[Screens] ON 
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'DashboardView', N'Main dashboard for the user', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'StockMarketView', N'Displays the stock market data', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, N'InformationPanelView', N'Shows general information and system status', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, N'AddOrderView', N'View for adding a new order', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, N'EditOrderView', N'View for editing existing orders', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (6, N'ManageOrderView', N'View for managing user orders', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (7, N'OpenOrdersView', N'Displays all open orders', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (8, N'OrderBookView', N'Displays the order book', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (9, N'OrderHistoryView', N'Shows the history of all user orders', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10, N'AccountPortfolioView', N'Displays account portfolios', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (11, N'AppearanceView', N'Allows user to change the application appearance', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (12, N'ProfileView', N'User profile management view', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (13, N'StockDetailView', N'Detailed view of a specific stock', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (14, N'StockChartView', N'Displays a stock chart for a selected stock', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (15, N'TradeBookView', N'Displays a log of trades', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Screens] ([ScreenID], [ScreenName], [ScreenDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (16, N'TradeHistoryView', N'Displays the history of user trades', CAST(N'2024-10-03T10:52:43.483' AS DateTime), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Screens] OFF
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 1, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 1, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 1, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, 1, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, 1, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (6, 2, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (7, 2, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (8, 2, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (9, 2, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10, 2, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (11, 3, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (12, 3, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (13, 3, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (14, 3, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (15, 3, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (16, 4, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (17, 4, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (18, 4, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (19, 4, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (20, 4, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (21, 5, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (22, 5, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (23, 5, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (24, 5, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (25, 5, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (26, 6, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (27, 6, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (28, 6, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (29, 6, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (30, 6, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (31, 7, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (32, 7, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (33, 7, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (34, 7, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (35, 7, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (36, 8, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (37, 8, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (38, 8, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (39, 8, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (40, 8, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (41, 9, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (42, 9, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (43, 9, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (44, 9, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (45, 9, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (46, 10, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (47, 10, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (48, 10, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (49, 10, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (50, 10, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (51, 11, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (52, 11, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (53, 11, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (54, 11, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (55, 11, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (56, 12, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (57, 12, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (58, 12, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (59, 12, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (60, 12, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (61, 13, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (62, 13, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (63, 13, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (64, 13, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (65, 13, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (66, 14, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (67, 14, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (68, 14, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (69, 14, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (70, 14, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (71, 15, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (72, 15, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (73, 15, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (74, 15, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (75, 15, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (76, 16, 1, 1, 1, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (77, 16, 0, 1, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (78, 16, 1, 0, 0, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (79, 16, 0, 0, 1, 0, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Permissions] ([PermissionID], [ScreenID], [CanCreate], [CanRead], [CanUpdate], [CanDelete], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (80, 16, 0, 0, 0, 1, CAST(N'2024-10-03T11:05:29.943' AS DateTime), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [CreatedDate], [UpdatedDate], [CreatedBy], [UpdatedBy], [LastPasswordChangeDate], [PasswordRetryCount], [DefaultPassword], [UserStatus]) VALUES (1, N'urehman', N'urehman@trafux', N'Trade@123', CAST(N'2024-08-19T16:25:24.047' AS DateTime), CAST(N'2024-08-19T16:25:24.047' AS DateTime), 0, 0, CAST(N'2024-08-19T16:25:24.047' AS DateTime), 55, 0, 1)
GO
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [CreatedDate], [UpdatedDate], [CreatedBy], [UpdatedBy], [LastPasswordChangeDate], [PasswordRetryCount], [DefaultPassword], [UserStatus]) VALUES (2, N'JohnDoe', N'johndoe@example.com', N'EncryptedPassword123', CAST(N'2024-10-03T11:27:49.217' AS DateTime), NULL, NULL, NULL, CAST(N'2024-10-03T11:27:49.217' AS DateTime), 3, 0, 1)
GO
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [CreatedDate], [UpdatedDate], [CreatedBy], [UpdatedBy], [LastPasswordChangeDate], [PasswordRetryCount], [DefaultPassword], [UserStatus]) VALUES (3, N'JaneSmith', N'janesmith@example.com', N'EncryptedPassword456', CAST(N'2024-10-03T11:27:49.217' AS DateTime), NULL, NULL, NULL, CAST(N'2024-10-03T11:27:49.217' AS DateTime), 3, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName], [RoleDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'ReadOnly', NULL, CAST(N'2024-10-03T11:12:39.680' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName], [RoleDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'ReadAndAdd', NULL, CAST(N'2024-10-03T11:12:39.680' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName], [RoleDescription], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, N'FullAccess', NULL, CAST(N'2024-10-03T11:12:39.680' AS DateTime), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 2)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 7)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 12)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 17)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 22)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 27)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 32)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 37)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 42)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 47)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 52)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 57)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 62)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 67)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 72)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (1, 77)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 2)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 3)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 7)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 8)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 12)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 13)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 17)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 18)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 22)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 23)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 27)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 28)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 32)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 33)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 37)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 38)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 42)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 43)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 47)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 48)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 52)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 53)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 57)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 58)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 62)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 63)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 67)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 68)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 72)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 73)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 77)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (2, 78)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 1)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 6)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 11)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 16)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 21)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 26)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 31)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 36)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 41)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 46)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 51)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 56)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 61)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 66)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 71)
GO
INSERT [dbo].[RolePermissions] ([RoleID], [PermissionID]) VALUES (3, 76)
GO
SET IDENTITY_INSERT [dbo].[Order_Types] ON 
GO
INSERT [dbo].[Order_Types] ([ID], [Name]) VALUES (1, N'Buy')
GO
INSERT [dbo].[Order_Types] ([ID], [Name]) VALUES (2, N'Sell')
GO
SET IDENTITY_INSERT [dbo].[Order_Types] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Statuses] ON 
GO
INSERT [dbo].[Order_Statuses] ([ID], [Name]) VALUES (1, N'New')
GO
INSERT [dbo].[Order_Statuses] ([ID], [Name]) VALUES (2, N'Fulfilled')
GO
INSERT [dbo].[Order_Statuses] ([ID], [Name]) VALUES (3, N'Cancelled')
GO
SET IDENTITY_INSERT [dbo].[Order_Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([AccountID], [AccountName], [AccountNumber], [CreatedDate]) VALUES (1, N'JD Account', N'103415', CAST(N'2024-09-02T17:14:41.517' AS DateTime))
GO
INSERT [dbo].[Accounts] ([AccountID], [AccountName], [AccountNumber], [CreatedDate]) VALUES (2, N'John Doe Account', N'103455', CAST(N'2024-09-02T17:14:41.517' AS DateTime))
GO
INSERT [dbo].[Accounts] ([AccountID], [AccountName], [AccountNumber], [CreatedDate]) VALUES (3, N'Trade Account', N'103435', CAST(N'2024-09-02T17:14:41.517' AS DateTime))
GO
INSERT [dbo].[Accounts] ([AccountID], [AccountName], [AccountNumber], [CreatedDate]) VALUES (4, N'Tether Account', N'103125', CAST(N'2024-09-02T17:14:41.517' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (2, CAST(N'2023-09-01T00:00:00.000' AS DateTime), 1, 100, CAST(150.00 AS Decimal(18, 2)), CAST(15000.00 AS Decimal(18, 2)), 3, 1, CAST(N'2023-12-31T00:00:00.000' AS DateTime), CAST(N'2023-09-05T00:00:00.000' AS DateTime), CAST(N'2023-09-01T00:00:00.000' AS DateTime), 1, N'AAPL', N'5b3e26d9-e0fe-4fd8-885b-57eb18083af1')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (3, CAST(N'2023-09-02T00:00:00.000' AS DateTime), 2, 200, CAST(250.50 AS Decimal(18, 2)), CAST(50100.00 AS Decimal(18, 2)), 2, 1, CAST(N'2023-12-30T00:00:00.000' AS DateTime), CAST(N'2023-09-06T00:00:00.000' AS DateTime), CAST(N'2023-09-02T00:00:00.000' AS DateTime), 1, N'MSFT', N'e18fba91-4c98-42b0-889b-8593cec74eb7')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (4, CAST(N'2023-09-03T00:00:00.000' AS DateTime), 1, 150, CAST(2800.75 AS Decimal(18, 2)), CAST(42011.25 AS Decimal(18, 2)), 3, 3, CAST(N'2023-12-29T00:00:00.000' AS DateTime), CAST(N'2023-09-07T00:00:00.000' AS DateTime), CAST(N'2023-09-03T00:00:00.000' AS DateTime), 1, N'GOOGL', N'a054f9af-ecf1-45ba-bb6e-76a367561ec7')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (5, CAST(N'2023-09-04T00:00:00.000' AS DateTime), 2, 120, CAST(3400.50 AS Decimal(18, 2)), CAST(40806.00 AS Decimal(18, 2)), 3, 4, CAST(N'2023-12-28T00:00:00.000' AS DateTime), CAST(N'2023-09-08T00:00:00.000' AS DateTime), CAST(N'2023-09-04T00:00:00.000' AS DateTime), 1, N'AMZN', N'8a00e2cd-0150-469d-b467-f627f93aa47e')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (6, CAST(N'2023-09-05T00:00:00.000' AS DateTime), 1, 80, CAST(500.80 AS Decimal(18, 2)), CAST(40064.00 AS Decimal(18, 2)), 2, 1, CAST(N'2023-12-27T00:00:00.000' AS DateTime), CAST(N'2023-09-09T00:00:00.000' AS DateTime), CAST(N'2023-09-05T00:00:00.000' AS DateTime), 1, N'TSLA', N'3a912fed-f6a7-4ede-bd2a-374d60b73b22')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (7, CAST(N'2023-09-06T00:00:00.000' AS DateTime), 2, 130, CAST(1200.45 AS Decimal(18, 2)), CAST(15605.85 AS Decimal(18, 2)), 3, 2, CAST(N'2023-12-26T00:00:00.000' AS DateTime), CAST(N'2023-09-10T00:00:00.000' AS DateTime), CAST(N'2023-09-06T00:00:00.000' AS DateTime), 1, N'META', N'79b517f6-93f6-4ca8-873f-ec7338ba970b')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (8, CAST(N'2023-09-07T00:00:00.000' AS DateTime), 1, 300, CAST(400.95 AS Decimal(18, 2)), CAST(120285.00 AS Decimal(18, 2)), 3, 1, CAST(N'2023-12-25T00:00:00.000' AS DateTime), CAST(N'2023-09-11T00:00:00.000' AS DateTime), CAST(N'2023-09-07T00:00:00.000' AS DateTime), 1, N'NVDA', N'7ab4b995-cd76-4a60-9009-d5685d2249b4')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (9, CAST(N'2023-09-08T00:00:00.000' AS DateTime), 2, 50, CAST(600.60 AS Decimal(18, 2)), CAST(30030.00 AS Decimal(18, 2)), 2, 3, CAST(N'2023-12-24T00:00:00.000' AS DateTime), CAST(N'2023-09-12T00:00:00.000' AS DateTime), CAST(N'2023-09-08T00:00:00.000' AS DateTime), 1, N'NFLX', N'c4dba051-fe73-4541-b195-41736249862a')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (10, CAST(N'2023-09-09T00:00:00.000' AS DateTime), 1, 180, CAST(700.70 AS Decimal(18, 2)), CAST(126126.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-12-23T00:00:00.000' AS DateTime), CAST(N'2023-09-13T00:00:00.000' AS DateTime), CAST(N'2023-09-09T00:00:00.000' AS DateTime), 1, N'ADBE', N'fe467c7e-2bc3-4b8f-8c43-cdc488239660')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (11, CAST(N'2023-09-10T00:00:00.000' AS DateTime), 2, 110, CAST(800.80 AS Decimal(18, 2)), CAST(88088.00 AS Decimal(18, 2)), 3, 1, CAST(N'2023-12-22T00:00:00.000' AS DateTime), CAST(N'2023-09-14T00:00:00.000' AS DateTime), CAST(N'2023-09-10T00:00:00.000' AS DateTime), 1, N'CRM', N'5c60ae2e-6405-4284-9838-939eaee8e6a1')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (12, CAST(N'2023-09-11T00:00:00.000' AS DateTime), 1, 90, CAST(900.90 AS Decimal(18, 2)), CAST(81081.00 AS Decimal(18, 2)), 2, 2, CAST(N'2023-12-21T00:00:00.000' AS DateTime), CAST(N'2023-09-15T00:00:00.000' AS DateTime), CAST(N'2023-09-11T00:00:00.000' AS DateTime), 1, N'INTC', N'2158014f-5632-4cce-bbfb-e0e9241434cc')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (13, CAST(N'2023-09-12T00:00:00.000' AS DateTime), 2, 70, CAST(1000.00 AS Decimal(18, 2)), CAST(70000.00 AS Decimal(18, 2)), 3, 2, CAST(N'2023-12-20T00:00:00.000' AS DateTime), CAST(N'2023-09-16T00:00:00.000' AS DateTime), CAST(N'2023-09-12T00:00:00.000' AS DateTime), 1, N'IBM', N'2aa96758-333d-4589-bb21-9bf61b81ea12')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (14, CAST(N'2023-09-13T00:00:00.000' AS DateTime), 1, 160, CAST(1100.10 AS Decimal(18, 2)), CAST(176016.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-12-19T00:00:00.000' AS DateTime), CAST(N'2023-09-17T00:00:00.000' AS DateTime), CAST(N'2023-09-13T00:00:00.000' AS DateTime), 1, N'ORCL', N'cf61338d-94cc-47d7-8563-78d009214e21')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (15, CAST(N'2023-09-14T00:00:00.000' AS DateTime), 2, 140, CAST(1200.20 AS Decimal(18, 2)), CAST(168028.00 AS Decimal(18, 2)), 2, 4, CAST(N'2023-12-18T00:00:00.000' AS DateTime), CAST(N'2023-09-18T00:00:00.000' AS DateTime), CAST(N'2023-09-14T00:00:00.000' AS DateTime), 1, N'QCOM', N'8a73914a-e705-4fbb-9f42-ce4122c9c076')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (16, CAST(N'2023-09-15T00:00:00.000' AS DateTime), 1, 220, CAST(1300.30 AS Decimal(18, 2)), CAST(286066.00 AS Decimal(18, 2)), 3, 1, CAST(N'2023-12-17T00:00:00.000' AS DateTime), CAST(N'2023-09-19T00:00:00.000' AS DateTime), CAST(N'2023-09-15T00:00:00.000' AS DateTime), 1, N'NOW', N'52b7a0a0-9f4b-492f-9d89-f772b84824e4')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (17, CAST(N'2023-09-16T00:00:00.000' AS DateTime), 2, 258, CAST(361303.20 AS Decimal(18, 2)), CAST(361303.20 AS Decimal(18, 2)), 3, 4, CAST(N'2023-12-16T00:00:00.000' AS DateTime), CAST(N'2023-09-20T00:00:00.000' AS DateTime), CAST(N'2023-09-16T00:00:00.000' AS DateTime), 1, N'DIS', N'c2ef8979-6294-4371-ab95-55fd1797d90c')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (18, CAST(N'2023-09-17T00:00:00.000' AS DateTime), 1, 290, CAST(1500.50 AS Decimal(18, 2)), CAST(435145.00 AS Decimal(18, 2)), 2, 2, CAST(N'2023-12-15T00:00:00.000' AS DateTime), CAST(N'2023-09-21T00:00:00.000' AS DateTime), CAST(N'2023-09-17T00:00:00.000' AS DateTime), 1, N'BA', N'0e30a48e-09d0-4e95-a173-116cca8303f1')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (19, CAST(N'2023-09-18T00:00:00.000' AS DateTime), 2, 100, CAST(1600.60 AS Decimal(18, 2)), CAST(160060.00 AS Decimal(18, 2)), 3, 4, CAST(N'2023-12-14T00:00:00.000' AS DateTime), CAST(N'2023-09-22T00:00:00.000' AS DateTime), CAST(N'2023-09-18T00:00:00.000' AS DateTime), 1, N'GE', N'dbe18a6e-589c-4ff5-9902-8801ef6c059b')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (20, CAST(N'2023-09-19T00:00:00.000' AS DateTime), 1, 80, CAST(1700.70 AS Decimal(18, 2)), CAST(136056.00 AS Decimal(18, 2)), 3, 4, CAST(N'2023-12-13T00:00:00.000' AS DateTime), CAST(N'2023-09-23T00:00:00.000' AS DateTime), CAST(N'2023-09-19T00:00:00.000' AS DateTime), 1, N'AXP', N'd0700e76-46cf-4a2d-a47f-00ac85f2ff34')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (21, CAST(N'2023-09-20T00:00:00.000' AS DateTime), 2, 180, CAST(1800.80 AS Decimal(18, 2)), CAST(324144.00 AS Decimal(18, 2)), 2, 2, CAST(N'2023-12-12T00:00:00.000' AS DateTime), CAST(N'2023-09-24T00:00:00.000' AS DateTime), CAST(N'2023-09-20T00:00:00.000' AS DateTime), 1, N'JNJ', N'75bf9eb9-cc06-4468-813b-49be1cdf09aa')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (22, CAST(N'2023-09-21T00:00:00.000' AS DateTime), 1, 220, CAST(1900.90 AS Decimal(18, 2)), CAST(418198.00 AS Decimal(18, 2)), 3, 2, CAST(N'2023-12-11T00:00:00.000' AS DateTime), CAST(N'2023-09-25T00:00:00.000' AS DateTime), CAST(N'2023-09-21T00:00:00.000' AS DateTime), 1, N'PG', N'4746e462-7cfc-416d-9647-37f5918d038d')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (23, CAST(N'2023-09-22T00:00:00.000' AS DateTime), 2, 315, CAST(480000.00 AS Decimal(18, 2)), CAST(151200000.00 AS Decimal(18, 2)), 1, 3, CAST(N'2023-12-10T00:00:00.000' AS DateTime), NULL, CAST(N'2023-09-22T00:00:00.000' AS DateTime), 1, N'KO', N'63a217a3-41e0-4182-a0ad-7124c7515c1b')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (24, CAST(N'2023-09-23T00:00:00.000' AS DateTime), 1, 190, CAST(2100.10 AS Decimal(18, 2)), CAST(399019.00 AS Decimal(18, 2)), 2, 3, CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(N'2023-09-27T00:00:00.000' AS DateTime), CAST(N'2023-09-23T00:00:00.000' AS DateTime), 1, N'PEP', N'c8c007a3-2544-4cf7-8fd5-927af142bc63')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (25, CAST(N'2023-09-24T00:00:00.000' AS DateTime), 2, 200, CAST(2200.20 AS Decimal(18, 2)), CAST(440040.00 AS Decimal(18, 2)), 3, 4, CAST(N'2023-12-08T00:00:00.000' AS DateTime), CAST(N'2023-09-28T00:00:00.000' AS DateTime), CAST(N'2023-09-24T00:00:00.000' AS DateTime), 1, N'WFC', N'3b594e93-e7f1-44f7-89f5-c1f7983d69c0')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (26, CAST(N'2023-09-25T00:00:00.000' AS DateTime), 1, 102, CAST(2300.30 AS Decimal(18, 2)), CAST(234630.60 AS Decimal(18, 2)), 1, 4, CAST(N'2023-12-07T00:00:00.000' AS DateTime), CAST(N'2023-09-29T00:00:00.000' AS DateTime), CAST(N'2023-09-25T00:00:00.000' AS DateTime), 1, N'JPM', N'613dea58-150b-49c7-a7a8-08c92168887d')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (27, CAST(N'2023-09-26T00:00:00.000' AS DateTime), 2, 170, CAST(2400.40 AS Decimal(18, 2)), CAST(408068.00 AS Decimal(18, 2)), 2, 2, CAST(N'2023-12-06T00:00:00.000' AS DateTime), CAST(N'2023-09-30T00:00:00.000' AS DateTime), CAST(N'2023-09-26T00:00:00.000' AS DateTime), 1, N'GS', N'6ee7eb98-1d6d-415b-9b31-0ade186c5b74')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (28, CAST(N'2023-09-27T00:00:00.000' AS DateTime), 1, 140, CAST(2500.50 AS Decimal(18, 2)), CAST(350070.00 AS Decimal(18, 2)), 3, 2, CAST(N'2023-12-05T00:00:00.000' AS DateTime), CAST(N'2023-10-01T00:00:00.000' AS DateTime), CAST(N'2023-09-27T00:00:00.000' AS DateTime), 1, N'MS', N'463a67c8-e3af-4ef2-a5c4-73c257b72981')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (29, CAST(N'2023-09-28T00:00:00.000' AS DateTime), 2, 129, CAST(2600.60 AS Decimal(18, 2)), CAST(335477.40 AS Decimal(18, 2)), 1, 2, CAST(N'2023-12-04T00:00:00.000' AS DateTime), CAST(N'2023-10-02T00:00:00.000' AS DateTime), CAST(N'2023-09-28T00:00:00.000' AS DateTime), 1, N'BAC', N'ffe27847-3c76-4826-b38c-7b73269ac053')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (30, CAST(N'2023-09-29T00:00:00.000' AS DateTime), 1, 130, CAST(2700.70 AS Decimal(18, 2)), CAST(351091.00 AS Decimal(18, 2)), 2, 2, CAST(N'2023-12-03T00:00:00.000' AS DateTime), CAST(N'2023-10-03T00:00:00.000' AS DateTime), CAST(N'2023-09-29T00:00:00.000' AS DateTime), 1, N'C', N'b0e7d957-2945-413a-8d88-c60d445a888e')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (31, CAST(N'2023-09-30T00:00:00.000' AS DateTime), 2, 50, CAST(2800.80 AS Decimal(18, 2)), CAST(140040.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-12-02T00:00:00.000' AS DateTime), CAST(N'2023-10-04T00:00:00.000' AS DateTime), CAST(N'2023-09-30T00:00:00.000' AS DateTime), 1, N'MMM', N'e1e30493-674c-43dd-8693-192beac09bc4')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (32, CAST(N'2023-10-01T00:00:00.000' AS DateTime), 1, 150, CAST(2900.90 AS Decimal(18, 2)), CAST(435135.00 AS Decimal(18, 2)), 1, 3, CAST(N'2023-12-01T00:00:00.000' AS DateTime), CAST(N'2023-10-05T00:00:00.000' AS DateTime), CAST(N'2023-10-01T00:00:00.000' AS DateTime), 1, N'CAT', N'e33b728c-206c-4c7d-9bf9-2fbcd8ff8aab')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (33, CAST(N'2023-10-02T00:00:00.000' AS DateTime), 2, 70, CAST(3000.00 AS Decimal(18, 2)), CAST(210000.00 AS Decimal(18, 2)), 2, 2, CAST(N'2023-11-30T00:00:00.000' AS DateTime), CAST(N'2023-10-06T00:00:00.000' AS DateTime), CAST(N'2023-10-02T00:00:00.000' AS DateTime), 1, N'XOM', N'848a4d5b-884e-432b-b43b-e52c3c003207')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (34, CAST(N'2023-10-03T00:00:00.000' AS DateTime), 1, 160, CAST(3100.10 AS Decimal(18, 2)), CAST(496016.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-11-29T00:00:00.000' AS DateTime), CAST(N'2023-10-07T00:00:00.000' AS DateTime), CAST(N'2023-10-03T00:00:00.000' AS DateTime), 1, N'CVX', N'e6eb7b43-d9e2-48b3-8881-2df385f084ba')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (35, CAST(N'2023-10-04T00:00:00.000' AS DateTime), 2, 110, CAST(3200.20 AS Decimal(18, 2)), CAST(352022.00 AS Decimal(18, 2)), 1, 3, CAST(N'2023-11-28T00:00:00.000' AS DateTime), CAST(N'2023-10-08T00:00:00.000' AS DateTime), CAST(N'2023-10-04T00:00:00.000' AS DateTime), 1, N'PFE', N'196cafdf-4b04-4ff6-b742-34984ceccd21')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (36, CAST(N'2023-10-05T00:00:00.000' AS DateTime), 1, 240, CAST(3300.30 AS Decimal(18, 2)), CAST(792072.00 AS Decimal(18, 2)), 2, 1, CAST(N'2023-11-27T00:00:00.000' AS DateTime), CAST(N'2023-10-09T00:00:00.000' AS DateTime), CAST(N'2023-10-05T00:00:00.000' AS DateTime), 1, N'MRK', N'3adc1bac-5254-4ac5-9a57-c738590ff0f1')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (37, CAST(N'2023-10-06T00:00:00.000' AS DateTime), 2, 170, CAST(3400.40 AS Decimal(18, 2)), CAST(578068.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-11-26T00:00:00.000' AS DateTime), CAST(N'2023-10-10T00:00:00.000' AS DateTime), CAST(N'2023-10-06T00:00:00.000' AS DateTime), 1, N'ABBV', N'346e4349-0ef5-4263-beb7-bd3ee40be325')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (38, CAST(N'2023-10-07T00:00:00.000' AS DateTime), 1, 100, CAST(3500.50 AS Decimal(18, 2)), CAST(350050.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-11-25T00:00:00.000' AS DateTime), CAST(N'2023-10-11T00:00:00.000' AS DateTime), CAST(N'2023-10-07T00:00:00.000' AS DateTime), 1, N'AMGN', N'adfebf24-579b-480a-aea8-d03a272d1145')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (39, CAST(N'2023-10-08T00:00:00.000' AS DateTime), 2, 140, CAST(3600.60 AS Decimal(18, 2)), CAST(504084.00 AS Decimal(18, 2)), 2, 3, CAST(N'2023-11-24T00:00:00.000' AS DateTime), CAST(N'2023-10-12T00:00:00.000' AS DateTime), CAST(N'2023-10-08T00:00:00.000' AS DateTime), 1, N'GILD', N'4345ace9-10ee-4eb1-ab20-621b513d737f')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (40, CAST(N'2023-10-09T00:00:00.000' AS DateTime), 1, 90, CAST(3700.70 AS Decimal(18, 2)), CAST(333063.00 AS Decimal(18, 2)), 3, 3, CAST(N'2023-11-23T00:00:00.000' AS DateTime), CAST(N'2023-10-13T00:00:00.000' AS DateTime), CAST(N'2023-10-09T00:00:00.000' AS DateTime), 1, N'BIIB', N'f0d7ffac-5b3e-4e76-8d33-fcd7aff7d4c5')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (41, CAST(N'2023-10-10T00:00:00.000' AS DateTime), 2, 80, CAST(3800.80 AS Decimal(18, 2)), CAST(304064.00 AS Decimal(18, 2)), 3, 1, CAST(N'2023-11-22T00:00:00.000' AS DateTime), CAST(N'2023-10-14T00:00:00.000' AS DateTime), CAST(N'2023-10-10T00:00:00.000' AS DateTime), 1, N'LLY', N'4c4910a4-769d-4f28-9256-7634c5f78f58')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (42, CAST(N'2023-10-11T00:00:00.000' AS DateTime), 1, 110, CAST(3900.90 AS Decimal(18, 2)), CAST(429099.00 AS Decimal(18, 2)), 2, 1, CAST(N'2023-11-21T00:00:00.000' AS DateTime), CAST(N'2023-10-15T00:00:00.000' AS DateTime), CAST(N'2023-10-11T00:00:00.000' AS DateTime), 1, N'NVS', N'583bac67-f673-4c8c-9d4b-015f526cf59e')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (43, CAST(N'2023-10-12T00:00:00.000' AS DateTime), 2, 130, CAST(4000.00 AS Decimal(18, 2)), CAST(520000.00 AS Decimal(18, 2)), 3, 2, CAST(N'2023-11-20T00:00:00.000' AS DateTime), CAST(N'2023-10-16T00:00:00.000' AS DateTime), CAST(N'2023-10-12T00:00:00.000' AS DateTime), 1, N'RHHBY', N'ad948291-35f9-468a-b229-97f15d1bbd59')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (44, CAST(N'2023-10-13T00:00:00.000' AS DateTime), 1, 200, CAST(4100.10 AS Decimal(18, 2)), CAST(820020.00 AS Decimal(18, 2)), 1, 3, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(N'2023-10-17T00:00:00.000' AS DateTime), CAST(N'2023-10-13T00:00:00.000' AS DateTime), 1, N'SNY', N'6bd7271e-1f39-4f13-a0ed-ecea152ccab0')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (45, CAST(N'2023-10-14T00:00:00.000' AS DateTime), 2, 190, CAST(4200.20 AS Decimal(18, 2)), CAST(798038.00 AS Decimal(18, 2)), 2, 1, CAST(N'2023-11-18T00:00:00.000' AS DateTime), CAST(N'2023-10-18T00:00:00.000' AS DateTime), CAST(N'2023-10-14T00:00:00.000' AS DateTime), 1, N'GSK', N'595e0223-73f1-4464-a2d9-c52b972bd50f')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (46, CAST(N'2023-10-15T00:00:00.000' AS DateTime), 1, 70, CAST(4300.30 AS Decimal(18, 2)), CAST(301021.00 AS Decimal(18, 2)), 3, 4, CAST(N'2023-11-17T00:00:00.000' AS DateTime), CAST(N'2023-10-19T00:00:00.000' AS DateTime), CAST(N'2023-10-15T00:00:00.000' AS DateTime), 1, N'AZN', N'289a32e6-45e8-4517-a077-71e48713aa5a')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (47, CAST(N'2023-10-16T00:00:00.000' AS DateTime), 2, 160, CAST(4400.40 AS Decimal(18, 2)), CAST(704064.00 AS Decimal(18, 2)), 1, 4, CAST(N'2023-11-16T00:00:00.000' AS DateTime), CAST(N'2023-10-20T00:00:00.000' AS DateTime), CAST(N'2023-10-16T00:00:00.000' AS DateTime), 1, N'BMY', N'13881bbe-51d7-4b7e-b2a7-cde37669959a')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (48, CAST(N'2023-10-17T00:00:00.000' AS DateTime), 1, 170, CAST(4500.50 AS Decimal(18, 2)), CAST(765085.00 AS Decimal(18, 2)), 2, 4, CAST(N'2023-11-15T00:00:00.000' AS DateTime), CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-10-17T00:00:00.000' AS DateTime), 1, N'BAX', N'61afb56d-cb38-456a-bd25-17e8bad40e95')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (49, CAST(N'2023-10-18T00:00:00.000' AS DateTime), 2, 150, CAST(4600.60 AS Decimal(18, 2)), CAST(690090.00 AS Decimal(18, 2)), 3, 4, CAST(N'2023-11-14T00:00:00.000' AS DateTime), CAST(N'2023-10-22T00:00:00.000' AS DateTime), CAST(N'2023-10-18T00:00:00.000' AS DateTime), 1, N'TMO', N'0c8eed4f-1141-405f-8bfa-c0b13fd6bf96')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (50, CAST(N'2023-10-19T00:00:00.000' AS DateTime), 1, 200, CAST(4700.70 AS Decimal(18, 2)), CAST(940140.00 AS Decimal(18, 2)), 1, 4, CAST(N'2023-11-13T00:00:00.000' AS DateTime), CAST(N'2023-10-23T00:00:00.000' AS DateTime), CAST(N'2023-10-19T00:00:00.000' AS DateTime), 1, N'DHR', N'761cfa21-a1ff-4ea1-bd92-11609eb7116d')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (51, CAST(N'2023-10-20T00:00:00.000' AS DateTime), 2, 180, CAST(4800.80 AS Decimal(18, 2)), CAST(864144.00 AS Decimal(18, 2)), 2, 1, CAST(N'2023-11-12T00:00:00.000' AS DateTime), CAST(N'2023-10-24T00:00:00.000' AS DateTime), CAST(N'2023-10-20T00:00:00.000' AS DateTime), 1, N'UNH', N'f5891ad9-353c-4016-b002-7781a732f757')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (52, CAST(N'2023-10-21T00:00:00.000' AS DateTime), 1, 130, CAST(4900.90 AS Decimal(18, 2)), CAST(637117.00 AS Decimal(18, 2)), 3, 1, CAST(N'2023-11-11T00:00:00.000' AS DateTime), CAST(N'2023-10-25T00:00:00.000' AS DateTime), CAST(N'2023-10-21T00:00:00.000' AS DateTime), 1, N'CI', N'ab029cb9-4739-4c4f-9c95-ecf8563d544b')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (53, CAST(N'2023-10-22T00:00:00.000' AS DateTime), 2, 110, CAST(5000.00 AS Decimal(18, 2)), CAST(550000.00 AS Decimal(18, 2)), 1, 2, CAST(N'2023-11-10T00:00:00.000' AS DateTime), CAST(N'2023-10-26T00:00:00.000' AS DateTime), CAST(N'2023-10-22T00:00:00.000' AS DateTime), 1, N'HUM', N'2526ad8b-e3ec-4e2a-92a9-392caacaa29e')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (54, CAST(N'2023-10-23T00:00:00.000' AS DateTime), 1, 100, CAST(5100.10 AS Decimal(18, 2)), CAST(510010.00 AS Decimal(18, 2)), 2, 3, CAST(N'2023-11-09T00:00:00.000' AS DateTime), CAST(N'2023-10-27T00:00:00.000' AS DateTime), CAST(N'2023-10-23T00:00:00.000' AS DateTime), 1, N'ANTM', N'531e759f-76e5-434a-80eb-20d1921c9ba7')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (55, CAST(N'2024-09-18T17:12:24.817' AS DateTime), 1, 6, CAST(7383.20 AS Decimal(18, 2)), CAST(44299.18 AS Decimal(18, 2)), 3, 4, CAST(N'2024-09-18T17:12:41.540' AS DateTime), CAST(N'2024-09-20T17:12:41.540' AS DateTime), CAST(N'2024-09-18T17:12:41.540' AS DateTime), 1, N'AAPL', N'b37a4ded-1f03-4ce7-8ae4-e684043c8e2f')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (56, CAST(N'2024-09-18T17:13:01.350' AS DateTime), 1, 11, CAST(7383.20 AS Decimal(18, 2)), CAST(81215.17 AS Decimal(18, 2)), 3, 3, CAST(N'2024-09-18T17:13:03.157' AS DateTime), CAST(N'2024-09-20T17:13:03.157' AS DateTime), CAST(N'2024-09-18T17:13:03.157' AS DateTime), 1, N'AAPL', N'd2988b2a-8632-4c9a-9d66-d81c9a4bc337')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (57, CAST(N'2024-09-18T17:23:10.447' AS DateTime), 1, 28, CAST(4630.21 AS Decimal(18, 2)), CAST(129645.88 AS Decimal(18, 2)), 1, 2, CAST(N'2024-09-18T17:23:10.447' AS DateTime), CAST(N'2024-09-20T17:23:10.447' AS DateTime), CAST(N'2024-09-18T17:23:10.447' AS DateTime), 1, N'GOOGL', N'2634a601-77de-4b1b-ac9a-8c18a5b4bc86')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (58, CAST(N'2024-09-30T13:40:19.633' AS DateTime), 1, 4, CAST(7383.20 AS Decimal(18, 2)), CAST(29532.79 AS Decimal(18, 2)), 1, 3, CAST(N'2024-09-30T13:40:19.633' AS DateTime), CAST(N'2024-10-02T13:40:19.633' AS DateTime), CAST(N'2024-09-30T13:40:19.633' AS DateTime), 1, N'AAPL', N'dba3986d-a9e3-4108-85f3-65695adaeb73')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (59, CAST(N'2024-09-30T13:40:49.543' AS DateTime), 1, 10, CAST(941.49 AS Decimal(18, 2)), CAST(9414.89 AS Decimal(18, 2)), 1, 4, CAST(N'2024-09-30T13:40:49.543' AS DateTime), CAST(N'2024-10-02T13:40:49.543' AS DateTime), CAST(N'2024-09-30T13:40:49.543' AS DateTime), 1, N'META', N'8190e9b2-1620-4547-a83c-b0b82b2f57ca')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (60, CAST(N'2024-09-30T13:44:46.190' AS DateTime), 1, 9, CAST(7383.20 AS Decimal(18, 2)), CAST(66448.77 AS Decimal(18, 2)), 1, 3, CAST(N'2024-09-30T13:44:49.147' AS DateTime), CAST(N'2024-10-02T13:44:49.147' AS DateTime), CAST(N'2024-09-30T13:44:49.147' AS DateTime), 1, N'AAPL', N'26fe8624-1f44-4969-b308-ac15a7b7cbfa')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (61, CAST(N'2024-09-30T13:48:29.727' AS DateTime), 1, 1, CAST(8549.98 AS Decimal(18, 2)), CAST(8549.98 AS Decimal(18, 2)), 1, 4, CAST(N'2024-09-30T13:48:33.010' AS DateTime), CAST(N'2024-10-02T13:48:33.010' AS DateTime), CAST(N'2024-09-30T13:48:33.010' AS DateTime), 1, N'ANTM', N'6bd13451-6667-476e-b1f0-ee71f547afc3')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (62, CAST(N'2024-09-30T13:49:16.327' AS DateTime), 1, 11, CAST(2568.47 AS Decimal(18, 2)), CAST(28253.15 AS Decimal(18, 2)), 3, 4, CAST(N'2024-09-30T13:49:16.327' AS DateTime), CAST(N'2024-10-02T13:49:16.327' AS DateTime), CAST(N'2024-09-30T13:49:16.327' AS DateTime), 1, N'LLY', N'20df8f32-09a7-458b-955c-a95e3dca4c95')
GO
INSERT [dbo].[Orders] ([OrderID], [OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid]) VALUES (63, CAST(N'2024-10-01T12:54:38.883' AS DateTime), 1, 14, CAST(7383.20 AS Decimal(18, 2)), CAST(103364.80 AS Decimal(18, 2)), 3, 4, CAST(N'2024-10-01T12:54:38.887' AS DateTime), CAST(N'2024-10-03T12:54:38.887' AS DateTime), CAST(N'2024-10-01T12:54:38.887' AS DateTime), 1, N'AAPL', N'7af4125f-08b1-4365-9066-087ab3899e08')
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[UserRoles] ([UserID], [RoleID], [AssignedDate]) VALUES (1, 3, CAST(N'2024-10-03T11:21:15.267' AS DateTime))
GO
INSERT [dbo].[UserRoles] ([UserID], [RoleID], [AssignedDate]) VALUES (2, 2, CAST(N'2024-10-03T11:28:07.367' AS DateTime))
GO
INSERT [dbo].[UserRoles] ([UserID], [RoleID], [AssignedDate]) VALUES (3, 1, CAST(N'2024-10-03T11:28:07.367' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Stocks] ON 
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (1, N'Apple Inc.', N'AAPL', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7383.1973 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (2, N'Microsoft Corporation', N'MSFT', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7322.9988 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (3, N'Alphabet Inc.', N'GOOGL', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(4630.2120 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (4, N'Amazon.com Inc.', N'AMZN', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2168.7702 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (5, N'Tesla Inc.', N'TSLA', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(3760.3569 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (6, N'Meta Platforms Inc.', N'META', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(941.4891 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (7, N'NVIDIA Corporation', N'NVDA', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(4131.2933 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (8, N'Netflix Inc.', N'NFLX', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8215.2115 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (9, N'Adobe Inc.', N'ADBE', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(5793.1269 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (10, N'Salesforce.com Inc.', N'CRM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(3030.8094 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (11, N'Intel Corporation', N'INTC', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(912.4502 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (12, N'IBM Corporation', N'IBM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(5423.4428 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (13, N'Oracle Corporation', N'ORCL', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7269.0161 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (14, N'Qualcomm Incorporated', N'QCOM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(852.3904 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (15, N'ServiceNow Inc.', N'NOW', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(9492.9586 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (16, N'Walt Disney Co.', N'DIS', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(3112.1718 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (17, N'Boeing Company', N'BA', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8504.4461 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (18, N'General Electric Company', N'GE', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(6585.6574 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (19, N'American Express Company', N'AXP', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(5888.9235 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (20, N'Johnson & Johnson', N'JNJ', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(3879.0256 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (21, N'Procter & Gamble Co.', N'PG', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7075.5241 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (22, N'Coca-Cola Co.', N'KO', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7616.8659 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (23, N'PepsiCo Inc.', N'PEP', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(6609.3806 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (24, N'Wells Fargo & Co.', N'WFC', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(5386.1787 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (25, N'JPMorgan Chase & Co.', N'JPM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(9202.1238 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (26, N'Goldman Sachs Group Inc.', N'GS', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2231.6748 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (27, N'Morgan Stanley', N'MS', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(1260.0492 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (28, N'Bank of America Corp.', N'BAC', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7413.9609 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (29, N'Citigroup Inc.', N'C', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7313.1015 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (30, N'3M Company', N'MMM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8688.6754 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (31, N'Caterpillar Inc.', N'CAT', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(1019.0536 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (32, N'Exxon Mobil Corporation', N'XOM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2005.1243 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (33, N'Chevron Corporation', N'CVX', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8444.9342 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (34, N'Pfizer Inc.', N'PFE', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(3957.0104 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (35, N'Merck & Co. Inc.', N'MRK', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(6109.5410 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (36, N'AbbVie Inc.', N'ABBV', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(5092.0907 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (37, N'Amgen Inc.', N'AMGN', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(6174.4631 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (38, N'Gilead Sciences Inc.', N'GILD', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8583.2562 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (39, N'Biogen Inc.', N'BIIB', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2297.2429 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (40, N'Eli Lilly and Company', N'LLY', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2568.4677 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (41, N'Novartis AG', N'NVS', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(6246.5599 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (42, N'Roche Holding AG', N'RHHBY', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(6285.9562 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (43, N'Sanofi', N'SNY', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(3440.2554 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (44, N'GlaxoSmithKline plc', N'GSK', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(4974.9827 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (45, N'AstraZeneca plc', N'AZN', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(7319.0848 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (46, N'Bristol-Myers Squibb Company', N'BMY', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(4238.0139 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (47, N'Baxter International Inc.', N'BAX', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(1703.3534 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (48, N'Thermo Fisher Scientific Inc.', N'TMO', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2370.7177 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (49, N'Danaher Corporation', N'DHR', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(2717.9820 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (50, N'UnitedHealth Group Incorporated', N'UNH', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(4293.4699 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (51, N'Cigna Corporation', N'CI', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8953.5071 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (52, N'Humana Inc.', N'HUM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(1300.4213 AS Decimal(10, 4)))
GO
INSERT [dbo].[Stocks] ([StockID], [StockName], [StockSymbol], [AddedDate], [LastPrice]) VALUES (53, N'Anthem Inc.', N'ANTM', CAST(N'2024-08-20T18:05:30.133' AS DateTime), CAST(8549.9813 AS Decimal(10, 4)))
GO
SET IDENTITY_INSERT [dbo].[Stocks] OFF
GO
