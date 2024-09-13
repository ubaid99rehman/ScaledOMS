USE [OMS]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[LastPasswordChangeDate] [datetime] NULL,
	[PasswordRetryCount] [int] NOT NULL,
	[DefaultPassword] [bit] NOT NULL,
	[UserStatus] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((3)) FOR [PasswordRetryCount]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [DefaultPassword]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [UserStatus]
GO
