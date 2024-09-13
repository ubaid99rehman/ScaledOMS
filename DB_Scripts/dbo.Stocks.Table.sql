USE [OMS]
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocks](
	[StockID] [int] IDENTITY(1,1) NOT NULL,
	[StockName] [nvarchar](100) NOT NULL,
	[StockSymbol] [nvarchar](50) NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[LastPrice] [decimal](10, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Stocks] ADD  DEFAULT (getdate()) FOR [AddedDate]
GO
