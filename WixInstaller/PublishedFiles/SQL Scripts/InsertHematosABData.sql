IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertHematosABData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertHematosABData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-11-15
-- Description:	Inserts export data
-- =============================================
Create PROCEDURE [dbo].[InsertHematosABData]
	-- Add the parameters for the stored procedure here
	@FileName nvarchar (300),
	@LotID nvarchar(30),
	@BatchID nvarchar (100),
	@SampleID nvarchar(100),
	@LuminexID varchar(20),
	@SiteCode varchar(2),
	@SpecCode varchar(20),
	@Comments nvarchar(1024)	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO dbo.tbHematosABExportHistory Values(@FileName, GETDATE(), @LotID, @BatchID, @SampleID, @LuminexID, @SiteCode, @SpecCode, @Comments)
END
GO