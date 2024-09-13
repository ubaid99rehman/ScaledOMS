USE [OMS]
GO
/****** Object:  Table [dbo].[UserPermissionGroups]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermissionGroups](
	[UserID] [int] NOT NULL,
	[PermissionGroupID] [int] NOT NULL,
	[AssignedDate] [datetime] NOT NULL,
	[AssignedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[PermissionGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserPermissionGroups] ADD  DEFAULT (getdate()) FOR [AssignedDate]
GO
ALTER TABLE [dbo].[UserPermissionGroups]  WITH CHECK ADD FOREIGN KEY([AssignedBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserPermissionGroups]  WITH CHECK ADD FOREIGN KEY([PermissionGroupID])
REFERENCES [dbo].[PermissionGroups] ([PermissionGroupID])
GO
ALTER TABLE [dbo].[UserPermissionGroups]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
