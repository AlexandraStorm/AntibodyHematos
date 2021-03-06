IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExportedHematosAbData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetExportedHematosAbData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 11-11-15
-- Description:	Retrieves previous data for samples already exported
-- =============================================
Create PROCEDURE [dbo].[GetExportedHematosAbData]
	-- Add the parameters for the stored procedure here
	@batchID nvarchar(100),
	@sampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT [sampleID], [SpecCode], [Comments]
			 FROM tbHematosABExportHistory WHERE [BatchID] = @batchID and [sampleID] = @sampleID
END
GO