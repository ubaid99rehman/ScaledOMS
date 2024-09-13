USE [OMS]
GO
/****** Object:  Table [dbo].[StockHistory]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[LastPrice] [decimal](18, 2) NOT NULL,
	[Change24H] [decimal](18, 2) NOT NULL,
	[High24H] [decimal](18, 2) NOT NULL,
	[Low24H] [decimal](18, 2) NOT NULL,
	[Change1H] [decimal](18, 2) NOT NULL,
	[High1H] [decimal](18, 2) NOT NULL,
	[Low1H] [decimal](18, 2) NOT NULL,
	[OpenPrice] [decimal](18, 2) NOT NULL,
	[ClosePrice] [decimal](18, 2) NOT NULL,
	[RecordedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StockHistory] ADD  DEFAULT (getdate()) FOR [RecordedAt]
GO
ALTER TABLE [dbo].[StockHistory]  WITH CHECK ADD FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
