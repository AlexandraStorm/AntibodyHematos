IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLumInfoHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLumInfoHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-5-15
-- Description:	Retrieves Luminex Info for export
-- =============================================
Create PROCEDURE [dbo].[GetLumInfoHematosAb]
	-- Add the parameters for the stored procedure here
	@batchID nvarchar(100),
	@Processed int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	CREATE TABLE #HematosSamples
	(
		BatchID NVARCHAR(100),
		sampleID NVARCHAR(100)
	);

	CREATE TABLE #HematosSamplesRET
	(
		BatchID NVARCHAR(100),
		sampleID NVARCHAR(100),
		CheckSpecification INT
	);
	
	IF @Processed = 0
		BEGIN
		INSERT INTO #HematosSamples 
			SELECT [sessionID] AS [BatchID], [sampleID]
				FROM tbAntibodyMethod WHERE [sessionID] = @batchID and [approved] = 1
			EXCEPT
			SELECT [BatchID], [sampleID]
				FROM tbHematosABExportHistory WHERE [BatchID] = @batchID

				INSERT INTO #HematosSamplesRET SELECT BatchID, sampleID, dbo.CheckTailHematosAb(BatchID, sampleID) as CheckSpecification 
					FROM #HematosSamples

					SELECT * FROM #HematosSamplesRET
				
		END
		ELSE
		BEGIN
		INSERT INTO #HematosSamples 
			SELECT [sessionID] AS [BatchID], [sampleID]
				FROM tbAntibodyMethod WHERE sampleID IN (
			SELECT [sampleID]
			 FROM tbHematosABExportHistory WHERE [BatchID] = @batchID) and sessionID = @batchID

			 INSERT INTO #HematosSamplesRET SELECT BatchID, sampleID, dbo.CheckTailHematosAb(BatchID, sampleID) as CheckSpecification 
					FROM #HematosSamples

					SELECT * FROM #HematosSamplesRET	 
		END
END
GO