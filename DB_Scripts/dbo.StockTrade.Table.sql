USE [OMS]
GO
/****** Object:  Table [dbo].[StockTrade]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTrade](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Symbol] [nvarchar](50) NOT NULL,
	[OrderType] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
