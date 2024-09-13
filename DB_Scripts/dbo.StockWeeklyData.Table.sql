USE [OMS]
GO
/****** Object:  Table [dbo].[StockWeeklyData]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockWeeklyData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[OpenPrice] [decimal](18, 2) NOT NULL,
	[ClosePrice] [decimal](18, 2) NOT NULL,
	[HighPrice] [decimal](18, 2) NOT NULL,
	[LowPrice] [decimal](18, 2) NOT NULL,
	[Volume] [int] NOT NULL,
	[RecordedAt] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StockWeeklyData]  WITH CHECK ADD FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
