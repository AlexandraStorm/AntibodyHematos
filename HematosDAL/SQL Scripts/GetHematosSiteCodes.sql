IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetHematosAbSiteCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetHematosAbSiteCodes]
GO

/****** Object:  StoredProcedure [dbo].[CheckBatch]    Script Date: 12/16/2014 16:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 12-16-14
-- Description:	Retrives Hematos Site Codes for Export
-- =============================================
Create PROCEDURE [dbo].[GetHematosAbSiteCodes]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	SELECT Concat([Site Code],' - ',[Description]) AS [SiteCode] FROM  [dbo].[HematosAbSiteCodes]
END
