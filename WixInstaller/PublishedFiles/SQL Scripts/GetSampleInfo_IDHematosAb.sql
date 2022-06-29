IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSampleInfo_IDHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSampleInfo_IDHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-11-15
-- Description:	Retrieves ID sample results for export
-- =============================================
Create PROCEDURE [dbo].[GetSampleInfo_IDHematosAb]
	-- Add the parameters for the stored procedure here
	@BatchID nvarchar(100),
	@SampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF (SELECT  count(tbAntibodyMethod.sampleID)
FROM            dbo.tbAntibodyStats  INNER JOIN
                         dbo.tbAntibodyMethod ON tbAntibodyMethod.AntibodyID = tbAntibodyStats.AntibodyID					
						  Where sessionID  = @BatchID and sampleID = @SampleID and (dbo.tbAntibodyStats.tail > 0)) = 0
						  Begin
						  SELECT  tbAntibodyMethod.sessionID AS batchID, tbAntibodyMethod.sampleID, dbo.GetUserNameHematosAb(tbAntibodyMethod.completedBy) AS completedBy, 
                         CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) AS completedDt, dbo.GetUserNameHematosAb(tbAntibodyMethod.approvedBy) AS approvedBy, tbAntibodyMethod.comments
FROM                      dbo.tbAntibodyMethod		
						  Where sessionID  = @BatchID and sampleID = @SampleID
						  END


Else 
		Begin

		SELECT  tbAntibodyMethod.sessionID AS batchID, tbAntibodyMethod.sampleID, tbAntibodyStats.antigen, dbo.GetUserNameHematosAb(tbAntibodyMethod.completedBy) AS completedBy, 
                         CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) AS completedDt, dbo.GetUserNameHematosAb(tbAntibodyMethod.approvedBy) AS approvedBy, CAST(Round(tbAntibodyStats.strength, 0) as int) AS [Strength], tbAntibodyMethod.comments
FROM            dbo.tbAntibodyStats  INNER JOIN
                         dbo.tbAntibodyMethod ON tbAntibodyMethod.AntibodyID = tbAntibodyStats.AntibodyID					
						  Where sessionID  = @BatchID and sampleID = @SampleID and (dbo.tbAntibodyStats.tail > 0)
						  Order By dbo.tbAntibodyStats.antigen
		END
END
GO