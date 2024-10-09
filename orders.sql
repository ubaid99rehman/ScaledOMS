DECLARE @counter INT = 1
DECLARE @max INT = 500 -- Number of records you want to insert
DECLARE @symbols TABLE (Symbol NVARCHAR(50))
DECLARE @orderTypes TABLE (OrderType INT)
DECLARE @statuses TABLE (Status INT)
DECLARE @accounts TABLE (AccountID INT)
DECLARE @users TABLE (UserID INT)

-- Available Stock Symbols
INSERT INTO @symbols VALUES ('AAPL'), ('MSFT'), ('GOOGL'), ('AMZN'), ('TSLA'), ('META'), ('NVDA'), ('NFLX'), ('ADBE'), ('CRM'), ('INTC'), ('IBM'), ('ORCL'), ('QCOM'), ('NOW'), ('DIS'), ('BA'), ('GE'), ('AXP'), ('JNJ'), ('PG'), ('KO'), ('PEP'), ('WFC'), ('JPM'), ('GS'), ('MS'), ('BAC'), ('C'), ('MMM'), ('CAT'), ('XOM'), ('CVX'), ('PFE'), ('MRK'), ('ABBV'), ('AMGN'), ('GILD'), ('BIIB'), ('LLY'), ('NVS'), ('RHHBY'), ('SNY'), ('GSK'), ('AZN'), ('BMY'), ('BAX'), ('TMO'), ('DHR'), ('UNH'), ('CI'), ('HUM'), ('ANTM')

-- Order Types
INSERT INTO @orderTypes VALUES (1), (2)

-- Order Statuses
INSERT INTO @statuses VALUES (1), (2), (3)

-- Accounts and Users (assuming 1 to 4)
INSERT INTO @accounts VALUES (1), (2), (3), (4)
INSERT INTO @users VALUES (1), (2), (3), (6)

-- Insert records in a loop
WHILE @counter <= @max
BEGIN
    DECLARE @randomSymbol NVARCHAR(50)
    DECLARE @randomOrderType INT
    DECLARE @randomStatus INT
    DECLARE @randomAccountID INT
    DECLARE @randomUserID INT
    DECLARE @randomQuantity INT = ABS(CHECKSUM(NEWID()) % 500) + 1  -- Random quantity between 1 and 500
    DECLARE @randomPrice DECIMAL(18, 2) = ABS(CHECKSUM(NEWID()) % 500) + 50.00  -- Random price between 50 and 550

    -- Select random stock symbol, order type, status, and account
    SELECT TOP 1 @randomSymbol = Symbol FROM @symbols ORDER BY NEWID()
    SELECT TOP 1 @randomOrderType = OrderType FROM @orderTypes ORDER BY NEWID()
    SELECT TOP 1 @randomStatus = Status FROM @statuses ORDER BY NEWID()
    SELECT TOP 1 @randomAccountID = AccountID FROM @accounts ORDER BY NEWID()
    SELECT TOP 1 @randomUserID = UserID FROM @users ORDER BY NEWID()

    -- Insert order with random values
    INSERT INTO [dbo].[Orders] 
        ([OrderDate], [OrderType], [Quantity], [Price], [Total], [Status], [AccountID], [ExpirationDate], [LastUpdatedDate], [CreatedDate], [AddedBy], [Symbol], [OrderGuid])
    VALUES 
        (GETDATE(), @randomOrderType, @randomQuantity, @randomPrice, @randomQuantity * @randomPrice, @randomStatus, @randomAccountID, GETDATE(), GETDATE(), GETDATE(), @randomUserID, @randomSymbol, NEWID())

    SET @counter = @counter + 1
END
