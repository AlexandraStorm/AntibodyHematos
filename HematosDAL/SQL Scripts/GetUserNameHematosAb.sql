IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserNameHematosAb]'))
DROP Function [dbo].[GetUserNameHematosAb]
GO

/****** Object:  UserDefinedFunction [dbo].[GetUserNameHematosAb]    Script Date: 1/11/2015 4:52:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[GetUserNameHematosAb] (@UserID INT)
   RETURNS VARCHAR (55)
AS
   BEGIN
      DECLARE @nRet   VARCHAR (55)

      SET @nRet =
             (SELECT UserName
            FROM dbo.tbUser
            WHERE dbo.tbUser.UserID = @UserID)
      RETURN @nRet
   END
GO
