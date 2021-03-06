IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBatchesHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBatchesHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 12-16-14
-- Description:	Retrieves batches for export
-- =============================================
Create PROCEDURE [dbo].[GetBatchesHematosAb]
	-- Add the parameters for the stored procedure here
	@lotid nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure hereDeclare @lotid nvarchar(100)

SELECT [sessionID] AS [BatchID] FROM tbBatch WHERE [lotID] = @lotid ORDER BY createdt DESC
END
