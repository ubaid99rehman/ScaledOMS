USE [OMS]
GO
/****** Object:  StoredProcedure [dbo].[SP_AuthenticateUser]    Script Date: 9/13/2024 5:35:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AuthenticateUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @UserId INT OUTPUT,
	@IsAuthenticated BIT OUTPUT,
    @IsDisabled BIT OUTPUT

AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ID INT;
    DECLARE @StoredPassword NVARCHAR(50);
    DECLARE @PasswordRetryCount INT;
    DECLARE @UserStatus BIT;

    -- Step 1: Check if the user exists
    SELECT 
        @ID = UserID,
        @StoredPassword = Password,
        @PasswordRetryCount = PasswordRetryCount,
        @UserStatus = UserStatus
    FROM Users
    WHERE Username = @Username;

    IF @ID IS NULL
    BEGIN
        -- User does not exist
		SET @UserId = 0;
        SET @IsAuthenticated = 0;
        SET @IsDisabled = 0;
        RETURN;
    END

	PRINT 'USER_ID';
	PRINT  @ID;

    -- Step 2: Validate the password and update retry count if necessary
    IF @StoredPassword = @Password AND @UserStatus = 1
    BEGIN
        -- Password is correct and user is active
		SET @UserId = @ID;
        SET @IsAuthenticated = 1;
        SET @IsDisabled = 0;

        -- Reset the password retry count
        UPDATE Users SET PasswordRetryCount = 0 WHERE UserID = @UserID;
		PRINT 'CORRECT PASSWORD';
		RETURN;
    END
    ELSE
    BEGIN
	PRINT 'WRONG PASSWORD';
        -- Password is incorrect or user is inactive
        SET @IsAuthenticated = 0;
        SET @IsDisabled = 0;
		SET @UserId = 0;

        -- Increment the password retry count
        UPDATE Users SET PasswordRetryCount = PasswordRetryCount + 1 WHERE UserID = @ID;

		PRINT 'UPDATED RETRY COUNT';
        -- Check the new retry count
        SELECT @PasswordRetryCount = PasswordRetryCount, @UserStatus=UserStatus FROM Users WHERE UserID = @UserID;
		PRINT @PasswordRetryCount

        IF @PasswordRetryCount >= 3 AND @UserStatus = 1
        BEGIN
            -- Disable the user
            UPDATE Users SET UserStatus = 0 WHERE UserID = @UserID;
            SET @IsDisabled = 1;
			PRINT 'USER DISABLED';
			PRINT @IsDisabled;
			
        END
		RETURN;
    END
END;
GO
