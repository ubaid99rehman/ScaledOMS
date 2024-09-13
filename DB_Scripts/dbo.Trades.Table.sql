USE [OMS]
GO
/****** Object:  Table [dbo].[Trades]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trades](
	[TradeID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[TradeType] [int] NOT NULL,
	[StockID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[AccountID] [int] NOT NULL,
	[TradeDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Trades] ADD  DEFAULT (getdate()) FOR [TradeDate]
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD FOREIGN KEY([AccountID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD FOREIGN KEY([StockID])
REFERENCES [dbo].[Stocks] ([StockID])
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD FOREIGN KEY([TradeType])
REFERENCES [dbo].[Order_Type] ([ID])
GO
