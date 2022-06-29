IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSampleInfo_LMXHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSampleInfo_LMXHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-13-15
-- Description:	Retrieves LMX sample results for export
-- =============================================
Create PROCEDURE [dbo].[GetSampleInfo_LMXHematosAb]
	-- Add the parameters for the stored procedure here
	@BatchID nvarchar(100),
	@SampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT Distinct tbAntibodyMethod.sessionID AS [Batch Name], tbAntibodyMethod.sampleID, tbAntibodyMethod.lotID, ClassI.result AS ClassIResults, 
                         ClassII.result AS ClassIIResults, tbAntibodyMethod.comments, dbo.[GetUserNameHematosAb](tbAntibodyMethod.completedBy) As completedBy, CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) As completedDt, dbo.[GetUserNameHematosAb](tbAntibodyMethod.approvedBy) As approvedBy
FROM            tbAntibodyMethod INNER JOIN
                         tbAntibodyResults AS ClassI ON tbAntibodyMethod.AntibodyID = ClassI.AntibodyID INNER JOIN
                         tbAntibodyResults AS ClassII ON tbAntibodyMethod.AntibodyID = ClassII.AntibodyID
						 Where tbAntibodyMethod.sessionID = @BatchID and tbAntibodyMethod.sampleID = @SampleID and ClassI.resultType = 'ClassI' and ClassII.resultType = 'ClassII'
END
GO